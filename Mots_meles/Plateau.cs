using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections;

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

namespace Mots_meles
{
    public class Plateau
    {
        private int difficulte;
        private string[] mots;
        private char[,] grid;
        private string langue;
        private string[] directions;
        private int width;
        private int heigth;

        public Plateau(int difficulte, string langue)
        {
            this.difficulte = difficulte;
            this.langue = langue;
            
            switch (difficulte)
            {
                case 1:
                    this.directions = new string[] { "S", "O" };
                    this.heigth = 7;
                    this.width = 13;
                    break;
                case 2:
                    this.directions = new string[] { "S", "O", "N", "E" };
                    this.heigth = 7;
                    this.width = 13;
                    break;
                case 3:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE" };
                    this.heigth = 7;
                    this.width = 13;
                    break;
                case 4:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 7;
                    this.width = 13;
                    break;
            }
            char[,] grid = new char[this.heigth, this.width];

            

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
            while(cont < 10)
            {
                string[] words = ReadFile("../../../wordFiles/MotsPossiblesFR.txt", difficulte);
                string word = words[rnd.Next(0, words.Length-1)];
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

            // Print the grid
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }


        public static string[] ReadFile(string filePath, int difficulte)
        {
            bool flag = false;
            ArrayList words = new ArrayList();
            try
            {
                string text = File.ReadAllText(filePath);
                words.AddRange(text.Split(' '));
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            int start = words.IndexOf(difficulte);
            int end;
            string[] wordsRes = new string[words.Count];
            if(difficulte == 4)
            {
                end = words.Count - 1;
            }
            else
            {
                end = words.IndexOf(difficulte + 1);
            }
            for(int i = 0; i < words.Count; i++)
            {
                wordsRes[i] = (string)words[i];
            }
            
            return wordsRes;
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