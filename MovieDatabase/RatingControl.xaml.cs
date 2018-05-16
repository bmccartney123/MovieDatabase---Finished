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
    /// Interaction logic for RatingControl.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        public RatingControl()
        {
            InitializeComponent();
        }


        public int Value
        {
            get
            {
                int RatingOption;
                RatingOption = Rb1.IsChecked.Value ? RatingOption = 1 :
                Rb2.IsChecked.Value ? RatingOption = 2 :
                Rb3.IsChecked.Value ? RatingOption = 3 :
                Rb4.IsChecked.Value ? RatingOption = 4 :
                Rb5.IsChecked.Value ? RatingOption = 5 : RatingOption = 0;
                return RatingOption;
            }

            set
            {
                Rb1.IsChecked = value.Equals(1);
                Rb2.IsChecked = value.Equals(2);
                Rb3.IsChecked = value.Equals(3);
                Rb4.IsChecked = value.Equals(4);
                Rb5.IsChecked = value.Equals(5);


            }
        }

        public void Clear()
        {
            Rb1.IsChecked = false;
            Rb2.IsChecked = false;
            Rb3.IsChecked = false;
            Rb4.IsChecked = false;
            Rb5.IsChecked = false;
        }

    }
}

