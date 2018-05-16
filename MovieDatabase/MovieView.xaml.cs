using Microsoft.Win32;
using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    /// 
    //enum used for window states
    enum WindowMode { VIEW, EDIT, CREATE }
    public partial class MovieView : Window
    {
        private WindowMode mode;
        private Database db;

        public MovieView()
        {
            InitializeComponent();
            //create new database
            db = new Database();
            //initialy set form to edit mode so data can't be entered
            SetFormToStartMode();
        }

        private void SetFormToStartMode()
        {
            //Start mode is essentially View mode only it contains a instruction label to tell the user how to enter the first movie
            SetFormToViewMode();
            Instruction.Visibility = Visibility.Visible;
            FirstButton.IsEnabled = false;
            LastButton.IsEnabled = false;
            NextButton.IsEnabled = false;
            PreviousButton.IsEnabled = false;
        }
        private void SetFormToViewMode()
        {
            //sets the form to view mode to allow the user to view movie records
            mode = WindowMode.VIEW;
            //enable and disable relavent file menu buttons
            FileButton.IsEnabled = true;
            NewButton.IsEnabled = true;
            LoadButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
            ExitButton.IsEnabled = true;


            //enable and disable relavent Edit menu buttons
            EditMenuButton.IsEnabled = true;
            CreateButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;

            //Enable and disable all relavent view menu buttons
            ViewMenuButton.IsEnabled = true;
            OrderTitleButton.IsEnabled = true;
            OrderDurationButton.IsEnabled = true;
            OrderYearButton.IsEnabled = true;

            //enable and disable all relavent Help menu buttons
            HelpMenuButton.IsEnabled = true;
            AboutButton.IsEnabled = true;

            //forms is in view mode so all text fields should be disabled
            Instruction.Visibility = Visibility.Collapsed;
            LabelUrl.Visibility = Visibility.Collapsed;
            TitleTxt.IsEnabled = false;
            DurationTxt.IsEnabled = false;
            YearTxt.IsEnabled = false;
            BudgetTxt.IsEnabled = false;
            RatingControl.IsEnabled = false;
            DirectorTxt.IsEnabled = false;
            GenreControl.IsEnabled = false;
            LabelUrl.Visibility = Visibility.Collapsed;
            URLTxt.Visibility = Visibility.Collapsed;
            ActorAdd.IsEnabled = false;
            Image.Visibility = Visibility.Visible;
            ImageBorder.Visibility = Visibility.Visible;
            ActorAdd.Text = "";

            //enable and disable relavent buttons
            FirstButton.Visibility = Visibility.Visible;
            LastButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            PreviousButton.Visibility = Visibility.Visible;
            SaveButtonForm.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            ActorAddButton.Visibility = Visibility.Collapsed;
            ActorDeleteButton.Visibility = Visibility.Collapsed;

            CheckNavigationButtons();
        }


        private void SetFormToCreateEditMode(WindowMode m)
        {
            //user is gievn the choice to be in edit or create
            mode = m;
            //enable and disable relavent file menu buttons
            FileButton.IsEnabled = false;
            NewButton.IsEnabled = false;
            LoadButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
            ExitButton.IsEnabled = false;

            //enable and disable relavent Edit menu buttons
            EditMenuButton.IsEnabled = false;
            CreateButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            ActorAddButton.IsEnabled = true;
            ActorDeleteButton.IsEnabled = true;

            //Enable and disable all relavent view menu buttons
            ViewMenuButton.IsEnabled = false;
            OrderTitleButton.IsEnabled = false;
            OrderDurationButton.IsEnabled = false;
            OrderYearButton.IsEnabled = false;

            //enable and disable all relavent Help menu buttons
            HelpMenuButton.IsEnabled = false;
            AboutButton.IsEnabled = false;

            //enable all data fields to allow data to be updated or rentered
            Instruction.Visibility = Visibility.Collapsed;
            LabelUrl.Visibility = Visibility.Visible;
            TitleTxt.IsEnabled = true;
            DurationTxt.IsEnabled = true;
            YearTxt.IsEnabled = true;
            BudgetTxt.IsEnabled = true;
            RatingControl.IsEnabled = true;
            DirectorTxt.IsEnabled = true;
            GenreControl.IsEnabled = true;
            URLTxt.IsEnabled = true;
            ActorAdd.IsEnabled = true;
            Image.Visibility = Visibility.Hidden;
            ImageBorder.Visibility = Visibility.Hidden;
            URLTxt.Visibility = Visibility.Visible;


            //enable and disable all relavent buttons
            FirstButton.Visibility = Visibility.Collapsed;
            LastButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            PreviousButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Visible;
            SaveButtonForm.Visibility = Visibility.Visible;
            ActorAddButton.Visibility = Visibility.Visible;
            ActorDeleteButton.Visibility = Visibility.Visible;

            //method which enables/disables relavent navigation buttons
            CheckNavigationButtons();
        }

        private Movie UpdateModelFromUI(Movie m)
        {
            //update values in the model with info from the UI
            m.Title = TitleTxt.Text;
            //use convert 32 method to allow string to allow int's to be converted
            m.Duration = Convert.ToInt32(DurationTxt.Text);
            m.Year = Convert.ToInt32(YearTxt.Text);
            m.Budget = Convert.ToInt32(BudgetTxt.Text);
            m.Director = DirectorTxt.Text;
            m.URL = URLTxt.Text;
            //add actors the list
            m.Actors = ListOfActors.Items.Cast<string>().ToList();
            //obtain values from the 2 user controls
            m.Genres = GenreControl.Selected;//add selected genres to the list
            m.Rating = RatingControl.Value;//obtain the value selected for rating
            return m;
        }

        private void UpdateUIFromModel(Movie m)
        {
            //bring all information from the database to the model
            TitleTxt.Text = m != null ? m.Title : "";
            DurationTxt.Text = m != null ? m.Duration.ToString() : "0";
            BudgetTxt.Text = m != null ? m.Budget.ToString() : "0";
            YearTxt.Text = m != null ? m.Year.ToString() : "0";
            DirectorTxt.Text = m != null ? m.Director : "";
            URLTxt.Text = m != null ? m.URL : "";

            ListOfActors.Items.Clear();
            //get actors and put them in the list box
            if (m != null)
            {
                foreach (var a in m.Actors)
                {
                    ListOfActors.Items.Add(a);
                }
            }

            //bring information from User controls
            RatingControl.Value = m != null ? m.Rating : 0;
            GenreControl.Selected = m != null ? m.Genres : new List<Genre>();

            //perform try/catch to check if image URL is valid and wether or not the image will display
            try
            {
                var imagePath = m.URL;
                var uriCheck = new Uri(imagePath, UriKind.Absolute);
                Image.Source = new BitmapImage(uriCheck);
            }
            catch (UriFormatException)
            {
                Image.Source = null;
            }
        }

        //method which check if the URL link provided is valid or not
        private bool ValidURL(string url)
        {
            try
            {
                var uriCheck = new Uri(url, UriKind.Absolute);
            }
            catch (UriFormatException)
            //URL link provided is not valid
            {
                return false;
            }
            return true;
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            //display information about the assignment and each group member
            MessageBox.Show("COM321 Group Assignment - Group Members:\nDiarmuid Bryson - B00709477\nSaoirse Brinkley - B00711059\nBronach McCartney - B00677646");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //sets form to edit mode and enables the save button
            if (db.Count() != 0)
            {
                SetFormToCreateEditMode(WindowMode.EDIT);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //allows the user to add a new movie
            if(db.Count() >= 0)
            {
                SetFormToCreateEditMode(WindowMode.CREATE);
            }
        }

        private bool ValidForm()
        {
            //method which checks to make sure all data fields are filled in before saving
            if (TitleTxt.Text == "" || DurationTxt.Text == "" || YearTxt.Text == "" || BudgetTxt.Text == "" || RatingControl.Value == 0 || GenreControl.Selected.Count == 0 || DirectorTxt.Text == "" || ListOfActors.Items.Count == 0)
            {
                return false;
            }
            return true;
        }

        private void SaveButtonForm_Click(object sender, RoutedEventArgs e)
        {
            //method which allows the user to add a new movie to the database
            //calls the validation method
            if (!ValidForm())
            {
                //some fields are left blank
                MessageBox.Show("Please ensure all fields are filled in correctly!");
                return;
            }
            if (!ValidURL(URLTxt.Text))
            {
                //invalid URL
                MessageBox.Show("Invalid URL!");
                return;
            }

            if (mode == WindowMode.CREATE)
            {
                //add movie to the database
                Movie m = UpdateModelFromUI(new Movie());
                db.Add(m);
            }
            else if (mode == WindowMode.EDIT)
            {
                //update details of existing movie
                Movie m = UpdateModelFromUI(db.Get());
                db.Update(m);
            }
            //form returns to view mode and the UI will update with data from the db
            SetFormToViewMode();
            UpdateUIFromModel(db.Get());
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //checks to see if any movies exist
            if(db.Count() != 0)
            {
                //movies already exist
                SetFormToViewMode();
                UpdateUIFromModel(db.Get());
            }
            else
            {
                //no movies exist
                SetFormToViewMode();
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            //initialise a new database by clearing all movies and initialise each textbox with an empty string
            //db = new Database();
            db.Clear();
            SetFormToViewMode();
            //UpdateUIFromModel(db.Get());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //asks the user do they want to close the program? yes will close, no will cancel the operation
            var selectionMade = MessageBox.Show("Do you wish to exit the program?", "Warning!", MessageBoxButton.YesNo);
            if (selectionMade == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //calls a method to present the user with the choice to close the application or not
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //method to allow the user to save movies to a json file

            var dialog = new SaveFileDialog()
            {
                //ensures only json files can be saved
                Filter = "json files|*.json",
                Title = "Please enter the name of the file you want to save"
            };

            if (dialog.ShowDialog() == true)
            {
                db.Save(dialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //method to allow the user to load a json file
            var dialog = new OpenFileDialog()
            {
                //ensures inly json files can be opened
                Filter = "json files|*.json",
                Title = "Enter the name of the file you want to load"
            };
            //if the user has entered a valid name, the file will be loaded
            if (dialog.ShowDialog() == true)
            {
                db.Load(dialog.FileName);
            }
            UpdateUIFromModel(db.Get());
            SetFormToViewMode();
        }
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            //perform check to call the first movie in the db by calling the first method
            //displays info in the UI
            if (db.First())
            {
                UpdateUIFromModel(db.Get());
                CheckNavigationButtons();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //navigates to the next movie by calling the next method
            //displays info in the UI
            if (db.Next())
            {
                UpdateUIFromModel(db.Get());
                CheckNavigationButtons();
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            //navigates to the previous movie by calling the prev method
            //displays info to the UI
            if (db.Prev())
            {
                UpdateUIFromModel(db.Get());
                CheckNavigationButtons();
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            //navigates to the last movie using the last method
            //displays info to the UI
            if (db.Last())
            {
                UpdateUIFromModel(db.Get());
                CheckNavigationButtons();
            }
        }

        //custom event handler
        private void OnlyAcceptDigitCheck(object sender, KeyEventArgs a)
        {
            //method which only allows the user to enter an int where suitable
            if (a.Key != Key.OemPeriod && (Key.D9 < a.Key || Key.D0 > a.Key))
            {
                a.Handled = true;
            }
            else
            {
                a.Handled = false;
            }
        }

        private void ActorAddButton_Click(object sender, RoutedEventArgs e)
        {
            //method whoch adds actors to the actor list
            if (ActorAdd.Text.Length > 0)
            {
                var actorToAdd = ActorAdd.Text;
                ListOfActors.Items.Add(actorToAdd);
                ActorAdd.Text = "";
            }
            else
            {
                //user has not entered a valid name
                MessageBox.Show("Please enter a valid name!", "Warning!");
            }
        }

        private void ActorDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //method which allows actors to be removed from the list
            var actorSelected = ListOfActors.SelectedIndex;
            if (actorSelected != -1)
            {
                //actor is removed
                ListOfActors.Items.RemoveAt(actorSelected);
            }
            else
            {
                //no actors are on the list
                MessageBox.Show("No actors to delete!", "Warning!");
            }
        }

        private void OrderTitleButton_Click(object sender, RoutedEventArgs e)
        {
            //orders all movies according to duration
            if (db.Count() > 0)
            {
                UpdateUIFromModel(db.Get());
                db.OrderByTitle();
            }
        }

        private void OrderYearButton_Click(object sender, RoutedEventArgs e)
        {
            //orders all movies in the database by year
            if (db.Count() > 0)
            {
                UpdateUIFromModel(db.Get());
                db.OrderByYear();
            }
        }

        private void OrderDurationButton_Click(object sender, RoutedEventArgs e)
        {
            //orders all movies in the database by duration
            if (db.Count() > 0)
            {
                UpdateUIFromModel(db.Get());
                db.OrderByDuration();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //deletes the current movie and displays the previous one
            if (db.Count() != 0)
            {
                db.Delete();
                UpdateUIFromModel(db.Get());
            }
        }

        private void CheckNavigationButtons()
        {
            //methosd which uses the index to verify what buttons should be enabled or disabled
            if(db.Index() == 0)
            {
                FirstButton.IsEnabled = false;
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = true;
                LastButton.IsEnabled = true;
            }

            else if(db.Index() == db.Count() - 1)
            {
                LastButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                FirstButton.IsEnabled = true;
                NextButton.IsEnabled = true;
            }

            else if(db.Index() == -1)
            {
                PreviousButton.IsEnabled = false;
                FirstButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                LastButton.IsEnabled = false;
            }

            else
            {
                FirstButton.IsEnabled = true;
                LastButton.IsEnabled = true;
                PreviousButton.IsEnabled = true;
                NextButton.IsEnabled = true;
            }
        }
    }
}
