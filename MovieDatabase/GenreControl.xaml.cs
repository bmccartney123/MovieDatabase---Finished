using System;
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

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for GenreControl.xaml
    /// </summary>
    public partial class GenreControl : UserControl
    {
        public GenreControl()
        {
            InitializeComponent();
        }

        public List<Models.Genre> Selected
        {
            get
            {
                var genre = new List<Models.Genre>();
                if (Comedy.IsChecked.Value)
                    genre.Add(Models.Genre.Comedy);
                if (Romance.IsChecked.Value)
                    genre.Add(Models.Genre.Romance);
                if (Action.IsChecked.Value)
                    genre.Add(Models.Genre.Action);
                if (Thriller.IsChecked.Value)
                    genre.Add(Models.Genre.Thriller);
                if (Family.IsChecked.Value)
                    genre.Add(Models.Genre.Family);
                if (Horror.IsChecked.Value)
                    genre.Add(Models.Genre.Horror);
                if (Western.IsChecked.Value)
                    genre.Add(Models.Genre.Western);
                if (SciFi.IsChecked.Value)
                    genre.Add(Models.Genre.SciFi);
                if (War.IsChecked.Value)
                    genre.Add(Models.Genre.War);

                return genre;
            }
            set
            {
                Comedy.IsChecked = value.Contains(Models.Genre.Comedy) ? true : false;
                Romance.IsChecked = value.Contains(Models.Genre.Romance) ? true : false;
                Action.IsChecked = value.Contains(Models.Genre.Action) ? true : false;
                Thriller.IsChecked = value.Contains(Models.Genre.Thriller) ? true : false;
                Family.IsChecked = value.Contains(Models.Genre.Family) ? true : false;
                Horror.IsChecked = value.Contains(Models.Genre.Horror) ? true : false;
                Western.IsChecked = value.Contains(Models.Genre.Western) ? true : false;
                SciFi.IsChecked = value.Contains(Models.Genre.SciFi) ? true : false;
                War.IsChecked = value.Contains(Models.Genre.War) ? true : false;
            }

        }
        public void Clear()
        {
            Comedy.IsChecked = false;
            Romance.IsChecked = false;
            Action.IsChecked = false;
            Thriller.IsChecked = false;
            Family.IsChecked = false;
            Horror.IsChecked = false;
            Western.IsChecked = false;
            SciFi.IsChecked = false;
            War.IsChecked = false;
        }
    }
}
