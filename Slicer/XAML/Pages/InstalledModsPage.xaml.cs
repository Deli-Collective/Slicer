﻿using Deli_Counter.Controls;
using Deli_Counter.XAML.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Deli_Counter.XAML.Pages
{
    /// <summary>
    /// Interaction logic for InstalledModsPage.xaml
    /// </summary>
    public partial class InstalledModsPage : ModernWpf.Controls.Page
    {
        public List<object> Mods;

        public InstalledModsPage()
        {
            InitializeComponent();

            foreach (var mod in ModRepository.Mods!.Where(x => x.Installed))
            {
                var latest = mod.LatestVersion;

                var card = new ModDisplayCard();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(latest.IconUrl, UriKind.Absolute);
                bitmap.EndInit();
                card.ModImage.Source = bitmap;
                card.ModShortDescription.Text = latest.ShortDescription;
                card.ModTitle.Text = latest.Name;

                ModList.Items.Add(card);
            }
        }
    }
}
