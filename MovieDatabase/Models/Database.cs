using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieDatabase.Models
{
    public class Database
    {
        private List<Movie> db; //list of movies currently stored in the database
        private int _index; //variable used to state the position of the movie in the database
       
        public Database()
        {
            //initialise a new database and set the index to -1
            db = new List<Movie>();
            //-1 indicates there are no movies stored
            _index = -1;
 
        } // A property to Return number of movies in the database
        public int Count()
        {
            //returns the number of movies stored using the count m
            return db.Count();
        }

        // A property to return  current _index position which should be either
        // -1 if database is empty
        // 0 - db.Count-1 if database is not empty
        public int Index()
        {
            
           if(db.Any())
            {
                //returns the index position
                return _index;

            } else
            {
                //returns -1 to indicate that the database is empty
                return -1;
            }
        } 

        // Add a movie to current position in database
        public void Add(Movie m)
        {
            //add a movie to the position and increment the index by +1
            db.Add(m);
            _index++;

        }

        // Return current movie or null if database empty
        public Movie Get()
        {
            
            //check the database to see if a movie exists at the current position, otherwise return null
               if(db.Count > 0)
            {
                return db[_index];          
            }
            else
            {
                //no movie exists at that index so return null
                return null;
            }
            
        }

        // Delete current movie at index if there is a movie and update index 
        public void Delete()
        {
            //remove the movie at the selected index
           if((_index >= db.Count() - 1) && db.Count >= 0)
            {
                //remove the film by passing in the index position
                db.RemoveAt(_index);
                //decrement the index to verify that a movie has been deleted
                _index--;
            }
        }

        // Update the current movie at index if there is a movie and update index
        public void Update(Movie m)
        {
            if(db.Count >= 0)
            {
                //update the movie at the index using the movie that is passed in
                db[_index] = m;
                _index++;
            }
        }

        // Delete all movies from the database and reset index
        public void Clear()
        {
            //method to wipe the database and remove any movies, reset the index back to -1
            db.Clear();
            //reset index to -1 to verify that no movies exist
            _index = -1;
        }
        

        // Move index position to first movie (0)
        // return true if index update was possible, false otherwise
        public bool First()
        {
           if(_index != -1 && db.Count() >= 1) 
            {
                //set index to 0 as it represents the first element
                _index = 0;
                db.ElementAt(0);
                return true;
            } else
            {
                return false;
            }
        }

        // Move index position to last movie
        // true if index update was possible, false otherwise</returns>
        public bool Last()
        {
            if (_index != -1 && db.Count() >= 1)
            {
                //index = no of movies -1 as index starts at 0
                _index = db.Count() - 1;
                db.ElementAt(_index);
                return true;
            } else
            {
                return false;
            }
        }

        // Move index position to next movie
        // true if index update was possible, false otherwise<
        public bool Next()
        {

            if(db.Count > 0 && _index < db.Count -1)
            {
                //increment index to show that you have movied to next movie
                _index++;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Move index position to previous movie
        // true if index update was possible, false otherwise
        public bool Prev()
        {
            if(db.Count > 0 && _index > 0)
            {
                //decrement index to show you have moved to previous movie
                _index--;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Load movies from a json file and set index to first record
        public void Load(string file)
        {
            string load = File.ReadAllText(file);

            if(new FileInfo(file).Length !=0)
            {
                var databaseSaveFile = JsonConvert.DeserializeObject<List<Movie>>(load);
                db = databaseSaveFile;
               _index = 0;
            }  
        }

        // Save movies to a Json file
        public void Save(string file)
        {
            var databaseSaveFile = file;
            string json = JsonConvert.SerializeObject(db.ToList());
            File.WriteAllText(file, json);
        }

        // Following methods update the List of movies (db) to the specified order

        // order the database by year of movie
        public void OrderByYear()
        {
            db = (from Movie in db
                         orderby Movie.Year ascending
                         //order the movies in a list
                         select Movie).ToList<Movie>();     
        }

        // order the database by title of movie (ascending)
        public void OrderByTitle()
        {
            db = (from Movie in db
                         orderby Movie.Title ascending
                        //order the movies in a list
                         select Movie).ToList<Movie>();
            
        }

        // order the database by budget of movie (ascending)
        public void OrderByDuration()
        {

            db = (from Movie in db
                         orderby Movie.Duration ascending
                         //order the movies in a list
                         select Movie).ToList<Movie>();

        }
    }
}
