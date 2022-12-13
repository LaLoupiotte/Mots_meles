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
        private List<string> words;
        private char[,] grid;
        private string langue;
        private string[] directions;
        private int width;
        private int heigth;
<<<<<<< HEAD
        private string motAleat;

=======
>>>>>>> 8c4cd58837146c3afbffa95677d2b41f829e63a8
        public char[,] Grid
        {
            get { return this.grid; }
        }
<<<<<<< HEAD

        public string MotAleat
        {
            get { return this.motAleat; }
        }

=======
>>>>>>> 8c4cd58837146c3afbffa95677d2b41f829e63a8
        public Plateau(int difficulte, string langue)
        {
            this.difficulte = difficulte;
            this.langue = langue;
            //Création d'une collection liste qui va stocker tout les mots de la langue this.langue
            words = new List<string>();
            //La boucle va de 2 à 15 car cela correspond a la taille des différents mots. Ici on les veut tous
            for (int longeur = 2; longeur < 16; longeur++)
            {
                //Ajout a la liste addrange de la liste words du dictionnaire correspodant à la longeur et a langue désirée
                words.AddRange(new Dictionnaire(longeur, langue).Words);
            }
            //On fait une switch pour affecter le bon tableau a this.directions et une taille de plus en plus grande aux grille
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
                    //Par défaut nous avons décidé de mettre la valeur maximale même si ce cas la ne sera jamais traité 
                    //il permet d'éviter une erreur de compilation lors des test 
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 16;
                    this.width = 21;
                    break;
            }
            //Création de la grille avec la bonne hauteur et largeur
            this.grid = new char[this.heigth, this.width];
            //Initialisation de la grille avec des espaces vides
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = ' ';
                }
            }

            GenereGrille();
            Console.Write("Saisir mot : ");
            string mot = Console.ReadLine();
            Console.Write("Saisir x : ");
            int x = Int32.Parse(Console.ReadLine());
            Console.Write("Saisir y : ");
            int y = Int32.Parse(Console.ReadLine());
            Console.Write("Saisir direction : ");
            string direciton = Console.ReadLine();
            Console.WriteLine(Test_Plateau(mot, x, y, direciton));

        }
        public void GenereGrille()
        {
            // Randomly insert words
            Random rnd = new Random();
            int cont = 0;
            while (cont < 10)
            {
                //Choisit un mot, une position (x, y) et une direcetions aléatoire
                string word = words[rnd.Next(0, words.Count - 1)];
                int x = rnd.Next(0, width);
                int y = rnd.Next(0, heigth);
                //Ici on génère aléatoirement un nombre en fonction de la longeur du tableau directions car il change
                //en fonction de la difficulté
                int directionIndice = rnd.Next(0, directions.Length);
                //Teste si le mot peut etre inserre dans la grille
                //J'utilise cette méthode pour ne pas avoir a tout supprimer si disons le 5eme lettre ne rentre pas.
                if (Disponible(x, y, word, this.directions[directionIndice], grid, width, heigth))
                {
                    //Si disponible alors on inserre le mot
                    Inserre(x, y, word, this.directions[directionIndice], grid);
                    Console.WriteLine("Word '{0}' starts at {1}, {2} and goes {3}", word, x, y, directions[directionIndice]);
                    cont += 1;

                    motAleat += word + "\n";
                }
            }
            //Remplir les espaces vides avec des characteres aléatoires
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
        public void Inserre(int x, int y, string word, string direction, char[,] grid)
        {
            //Boucle qui traite chaque charactere du mot
            for (int i = 0; i < word.Length; i++)
            {
                //On remplit la case par le charactere souhaité
                grid[x, y] = char.ToUpper(word[i]);
                //On fait un switch qui en fonction de la direction change les valeurs des coordonnées (x, y)
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
        public bool Disponible(int x, int y, string word, string direction, char[,] grid, int width, int height)
        {
            //Boucle qui traite chaque charactere du mot
            for (int i = 0; i < word.Length; i++)
            {
                //Ce if nous permet de savoir si le mot rentre dans la grille
                if (x < 0 || x > height - 1 || y < 0 || y > width - 1)
                {
                    return false;
                }
                //Ce else if nous permet de savoir si la position dans la grille est vide afin de la remplacer par le 
                //charactere word[i]. Si la case est occupé par le même charactere que word[i] la condition reste True
                //car sinon les mots ne peuvent pas se "croisés" dans la grille 
                else if (grid[x, y] != ' ' && grid[x, y] != word[i])
                {
                    return false;
                }
                //On fait un switch qui en fonction de la direction change les valeurs des coordonnées (x, y)
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
        public bool Test_Plateau(string mot, int ligne, int colonne, string direction)
        {

            bool res = true;


            //Boucle qui traite chaque charactere du mot
            for (int i = 0; i < mot.Length; i++)
            {
                //Ce if nous permet de savoir si le mot rentre dans la grille
                if (ligne < 0 || ligne > this.grid.GetLength(0) - 1 || colonne < 0 || colonne > this.grid.GetLength(1) - 1)
                {
                    return false;
                }
                //Test si le charactere mot[i] correspond bien au charactere dans le grille ayant pour coordonées (x, y)
                else if (grid[ligne, colonne] != mot[i])
                {
                    res = false;
                }
                //On fait un switch qui en fonction de la direction change les valeurs des coordonnées (x, y)
                switch (direction)
                {
                    // Est
                    case "E":
                        colonne--;
                        break;
                    // Nord Est
                    case "NE":
                        colonne--;
                        ligne--;
                        break;
                    // Nord
                    case "N":
                        ligne--;
                        break;
                    // Nord Ouest
                    case "NO":
                        colonne++;
                        ligne--;
                        break;
                    // Ouest
                    case "O":
                        colonne++;
                        break;
                    // Sud Ouest
                    case "SO":
                        colonne++;
                        ligne++;
                        break;
                    // Sud
                    case "S":
                        ligne++;
                        break;
                    // Sud Est
                    case "SE":
                        colonne--;
                        ligne++;
                        break;
                }
            }
            return res;
        }

    }
}