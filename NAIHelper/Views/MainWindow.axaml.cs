using Avalonia.Controls;
using Avalonia.Input;
using HtmlAgilityPack;
using NAIHelper.Utils;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace NAIHelper.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
        {
            //if (e.Key == Key.D0 && e.KeyModifiers == KeyModifiers.Control)
            //{
            //    new DanbooruDownloader().DownloadAll();
            //}
        }
        
    }
}
