using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mots_meles
{
    public class Plateau
    {
        private int difficulte;
        private char[,] grid;
        private string langue;
        private string[] directions;
        private string filename;
        private int width;
        private int nombreMots;
        private int heigth;
        private List<string> words;
        private List<string> motsAjoutes;
        public char[,] Grid
        {
            get { return this.grid; }
        }

        public List<string> MotsAjoutes
        {
            get { return this.motsAjoutes; }
        }

        public Plateau(string filename)
        {
            //Ce constructeur permet de créer une instance de plateau en utilisant la fonction ToRead;
            this.filename = filename;
            ToRead(filename);
        }

        public Plateau(int difficulte, string langue)
        {
            //Ce constructeur est utilise quand on veut generer une grille en donnant une difficulte et une langue
            this.difficulte = difficulte;
            this.langue = langue;
            this.motsAjoutes = new List<string>();
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
                    this.heigth = 6;
                    this.width = 7;
                    nombreMots = 8;
                    break;
                case 2:
                    this.directions = new string[] { "S", "O", "N", "E" };
                    this.heigth = 8;
                    this.width = 10;
                    nombreMots = 13;
                    break;
                case 3:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE" };
                    this.heigth = 10;
                    this.width = 11;
                    nombreMots = 20;
                    break;
                case 4:
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 13;
                    this.width = 13;
                    nombreMots = 28;
                    break;
                default:
                    //Par défaut nous avons décidé de mettre la valeur maximale même si ce cas la ne sera jamais traité 
                    //il permet d'éviter une erreur de compilation lors des test 
                    this.directions = new string[] { "S", "O", "N", "E", "SO", "NE", "NO", "SE" };
                    this.heigth = 13;
                    this.width = 13;
                    nombreMots = 28;
                    break;
            }
            //Création de la graille avec la bonne hauteur et largeur
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
            ToFile("tableauDebug");
        }

        //Cette fonction genere une grille en placant les mots aléatoirement
        public void GenereGrille()
        { 
            Random rnd = new Random();
            int cont = 0;
            while (this.motsAjoutes.Count < nombreMots)
            {
                string word = "";
                //Choisit un mot, une position (x, y) et une direcetions aléatoire
                //Le while nous permet de nes pas avoir un mot nul. Cela peut arriver si il y a un espace dans le fichier
                while (word.Length < 2)
                {
                    word = words[rnd.Next(0, words.Count - 1)];
                }
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
                    motsAjoutes.Add(word);
                    cont += 1;
                }
            }
            //Remplir les espaces vides avec des characteres aléatoires
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = alphabet[rnd.Next(0, alphabet.Length-1)]; 
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
                        colonne++;
                        break;
                    // Nord Est
                    case "NE":
                        colonne++;
                        ligne--;
                        break;
                    // Nord
                    case "N":
                        ligne--;
                        break;
                    // Nord Ouest
                    case "NO":
                        colonne--;
                        ligne--;
                        break;
                    // Ouest
                    case "O":
                        colonne--;
                        break;
                    // Sud Ouest
                    case "SO":
                        colonne--;
                        ligne++;
                        break;
                    // Sud
                    case "S":
                        ligne++;
                        break;
                    // Sud Est
                    case "SE":
                        colonne++;
                        ligne++;
                        break;
                }
            }
            return res;
        }

        public void ToFile(string nomfile)
        {
            string res = "" + this.difficulte + ";" + this.heigth + ";" + this.width + ";" + this.motsAjoutes.Count+"\n";
            for(int i = 0; i < this.motsAjoutes.Count-1; i++)
            {
                res+=motsAjoutes[i]+";";
            }
            res += motsAjoutes[motsAjoutes.Count - 1] + "\n";
            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1)-1; j++)
                {
                    res+=grid[i,j] + ";";
                }
                res += grid[i, grid.GetLength(1) - 1] + "\n";
            }
            string path = "./tableau/" + nomfile +".csv";
            File.WriteAllText(path, res);
        }

        public void ToRead(string nomfile)
        {
            string text = File.ReadAllText("./tableau/" + nomfile +".csv");
            string[] lines = text.Split("\n");
            string[] line1 = lines[0].Split(";");
            this.difficulte = Convert.ToInt32(line1[0]);
            this.heigth = Convert.ToInt32(line1[1]);
            this.width = Convert.ToInt32(line1[2]);
            this.nombreMots = Convert.ToInt32(line1[3]);
            this.motsAjoutes = new List<string>();
            this.motsAjoutes.AddRange(lines[1].Split(";"));
            this.grid = new char[heigth, width];
            Console.WriteLine(width + " " + heigth);
            for(int i = 0; i < heigth; i++)
            {
                string[] lineTemp = lines[i + 2].Split(";");
                for(int j = 0; j < width; j++)
                {
                    this.grid[i, j] = Convert.ToChar(lineTemp[j]);
                    Console.Write(grid[i,j]);
                }
                Console.WriteLine();
            }
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> 5f3b885fe29f36c0f6401debb44ca8f5002937cc

        public void AfficheGrille()
        {
            for(int i = 0; i < heigth; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }
<<<<<<< HEAD
>>>>>>> 5f3b885fe29f36c0f6401debb44ca8f5002937cc
=======
>>>>>>> 5f3b885fe29f36c0f6401debb44ca8f5002937cc
    }
}