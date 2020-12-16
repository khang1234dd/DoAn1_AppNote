using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.IO;

namespace AppNote
{
    public class MainPage : ContentPage
    {
        Editor noteEditer;
        Label textLabel;
        Button saveButton, deleteButton,saveFile,docFile,nextPage;
        ScrollView scroll;
        Note note = new Note();

        public MainPage()
        {
            
            BackgroundColor = Color.PowderBlue;
            NavigationPage.SetHasNavigationBar(this, false);

            //Tạo đối tượng cho Image
            Image noteImage = new Image
            {
                Source = "stickynote.png"
            };

            //Tạo đối tượng cho Editor
            noteEditer = new Editor
            {
                Placeholder = "Enter Note",  // phương thức giữ vị trí trong Editor
                BackgroundColor = Color.White, // Nền của Editor
                Margin = new Thickness(10) // Khoảng cách các lề
            };



            // nút Lưu
            saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Margin = new Thickness(5)
            };
            saveButton.Clicked += SaveButton_Clicked;

            // nút Xóa
            deleteButton = new Button
            {
                Text = "Delete",
                TextColor = Color.White,
                BackgroundColor = Color.LightGreen,
                Margin = new Thickness(5)
            };
            deleteButton.Clicked += DeleteButton_Clicked;

            saveFile = new Button
            {
                Text = "Save File",
                TextColor = Color.White,
                BackgroundColor = Color.Blue,
                Margin = new Thickness(5)
            };
            saveFile.Clicked += SaveFile_Clicked;
            docFile = new Button
            {
                Text = "Load",
                TextColor = Color.White,
                BackgroundColor = Color.Orange,
                Margin = new Thickness(5)
            };
            docFile.Clicked += DocFile_Clicked;

            textLabel = new Label
            {
                FontSize = 30,
                BackgroundColor = Color.FromHex("FDFFAA"),
                TextColor = Color.Black,
                Text = note.text,
                Margin = new Thickness(5)
            };
            //textLabel_clic
            //var secondpage = new SecondPage();
            //secondpage.BindingContext = textLabel.Text;
            scroll = new ScrollView();
            scroll.Content = textLabel;

            nextPage = new Button
            {
                Text = "View",
                TextColor = Color.White,
                BackgroundColor = Color.LightPink,
                Margin = new Thickness(5)
            };
            nextPage.Clicked += NextPage_Clicked;
            //Quy gird gồm 2 cột và 4 hàng
            Grid grid = new Grid
            {
                Margin = new Thickness(20, 40),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                }
            };

            //Vị trí của từng đối tượng trên Lưới.

            // gọi đối tượng image ở vị trí hàng 0 cột 0
            grid.Children.Add(noteImage, 0, 0);
            Grid.SetColumnSpan(noteImage, 2);
            //gọi đối tượng Editor ở vị trí hàng 1 cột 0
            grid.Children.Add(noteEditer, 0, 1);
            Grid.SetColumnSpan(noteEditer, 2);

            // gọi đối tượng buttton ở vị trí hàng 2 cột 0 và deletebutton hàng 2 cột 1
            grid.Children.Add(saveButton, 0, 2);
            grid.Children.Add(deleteButton, 1, 2);

            grid.Children.Add(saveFile, 0, 3);
            grid.Children.Add(docFile, 1, 3);
            //Grid.SetColumnSpan(saveFile, 2);
            // gọi đối tượng text Label hiển thị ở vị trí hàng 3 cột 0
            grid.Children.Add(scroll, 0, 4);
            Grid.SetColumnSpan(scroll, 2);
            // button next page
            grid.Children.Add(nextPage, 0, 5);
            Grid.SetColumnSpan(nextPage, 2);
            // scroll.Content = textLabel;
            Content = grid;

            
        }

        async void NextPage_Clicked(object sender, EventArgs e)
        {
            SecondPage secondPage = new SecondPage();
            secondPage.BindingContext = textLabel;
            await Navigation.PushAsync(secondPage);
        }

        private void DocFile_Clicked(object sender, EventArgs e)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileName = Path.Combine(documentsPath, "temp.txt");
            bool doesExist = File.Exists(fileName);
            if (doesExist == true)
            {
                string text = File.ReadAllText(fileName);
                textLabel.Text = text;
            }
            else
            {
                textLabel.Text = "False";
            }    
        }

        private void SaveFile_Clicked(object sender, EventArgs e)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileName = Path.Combine(documentsPath, "temp.txt");
            bool doesExist = File.Exists(fileName);
            File.WriteAllText(fileName, note.text);
            if(doesExist == true && note.text != null)
            {
                textLabel.Text = "Done!";
            }
            else
            {
                textLabel.Text = "File does not exist!";
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            textLabel.Text = "";
            noteEditer.Text = "";
            Note note = new Note();
            note.text = noteEditer.Text;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            note.text = noteEditer.Text;
            textLabel.Text = note.text;
        }

    }
}
