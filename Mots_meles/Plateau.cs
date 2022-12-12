using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mots_meles
{
    public class Plateau
    {
        private int difficulte;
        private string[] words;
        private char[,] grid;
        private string langue;
        private string[] directions;
        private int width;
        private int heigth;

        public char[,] Grid
        {
            get { return this.grid; }
        }

        public Plateau(int difficulte, string langue)
        {
            this.difficulte = difficulte;
            this.langue = langue;
            List<string> words = new List<string>();
            for (int i = 2; i < 15; i++)
            {
                words.AddRange(new Dictionnaire(i, langue).Words);
            }
            
            switch (difficulte)
            {
                case 1:
                    this.directions = new string[] { "S", "O" };
                    this.heigth = 7;
                    this.width = 13;
                    break;
                case 2:
                    this.directions = new string[] { "S", "O", "N", "E" };
                    this.heigth = 10;
                    this.width = 16;
                    break;
                case 3:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE" };
                    this.heigth = 13;
                    this.width = 18;
                    break;
                case 4:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 16;
                    this.width = 21;
                    break;
                default:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 16;
                    this.width = 21;
                    break;
            }
            this.grid = new char[this.heigth, this.width];

            

            // Initialise the grid with empty spaces
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = ' ';
                }
            }

            // Randomly insert words
            Random rnd = new Random();
            int cont = 0;
            while (cont < 10)
            {
                
                string word = words[rnd.Next(0, words.Count-1)];
                int x = rnd.Next(0, width);
                int y = rnd.Next(0, heigth);
                int directionIndice = rnd.Next(0, directions.Length);

                // Check if the word fits in the grid
                if (CheckWord(x, y, word, this.directions[directionIndice], grid, width, heigth))
                {
                    InsertWord(x, y, word, this.directions[directionIndice], grid);
                    Console.WriteLine("Word '{0}' starts at {1}, {2} and goes {3}", word, x, y, directions[directionIndice]);
                    cont += 1;
                }

            }

            // Fill the empty spaces with random characters
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = (char)rnd.Next(97, 123); //Revoir lexplication de ca 
                    }
                }
            }

        }

        public void InsertWord(int x, int y, string word, string direction, char[,] grid)
        {
            for (int i = 0; i < word.Length; i++)
            {
                grid[x, y] = char.ToUpper(word[i]);
                switch (direction)
                {
                    // Est
                    case "E":
                        y--;
                        break;
                    // Nord Est
                    case "NE":
                        y--;
                        x--;
                        break;
                    // Nord
                    case "N":
                        x--;
                        break;
                    // Nord Ouest
                    case "NO":
                        y++;
                        x--;
                        break;
                    // Ouest
                    case "O":
                        y++;
                        break;
                    // Sud Ouest
                    case "SO":
                        y++;
                        x++;
                        break;
                    // Sud
                    case "S":
                        x++;
                        break;
                    // Sud Est
                    case "SE":
                        y--;
                        x++;
                        break;
                }
            }
        }

        public bool CheckWord(int x, int y, string word, string direction, char[,] grid, int width, int height)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (x < 0 || x > height-1 || y < 0 || y > width-1)
                {
                    return false;
                }
                if (grid[x, y] != ' ' && grid[x, y] != word[i])
                {
                    return false;
                }
                switch (direction)
                {
                    // Est
                    case "E":
                        y--;
                        break;
                    // Nord Est
                    case "NE":
                        y--;
                        x--;
                        break;
                    // Nord
                    case "N":
                        x--;
                        break;
                    // Nord Ouest
                    case "NO":
                        y++;
                        x--;
                        break;
                    // Ouest
                    case "O":
                        y++;
                        break;
                    // Sud Ouest
                    case "SO":
                        y++;
                        x++;
                        break;
                    // Sud
                    case "S":
                        x++;
                        break;
                    // Sud Est
                    case "SE":
                        y--;
                        x++;
                        break;
                }
            }
            return true;
        }
    }
}