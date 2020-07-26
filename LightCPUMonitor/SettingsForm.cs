/*
 
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
	Copyright (C) 2020 Tomasz Kromrych <tkromrych@gmail.com>
	
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightCPUMonitor
{
		public partial class SettingsForm : Form
		{
				DataTable dt1 = new DataTable();
				DataTable dt = new DataTable();

				public SettingsForm()
				{
						InitializeComponent();
						dt1.Columns.Add("key");
						dt1.Columns.Add("text");
						dt.Columns.Add("key");
						dt.Columns.Add("text");
						dt.Rows.Add("/none", "Hide");

						cbIcon1.DataSource = dt1;
						cbIcon1.BindingContext = new BindingContext();
						cbIcon1.DisplayMember = "text";
						cbIcon1.ValueMember = "key";

						cbIcon2.DataSource = dt;
						cbIcon2.BindingContext = new BindingContext();
						cbIcon2.DisplayMember = "text";
						cbIcon2.ValueMember = "key";

						cbIcon3.DataSource = dt;
						cbIcon3.BindingContext = new BindingContext();
						cbIcon3.DisplayMember = "text";
						cbIcon3.ValueMember = "key";

						cbIcon4.DataSource = dt;
						cbIcon4.BindingContext = new BindingContext();
						cbIcon4.DisplayMember = "text";
						cbIcon4.ValueMember = "key";

				}

				public void AddSensorItem(string key, string name)
				{
						dt.Rows.Add(key, name);
						dt1.Rows.Add(key, name);
				}

				public List<string> GetSelection()
				{
						List<string> results = new List<string>();
						results.Add(cbIcon1.SelectedValue.ToString());
						results.Add(cbIcon2.SelectedValue.ToString());
						results.Add(cbIcon3.SelectedValue.ToString());
						results.Add(cbIcon4.SelectedValue.ToString());
						return results;
				}

				public void SetInitialKeys(List<string> keys)
				{
						cbIcon1.SelectedValue = keys.Count() > 0 ? keys[0] : dt1.Rows[0]["key"].ToString();
						cbIcon2.SelectedValue = keys.Count() > 1 ? keys[1] : dt.Rows[0]["key"].ToString();
						cbIcon3.SelectedValue = keys.Count() > 2 ? keys[2] : dt.Rows[0]["key"].ToString();
						cbIcon4.SelectedValue = keys.Count() > 3 ? keys[3] : dt.Rows[0]["key"].ToString();
				}
		}
}
