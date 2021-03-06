﻿/*
 
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
	Copyright (C) 2020 Tomasz Kromrych <tkromrych@gmail.com>
	
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LightCPUMonitor
{
		static class Program
		{
				/// <summary>
				/// The main entry point for the application.
				/// </summary>
				[STAThread]
				static void Main()
				{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(new LightCPUMonitorAppContext());
				}
		}
}
