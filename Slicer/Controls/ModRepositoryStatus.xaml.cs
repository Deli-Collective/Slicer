﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Slicer.Backend;
using Slicer.Backend.ModOperation;

namespace Slicer.Controls
{
    /// <summary>
    ///     Interaction logic for RepositoryStatus.xaml
    /// </summary>
    public partial class ModRepositoryStatus : UserControl
    {
        public ModRepositoryStatus()
        {
            InitializeComponent();
            ModRepository.Instance.RepositoryUpdated += Update;
        }

        private void Update(ModRepository.State state, Exception e)
        {
            App.RunInMainThread(() =>
            {
                ButtonRefresh.IsEnabled = true;
                switch (state)
                {
                    case ModRepository.State.Error:
                        StatusIcon.Text = SegoeGlyphs.X;
                        StatusIcon.Foreground = new SolidColorBrush(Colors.Red);
                        LastUpdateText.Text = e.Message;
                        StatusText.Text = "Error";
                        break;
                    case ModRepository.State.CantUpdate:
                        StatusIcon.Text = "\uF13C";
                        StatusIcon.Foreground = new SolidColorBrush(Colors.Orange);
                        LastUpdateText.Text = e.Message;
                        StatusText.Text = "Offline";
                        break;
                    case ModRepository.State.UpToDate:
                        StatusIcon.Text = SegoeGlyphs.Checkmark;
                        StatusIcon.Foreground = new SolidColorBrush(Colors.Green);
                        LastUpdateText.Text =
                            $"Last update: {ModRepository.Instance.Repo.Head.Commits.First().Author.When}";
                        StatusText.Text = "Up to date!";
                        break;
                }
            });
        }

        private void ButtonRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusIcon.Text = SegoeGlyphs.Repeat;
            StatusIcon.Foreground = new SolidColorBrush(Colors.Orange);
            LastUpdateText.Text = "Please wait...";
            StatusText.Text = "Fetching data...";
            ButtonRefresh.IsEnabled = false;
            App.RunInBackgroundThread(ModRepository.Instance.Refresh);
        }
    }
}