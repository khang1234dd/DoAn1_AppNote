using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppNote
{
    public class SecondPage : ContentPage
    {
        Label textLabelPage2;
        ScrollView scroll;
        Button quayLai;
        public SecondPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Color.PowderBlue;
            textLabelPage2 = new Label
            {
                BackgroundColor = Color.FromHex("#66CCFF"),
                FontSize = 30,
                TextColor = Color.Black,
                Margin = new Thickness(5)
            };
            textLabelPage2.SetBinding(Label.TextProperty, "Text");

            scroll = new ScrollView();
            scroll.Content = textLabelPage2;

            quayLai = new Button
            {
                Text = "Back",
                BackgroundColor = Color.Gray,
                TextColor = Color.White,
                
            };
            quayLai.Clicked += QuayLai_Clicked;


            Grid grid = new Grid
            {
                Margin = new Thickness(20, 40),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(5.0,GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(textLabelPage2, 0, 0);
            grid.Children.Add(quayLai, 0, 1);
            Content = grid;
        }

        async void QuayLai_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
