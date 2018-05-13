﻿using ImageServiceApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageServiceApp
{
    /// <summary>
    /// Interaction logic for SettingsWin.xaml
    /// </summary>
    public partial class SettingsWin : UserControl
    {
        private SettingsViewModel SettingsViewModel;

        public SettingsWin()
        {
            InitializeComponent();
            SettingsViewModel = new SettingsViewModel();
            this.DataContext = SettingsViewModel;
        }
    }
}
