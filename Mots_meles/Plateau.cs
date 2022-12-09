using System;
using System.ComponentModel.DataAnnotations;

namespace Mots_meles
{
    public class Plateau
    {
        private int difficulte;
        private int nbColonnes;
        private int nbLignes;
        private string[] mots;
        private char[,] tableau;



        /*
         * 
         * 
        private string[,] matriceFromMots(string[] mots, int difficulte = 1)
        {
            int conteurMots = 0;
            while(conteurMots < mots.Length)
            {
                var rand = new Random();
                int colonne = rand.Next(nbColonnes);
                int ligne = rand.Next(nbLignes);
                string direction = directions[rand.Next(directions.Length)];
            }
        }

        private void rempliN(int x, int y, string mot)
        {
            int cont = 0;
            for(int i = y; i < y+mot.Length; i++)
            {
                this.tableau[x, i] = mot[cont];
                cont += 1;
            }
        }

        private void rempliS(int x, int y, string mot)
        {
            int cont = 0;
            for(int i = y; i > y-mot.Length; i--)
            {
                this.tableau[x, i] = mot[cont];
                cont += 1;
            }
        }

        private void rempliO(int x, int y, string mot)
        {
            int cont = 0;
            for (int i = x; i > x - mot.Length; i--)
            {
                this.tableau[i, y] = mot[cont];
                cont += 1;
            }
        }

        private void rempliE(int x, int y, string mot)
        {
            int cont = 0;
            for (int i = x; i > y - mot.Length; i--)
            {
                this.tableau[i, y] = mot[cont];
                cont += 1;
            }
        }

        private string printMatrice(int[,] matrice)
        {
            string res = "";
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    res += matrice[i, j] + ",";
                }
                res += "\n";
            }
            return res;
        }
         * 
         */

        public Plateau()
        {
            //Constructeur par default avec ces mots par defauts
            //On utilise se constructeur en tant que brouillon
            string[] words = new string[] { "apple", "banana", "grapes", "strawberry", "orange", "mango", "kiwi", "plum", "pear", "apricot" };
            string[] directions = new string[] { "N", "O", "S", "E", "SO", "SE", "NO", "NE" };
            int length = 13;
            int heigth = 7;
            char[,] grid = new char[heigth, length];

            // Initialise the grid with empty spaces
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    grid[i, j] = ' ';
                }
            }

            // Randomly insert words
            Random rnd = new Random();
            for (int i = 0; i < heigth; i++)
            {
                string word = words[rnd.Next(0, 10)];
                int x = rnd.Next(0, length);
                int y = rnd.Next(0, heigth);
                int directionIndice = rnd.Next(0, 8);

                // Check if the word fits in the grid
                if (CheckWord(x, y, word, directions[directionIndice], grid, length, heigth))
                {
                    InsertWord(x, y, word, directions[directionIndice], grid);
                    Console.WriteLine("Word '{0}' starts at {1}, {2} and goes {3}", word, x, y, directions[directionIndice]);
                }
            }

            // Fill the empty spaces with random characters
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = (char)rnd.Next(97, 123); //Revoir lexplication de ca 
                    }
                }
            }

            // Print the grid
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }


        public void InsertWord(int x, int y, string word, string direction, char[,] grid)
        {
            for (int i = 0; i < word.Length; i++)
            {
                grid[x, y] = char.ToUpper(word[i]);
                switch (direction)
                {
                    // Left
                    case "E":
                        y--;
                        break;
                    // Left-Up
                    case "NE":
                        y--;
                        x--;
                        break;
                    // Up
                    case "N":
                        x--;
                        break;
                    // Right-Up
                    case "NO":
                        y++;
                        x--;
                        break;
                    // Right
                    case "O":
                        y++;
                        break;
                    // Right-Down
                    case "SO":
                        y++;
                        x++;
                        break;
                    // Down
                    case "S":
                        x++;
                        break;
                    // Left-Down
                    case "SE":
                        y--;
                        x++;
                        break;
                }
            }
        }

        public bool CheckWord(int x, int y, string word, string direction, char[,] grid, int length, int height)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (x < 0 || x > height-1 || y < 0 || y > length-1)
                {
                    return false;
                }
                if (grid[x, y] != ' ' && grid[x, y] != word[i])
                {
                    return false;
                }
                switch (direction)
                {
                    // Left
                    case "E":
                        y--;
                        break;
                    // Left-Up
                    case "NE":
                        y--;
                        x--;
                        break;
                    // Up
                    case "N":
                        x--;
                        break;
                    // Right-Up
                    case "NO":
                        y++;
                        x--;
                        break;
                    // Right
                    case "O":
                        y++;
                        break;
                    // Right-Down
                    case "SO":
                        y++;
                        x++;
                        break;
                    // Down
                    case "S":
                        x++;
                        break;
                    // Left-Down
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