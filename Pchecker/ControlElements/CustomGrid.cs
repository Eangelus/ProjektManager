﻿using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjektManager.ControlElements
{
    public class CustomGrid: Grid
    {

        public CustomGrid() {
            ColumnDefinitions.Add(new ColumnDefinition());
        }

    }
}
