﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// FoodPage.xaml 的交互逻辑
    /// </summary>
    public partial class FoodPage : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_loadedTime != 0) return;
            _loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            var extraData = (string[])e.ExtraData;
            Deserialize();
            if (extraData == null)
            {
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), Global.FoodRecipeData[0]);
            }
            else
            {
                var suggestBoxItemPicture = extraData[1];
                //导航到指定页面
                switch (extraData[0])
                {
                    case "FoodRecipe":
                        OnNavigatedToFoodRecipeDialog(suggestBoxItemPicture);
                        break;
                    case "FoodMeats":
                        OnNavigatedToFoodDialog(Global.FoodMeatData, suggestBoxItemPicture);
                        break;
                    case "FoodVegetables":
                        OnNavigatedToFoodDialog(Global.FoodVegetableData, suggestBoxItemPicture);
                        break;
                    case "FoodFruits":
                        OnNavigatedToFoodDialog(Global.FoodFruitData, suggestBoxItemPicture);
                        break;
                    case "FoodEggs":
                        OnNavigatedToFoodDialog(Global.FoodEggData, suggestBoxItemPicture);
                        break;
                    case "FoodOthers":
                        OnNavigatedToFoodDialog(Global.FoodOtherData, suggestBoxItemPicture);
                        break;
                    case "FoodNoFc":
                        OnNavigatedToFoodDialog(Global.FoodNoFcData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToFoodRecipeDialog(string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in Global.FoodRecipeData)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), food);
                break;
            }
        }

        private void OnNavigatedToFoodDialog(List<Food> foodCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in foodCollection)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new FoodDetail(), food);
                break;
            }
        }

        public FoodPage()
        {
            InitializeComponent();
            Global.FoodLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        public void Deserialize()
        {
                RecipesExpander.DataContext = Global.FoodRecipeData;
                MeatsExpander.DataContext = Global.FoodMeatData;
                VegetablesExpander.DataContext = Global.FoodVegetableData;
                FruitsExpander.DataContext = Global.FoodFruitData;
                EggsExpander.DataContext = Global.FoodEggData;
                OtherExpander.DataContext = Global.FoodOtherData;
                NoFcExpander.DataContext = Global.FoodNoFcData;
        }

        private void FoodRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var foodRecipe = (FoodRecipe2)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), foodRecipe);
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            var food = (Food)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new FoodDetail(), food);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
