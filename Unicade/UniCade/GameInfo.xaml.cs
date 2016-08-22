﻿using System;
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
using System.Windows.Shapes;

namespace UniCade
{
    /// <summary>
    /// Interaction logic for GameInfo.xaml
    /// </summary>
    public partial class GameInfo : Window
    {
        bool enlarge = false;


        public GameInfo()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);


        }

        public void displayEsrb(String esrb)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(@esrb);
            b.EndInit();
            image3.Source = b;
            
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Escape) || (e.Key == Key.Back)|| (e.Key == Key.I))
            {
                Close();

            }

            if ((e.Key == Key.Tab) )
            {
                if (!enlarge)
                {
                    image3.Width = 500;
                    image3.Height = 500;
                    enlarge = true;
                }
                else
                {
                    image3.Width = 200;
                    image3.Height = 200;
                    enlarge = false;
                }

            }


        }
    }
}
