/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
  Copyright (C) 2020 Tomasz Kromrych <tkromrych@gmail.com>
	
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace LightCPUMonitor {
    class LightCPUMonitorAppContext : ApplicationContext

    {
        const int DELAY_TIME = 500;
        const int UPDATE_TIME = 500;

        private IDictionary<IHardware, IList<ISensor>> sensors =
            new SortedDictionary<IHardware, IList<ISensor>>(new HardwareComparer());
        List<NotifyIcon> icons = new List<NotifyIcon>();
        List<int> icon_sensors_idx = new List<int> { -1, -1, -1, -1 };
        
        Font fontToUse = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brushToUse = new SolidBrush(Color.Black);
        Bitmap bitmapText = new Bitmap(32, 32);
        Graphics g;
        IntPtr hIcon = IntPtr.Zero;
        System.Threading.Timer updateTimer;
        private OpenHardwareMonitor.PersistentSettings settings;
        Computer computer;
        object lockObject = new object();
        public LightCPUMonitorAppContext()
        {
           

            for (int i = 0; i < 4; i++)
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
                MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));
                notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                { configMenuItem, exitMenuItem });
                notifyIcon.Visible = true;
                icons.Add(notifyIcon);
            }

            this.settings = new OpenHardwareMonitor.PersistentSettings();
            this.settings.Load(Path.ChangeExtension(Application.ExecutablePath, ".config"));
            settings.SetValue("CPUEnabled", true);
            this.computer = new Computer(settings);
            computer.CPUEnabled = true;
            computer.Open();
            IList<ISensor> list;
            if (!sensors.TryGetValue(computer.Hardware[0], out list))
            {
                list = new List<ISensor>();
                sensors.Add(computer.Hardware[0], list);
            }

            // insert the sensor at the right position
            foreach (var sensor in computer.Hardware[0].Sensors)
            {
                int i = 0;
                while (i < list.Count && (list[i].SensorType < sensor.SensorType ||
                  (list[i].SensorType == sensor.SensorType &&
                   list[i].Index < sensor.Index))) i++;
                list.Insert(i, sensor);
            }
            g = System.Drawing.Graphics.FromImage(bitmapText);
            g.PageUnit = GraphicsUnit.Pixel;
            UpdateSensorIndexes();
            //notifyIcon.Icon = CPUSpeed.Properties.Resources.AppIcon;
            
            updateTimer = new System.Threading.Timer(new TimerCallback(UpdateValue));
            updateTimer.Change(DELAY_TIME, UPDATE_TIME);
        }

        private void ShowConfig(object sender, EventArgs e)
        {
            updateTimer.Change(Timeout.Infinite, Timeout.Infinite);
            SettingsForm f = new SettingsForm();
            foreach (var s in sensors[computer.Hardware[0]])
            {
                f.AddSensorItem(s.Identifier.ToString(), s.Name + " - " + s.SensorType.ToString());
            }
            List<string> keys = new List<string>();
            for (int i = 1; i<5; i++)
            {
                if (!settings.Contains("icon" + i.ToString().Trim()))
                    break;
                keys.Add(settings.GetValue("icon" + i.ToString().Trim(),""));
            }
            f.SetInitialKeys(keys);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //update icons, save params
                var results = f.GetSelection();
                int index = 1;
                foreach (var line in results)
                {
                    settings.SetValue("icon" + index.ToString().Trim(), line);
                    index++;
                }
                this.settings.Save(Path.ChangeExtension(Application.ExecutablePath, ".config"));
                UpdateSensorIndexes();
            }
            updateTimer.Change(DELAY_TIME, UPDATE_TIME);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
        void UpdateIconText()
        {
            int icon_counter = -1;
            foreach (var icon in icons)
            {
                icon_counter++;
                if (!icon.Visible)
                    continue;
                string icon_value = "N/A";
                if (icon_sensors_idx[icon_counter] > -1)
                {
                    var sensor = sensors[computer.Hardware[0]][icon_sensors_idx[icon_counter]];
                    string format = "{0:F1}";
                    float? value = sensor.Value;
                    switch (sensor.SensorType)
                    {
                        case SensorType.Voltage: format = "{0:F3}"; break;
                        case SensorType.Clock: format = "{0:F1}"; value = value / 1000f; break;
                        case SensorType.Load: format = "{0:F1}"; break;
                        case SensorType.Temperature: format = "{0:F0}°"; break;
                        case SensorType.Fan: format = "{0:F0}"; break;
                        case SensorType.Flow: format = "{0:F0}"; break;
                        case SensorType.Control: format = "{0:F1}%"; break;
                        case SensorType.Level: format = "{0:F1}%"; break;
                        case SensorType.Power: format = "{0:F0}W"; break;
                        case SensorType.Data: format = "{0:F1}"; break;
                        case SensorType.SmallData: format = "{0:F1}"; break;
                        case SensorType.Factor: format = "{0:F3}"; break;
                    }
                    icon_value = string.Format(format, value);
                }
                g.Clear(Color.White);
                SizeF room = new Size(16, 16);
                Font goodFont = FindFont(icon_value, ref room, fontToUse);
                float textHeight = 0f;
                goodFont = GetAdjustedFont(icon_value, fontToUse, 32f, 20, 5, true, ref textHeight);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                //SizeF size = g.MeasureString(str, fontToUse);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                //stringFormat.LineAlignment = StringAlignment.Center;
                g.DrawString(icon_value, goodFont, brushToUse, new RectangleF(0f, (32f - textHeight) / 2f, 32f, 32f), stringFormat);

                hIcon = (bitmapText.GetHicon());
                icon.Icon = System.Drawing.Icon.FromHandle(hIcon);
                if (hIcon != IntPtr.Zero)
                {
                    try
                    {
                        DestroyIcon(hIcon);
                    }
                    catch
                    {
                    }


                }
            }
        }
        private void UpdateValue(object status)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    computer.Hardware[0].Update();

                    //s = "AB";
                    UpdateIconText();
                } finally
                {
                    Monitor.Exit(lockObject);
                }
            } 
        }

        private Font FindFont(string longString, ref SizeF Room, Font PreferedFont)
        {
            // you should perform some scale functions!!!
            SizeF RealSize = g.MeasureString(longString, PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;

            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio)
               ? ScaleRatio = HeightScaleRatio
               : ScaleRatio = WidthScaleRatio;

            float ScaleFontSize = PreferedFont.Size * WidthScaleRatio;
            Room.Height = RealSize.Height * WidthScaleRatio;
            Room.Width = RealSize.Width * WidthScaleRatio; 

            return new Font(PreferedFont.FontFamily, ScaleFontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        }


        public Font GetAdjustedFont(string GraphicString, Font OriginalFont, float ContainerWidth, int MaxFontSize, int MinFontSize, bool SmallestOnFail, ref float ResultHeight)
        {
            // We utilize MeasureString which we get via a control instance           
            for (float AdjustedSize = MaxFontSize; AdjustedSize >= MinFontSize; AdjustedSize-=0.5f)
            {
                Font TestFont = new Font(OriginalFont.Name, AdjustedSize, OriginalFont.Style, GraphicsUnit.Pixel);

                // Test the string with the new size
                SizeF AdjustedSizeNew = g.MeasureString(GraphicString, TestFont);

                if (ContainerWidth >= AdjustedSizeNew.Width)
                {
                    // Good font, return it
                    ResultHeight = AdjustedSizeNew.Height;
                    return TestFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return MinimumSize or Original?
            if (SmallestOnFail)
            {
                Font minFont = new Font(OriginalFont.Name, MinFontSize, OriginalFont.Style);
                SizeF AdjustedSizeNew = g.MeasureString(GraphicString, minFont);
                ResultHeight = AdjustedSizeNew.Height;
                return minFont ;
            }
            else
            {
                return OriginalFont;
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            updateTimer.Change(Timeout.Infinite, Timeout.Infinite);
            while (!Monitor.TryEnter(lockObject)) ;
            foreach (var icon in icons) 
                icon.Visible = false;
            computer.Close();

            Application.Exit();
        }

        void UpdateSensorIndexes()
        {
            for (int i = 1; i < 5; i++)
            {
                icon_sensors_idx[i - 1] = -1;
                if (settings.Contains("icon" + i.ToString().Trim()))
                {
                    var id = settings.GetValue("icon" + i.ToString().Trim(), "");
                    var index = 0;
                    foreach (var sensor in sensors[computer.Hardware[0]])
                    {
                        if (sensor.Identifier.ToString() == id)
                        {
                            icon_sensors_idx[i - 1] = index;
                            break;
                        }
                        index++;
                    }
                }
                icons[i - 1].Visible = (icon_sensors_idx[i - 1] > -1);
            }
            if (icon_sensors_idx[0] == -1)
            {
                icon_sensors_idx[0] = 0;
                icons[0].Visible = true;
            }

        }

        private class HardwareComparer : IComparer<IHardware>
        {
            public int Compare(IHardware x, IHardware y)
            {
                if (x == null && y == null)
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;

                if (x.HardwareType != y.HardwareType)
                    return x.HardwareType.CompareTo(y.HardwareType);

                return x.Identifier.CompareTo(y.Identifier);
            }
        }

    }
}
