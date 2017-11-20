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
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// GoodMaterialDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodMaterialDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodMaterial)e.ExtraData);
        }

        public GoodMaterialDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private void LoadData(GoodMaterial c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 可制作科技/来源于生物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Science == null || c.Science.Count == 0)
            {
                GoodScienceTextBlock.Visibility = Visibility.Collapsed;
                GoodScienceWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Science)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    //picButton.Tapped += Good_Jump_Tapped;
                    GoodScienceWrapPanel.Children.Add(picButton);
                }
            }
            if (c.SourceCreature == null || c.SourceCreature.Count == 0)
            {
                GoodSourceCreatureTextBlock.Visibility = Visibility.Collapsed;
                GoodSourceCreatureWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.SourceCreature)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    //picButton.Tapped += Good_Jump_Tapped;
                    GoodSourceCreatureWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            GoodIntroduction.Text = c.Introduction;
            // 控制台
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }


        private void ConsoleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.ConsoleNumTextCheck(textbox);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConsoleNum.Text))
            {
                ConsoleNum.Text = "1";
            }
            Clipboard.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
        }
    }
}
