﻿using System;
using System.Windows;
using System.Windows.Data;
using Slicer.Backend;
using ModernWpf;
using Slicer.Controls;
using Slicer.Properties;

namespace Slicer.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage
    {
        private Settings _settings;

        public SettingsPage()
        {
            InitializeComponent();
            _settings = Settings.Default;
            DataContext = _settings;
        }

        private void AutoDetectGameLocation_OnChecked(object sender, RoutedEventArgs e)
        {
            _settings.GameLocation = GameLocator.GameDirectory;

            // If it wasn't set revert since it can't be found automatically
            if (string.IsNullOrEmpty(_settings.GameLocation))
            {
                _settings.AutoDetectGameLocation = false;
                var dialogue = new AlertDialogue("Couldn't detect game folder",
                    "Hey! It seems we couldn't auto-detect your game folder. Please set it manually!");
                dialogue.ShowAsync();
            }
        }

        private void DarkMode_OnChecked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ApplicationTheme =
                _settings.EnableDarkMode ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }

        private void GenerateDiagnosticFile(object sender, RoutedEventArgs e) => InfoCollector.CollectAll();
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}