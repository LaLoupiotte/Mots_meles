using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.Emit;

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
            

            for(int level=1; level<diff+1; level++) //boucle de tours
            {
                for(int j=0; j<nb_joueur; j++) //boucle de joueurs
                {
                    //déclarations
                    bool res = true;
                    string motEnMoins = null;

                    //affichage
                    Console.WriteLine("\n=-=-=-= Au tour de " + tab_joueurs[j].Nom + " =-=-=-=\n");
                    Console.WriteLine("Difficulte : " + level);

                    plateau = new Plateau(level, langue_dic);
                    char[,] grille = plateau.Grid;
                    List<string> mots;
                    mots = plateau.MotsAjoutes; //liste des mots contenus dans la grille à trouver
                   
                    
                    ///DEBUT DU JEU
                    //insérer le timer ici ----- les instructions après s'executent en boucle jusqu'a ce que le timer soit ecoule
                    
                    for (int h = 0; h < 3; h++) //test, à retirer et remplacer par timer
                    {
                        Console.WriteLine("Score : " + tab_joueurs[j].Score);
                        AffichagePlateau(grille, mots, level, langue_dic, motEnMoins);

                        Console.WriteLine("\nSaisir le mot trouve : ");
                        motSaisie = Console.ReadLine();
                        motSaisie = motSaisie.ToUpper();
                        Console.WriteLine("Saisir la ligne : ");
                        ligne = int.Parse(Console.ReadLine());
                        Console.WriteLine("Saisir la colonne : ");
                        colonne = int.Parse(Console.ReadLine());
                        Console.WriteLine("Saisir la direction (N,S,E,O,NE,NO,SE,SO) : ");
                        direction = Console.ReadLine();

                        res = plateau.Test_Plateau(motSaisie, ligne - 1, colonne - 1, direction);

                        if (res == true)
                        {
                            tab_joueurs[j].Score += motSaisie.Length;
                            motEnMoins = motSaisie;
                            tab_joueurs[j].Add_Mot(motSaisie); //incrémentation du motSaisie dans les mots trouvés
                        }
                        else if (res == false)
                        {
                            Console.WriteLine("Le mot n'est pas dans la grille!");
                        }
                        Console.Clear();
                    }
                    //fin du timer ici -------
                    //afficher ca pendant 5 sec si possible, ou mettre dans la fonction timer avec 5 sec de + pour éviter de clear trop vite
                    Console.WriteLine("Temps ecoule !");
                    Console.WriteLine("Score : " + tab_joueurs[j].Score);
                    Console.WriteLine("Mots trouves : " + tab_joueurs[j].MotsTrouvesText());




                }
            }


        }

        
        public void AffichagePlateau(char[,] grille, List<string> mots, int level, string langue_disc, string motTrouve)
        {
            
            if(motTrouve!=null)
            {
                mots.Remove(motTrouve);
            }

            Console.WriteLine("Mots a trouver : ");
            foreach (string item in mots) //affichage de la liste de mots
            {
                Console.Write(item + "-");
            }
            Console.WriteLine("\n");

            Console.Write("    "); //affichage indices + cadre brouillon
            for(int i=0; i<grille.GetLength(0)-1; i++)
            {
                Console.Write(" " + (i+1) + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < grille.GetLength(0); i++) //affichage de la grille
            {
                Console.Write(" " + (i+1) + " | ");

                for (int k = 0; k < grille.GetLength(1); k++)
                {
                    Console.Write(grille[i, k] + "  ");
                }
                Console.Write("|");
                Console.WriteLine();
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