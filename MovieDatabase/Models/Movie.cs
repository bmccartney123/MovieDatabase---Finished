using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Models
{

    public enum Genre { Comedy, Romance, Action, Thriller, Family, Horror, Western, SciFi, War };
    public class Movie
    {
        //define the attributes of the movie class
        public string Title { get; set; } 
        public int Year { get; set; } 
        public string Director { get; set; } 
        public int Duration { get; set; } 
        public double Budget { get; set; } 
        public string URL { get; set; } 
        public int Rating { get; set; } 
        public List<Genre> Genres { get; set; } //list used to store all genres of a film
        public List<string> Actors { get; set; } //list used to store the names of all actors of a film


        // constructor - initialise all variables as blank or 0
        public Movie()
        {
            Title = "";
            Director = "";
            URL = "";
            Year = 0;
            Duration = 0;
            Budget =0;
            Rating = 0;
            Genres = new List<Genre>();
            Actors = new List<string>();
        }
    }
}




