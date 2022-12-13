using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Mots_meles
{
	public class Jeu
	{
        private Joueur[] tab_joueurs;
        private string langue_dic;
        private int diff;
        private Plateau plateau;
		public Jeu()
		{

            string langue_dic = null;
            string nom_temp = null;
            string motSaisie;
            int ligne;
            int colonne;
            string direction;

            //Affichage menu + saisie des caractéristiques des joueurs 
            int nb_joueur;
            diff = 4; //difficulté initialisé à la plus forte

            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n" + "\nBienvenue dans ce mots meles Devinci !"); 
            Console.WriteLine("Le but du jeu est de trouver tous les mots d'une grille dans le temps imparti ( min), en indiquant le mot, son sens de lecture, et sa position.");
            Console.WriteLine("Pour plus de challenge, les grilles augmenteront de niveau a chaque tour :)");
            do
            {
                Console.WriteLine("\nInserer le nombre de joueurs");
                nb_joueur = int.Parse(Console.ReadLine()); //saisie par le joueur du nb de joueurs
            }while (nb_joueur < 0 || nb_joueur == 0);

            tab_joueurs = new Joueur[nb_joueur];


            for (int i = 0; i < nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\nSaisir le nom du joueur " + (i + 1));

                tab_joueurs[i] = new Joueur(Console.ReadLine());
            }

            Console.WriteLine("\nSaisir une langue : \nFR : Francais\nEN : Anglais"); //saisie de la langue du dico
            langue_dic = Console.ReadLine();

            Console.WriteLine("NOMBRE DE JOUEURS : " + nb_joueur);
            Console.WriteLine("LANGUE DU PLATEAU : " + langue_dic+"\n");

            for (int i=0; i<nb_joueur; i++)
            {
                Console.WriteLine("\nJOUEUR" + (i+1) + "\n" + tab_joueurs[i].ToString());

            }

            Console.Clear();
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");

            ///LANCEMENT D UNE PARTIE
            ///
            //plateau = new Plateau(diff, langue_dic);
           // char[,] grille = plateau.Grid; //génération d'une grille selon la difficulté et la langue 
           // List<string> mots; 
            //mots = plateau.MotsAjoutes; //liste des mots contenus dans la grille à trouver

            //char[][,] grilles = new char[5][,]; //tab de 5 plateaux


            for(int level=1; level<diff+1; level++) //boucle de tours
            {
                for(int j=0; j<nb_joueur; j++) //boucle de joueurs
                {
                    plateau = new Plateau(level, langue_dic);
                    char[,] grille = plateau.Grid; //génération d'une grille selon la difficulté et la langue 
                    List<string> mots;
                    mots = plateau.MotsAjoutes; //liste des mots contenus dans la grille à trouver

                    Console.WriteLine("Au tour de " + tab_joueurs[j].Nom + "\n");
                    Console.WriteLine("Difficulte : " + level);
                    Console.WriteLine("Mots a trouver : ");

                    foreach (string item in mots) //affichage de la liste de mots
                    {
                        Console.Write(item + "-");
                    }
                    Console.WriteLine();


                    for (int i = 0; i < grille.GetLength(0); i++) //affichage de la grille
                    {
                        for (int k = 0; k < grille.GetLength(1); k++)
                        {
                            Console.Write(grille[i, k] + " ");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Saisir le mot trouve : ");
                    motSaisie = Console.ReadLine();
                    Console.WriteLine("Saisir la ligne : ");
                    ligne = int.Parse(Console.ReadLine());
                    Console.WriteLine("Saisir la colonne : ");
                    colonne = int.Parse(Console.ReadLine());
                    Console.WriteLine("Saisir la direction (N,S,E,O,NE,NO,SE,SO) : ");
                    direction = Console.ReadLine();
                    

                }
            }



            /* Affichage D'UN tableau
            for (int i=0; i<grille.GetLength(0); i++)
            {
                for(int j=0; j < grille.GetLength(1); j++)
                {
                    Console.Write(grille[i, j] + " ");
                }
            }*/

            /*int cont = 0;
            for(int i=0; i<grilles.Length; i++)
            {
                if (cont <= 4) { cont += 1; }
                grilles[i] = new Plateau(cont, "EN").Grid;
            }/*
            /*
            for(int i = 0; i<grilles.Length; i++)
            {
                for(int j = 0; j < grilles[i].GetLength(0); j++)
                {

                    for(int k = 0; k < grilles[i].GetLength(1); k++)
                    {
                        Console.Write(grilles[i][j, k]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("-------------");
            }*/

            //AFFICHAGE LISTE DES MOTS
            /*
            Console.WriteLine("____");
            foreach (string item in mots)
            {
                Console.WriteLine(item);
            }*/
        }
    }
}