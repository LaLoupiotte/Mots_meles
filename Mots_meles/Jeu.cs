using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Mots_meles
{
	public class Jeu
	{
        private Joueur[] tab_joueurs;
        private int difficulte;
        private Plateau plateau;
        private Dictionnaire dictionnaire;
        private Plateau plateauCourant;
        private List<List<Plateau>> plateauPrec;

		public Jeu()
		{
            string langue_dic = null;
            string motSaisie;
            int ligne=0;
            int colonne=0;
            string direction;
            int nb_joueur=0;
            difficulte = 4; //difficult� initialis� � la plus forte
            int tempsSec = 0; //temps imparti de 1 minute
            plateauPrec = new List<List<Plateau>>();

            //Affichage menu + saisie des caract�ristiques des joueurs 
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n" + "\nBienvenue dans ce mots meles Devinci !"); 
            Console.WriteLine("Le but du jeu est de trouver tous les mots d'une grille dans le temps imparti (1 min), en indiquant le mot, son sens de lecture, et sa position.");
            Console.WriteLine("Pour plus de challenge, les grilles augmenteront de niveau a chaque tour !");

            do //blindage + saisie du nb de joueurs
            {
                Console.WriteLine("\nInserer le nombre de joueurs");
                try
                {
                    nb_joueur=int.Parse(Console.ReadLine());
                }
                catch(FormatException) //blindage si la saisie n'est pas un entier
                {
                    Console.WriteLine("Erreur de saisie.");
                }
            }while (nb_joueur < 2); //si la saisie est un entier hors des bornes

            tab_joueurs = new Joueur[nb_joueur]; //cr�ation du tableau de joueur avec une taille �gale au nb de joueurs


            for (int i = 0; i < nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\nSaisir le nom du joueur " + (i + 1));
                tab_joueurs[i] = new Joueur(Console.ReadLine());
            }

            do
            {
                Console.WriteLine("\nSaisir le temps imparti en secondes : ");
                try
                {
                    tempsSec = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur de saisie.");
                }
            } while (tempsSec < 0 || tempsSec == 0);
            

            Console.WriteLine("\nSaisir une langue : \nFR : Francais\nEN : Anglais \nSi la saisie est mauvaise, le francais sera mis par defaut"); //saisie de la langue du dico
            langue_dic = Console.ReadLine();
            langue_dic = langue_dic.ToUpper();

            if(Equals(langue_dic, "FR")!=true && Equals(langue_dic, "EN")!=true)
            {
                langue_dic = "FR"; //la langue est automatiquement mise en fran�ais si la saisie est incorrecte
            }


            Console.Clear(); //on supprime l'affichage courant
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");


            ///LANCEMENT D UNE PARTIE
            
            for(int level=1; level<difficulte+1; level++) //boucle de tours
            {
                for(int j=0; j<nb_joueur; j++) //tours de joueurs
                {
                    if (level == 1)
                    {
                        plateauPrec.Add(new List<Plateau>());
                    }
                    //d�clarations
                    bool res = true;
                    string motEnMoins = null;
                    DateTime startTime=DateTime.Now; //on initialise le chrono de d�part � la minute � laquelle le chrono commence (now)

                    plateauCourant = null;
                    
                    //affichage
                    Console.WriteLine("\n=-=-=-= Au tour de " + tab_joueurs[j].Nom + " =-=-=-=\n");
                    Console.WriteLine("Difficulte : " + level);

                    plateau = new Plateau(level, langue_dic); //g�n�ration d'un nouveau plateau � la fin du tour 
                    char[,] grille = plateau.Grid; //la grille du plateau est stock�e dans le tableau "grille"

                    plateauCourant = plateau; //le nouveau plateau g�n�r� est 
                    plateauPrec[j].Add(plateau);
                    

                    List<string> mots; 
                    mots = plateau.MotsAjoutes; //liste des mots contenus dans la grille � trouver


                    ///DEBUT DU JEU

                    while (true) //on boucle l'affichage de la grille + le jeu tant que le temps n'est pas �coul�
                    {
                        DateTime currentTime = DateTime.Now; 
                        TimeSpan duree = currentTime.Subtract(startTime); //la diff�rence entre le temps courant et le temps de d�part est stock� dans "duration"
                        int tempsRest = (int)duree.TotalSeconds;

                        Console.WriteLine("\nTemps ecoule : " + tempsRest + " secondes");
                        Console.WriteLine("\nScore : " + tab_joueurs[j].Score); //affichage du score du joueur
                        Console.WriteLine("Mots trouves : " + tab_joueurs[j].MotsTrouvesText());//affichage des mots trouves du joueur
                        AffichagePlateau(grille, mots, level, langue_dic, motEnMoins); //affichage du plateau courant

                        Console.WriteLine("\nSaisir le mot trouve : ");
                        motSaisie = Console.ReadLine(); //saisie du mot trouv�
                        motSaisie = motSaisie.ToUpper(); //mise en majuscule du mot trouv�

                        do 
                        {
                            Console.WriteLine("Saisir la ligne : "); //blindage et saisie du num�ro de la ligne
                            try
                            {
                                ligne = int.Parse(Console.ReadLine());
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("Erreur de saisie ! ");
                            }
                        }while(ligne<0 && ligne>grille.GetLength(0));

                        do
                        {
                            Console.WriteLine("Saisir la colonne : "); //blindage et saisie du num�ro de la colonne
                            try
                            {
                                colonne = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Erreur de saisie ! ");
                            }
                        } while (colonne < 0 && colonne > grille.GetLength(1));


                        do
                        {
                            Console.WriteLine("Saisir la direction (N,S,E,O,NE,NO,SE,SO) : "); //blindage et saisie de la direction
                            direction = Console.ReadLine();
                            direction = direction.ToUpper();
                        } while (Equals(direction, "N") != true && Equals(direction, "S") != true && Equals(direction, "E") != true && Equals(direction, "O") != true && Equals(direction, "NE") != true && Equals(direction, "NO") != true && Equals(direction, "SE") != true && Equals(direction, "S0") != true);

                        
                        res = plateau.Test_Plateau(motSaisie, ligne - 1, colonne - 1, direction); //appell de la fonction de test

                        if (duree.TotalSeconds <= tempsSec && res == true) //si la r�ponse est bonne et dans le temps imparti
                        {
                            tab_joueurs[j].Score += motSaisie.Length; //incr�mentation du nb de lettre du mots trouv�s dans le score
                            motEnMoins = motSaisie;
                            tab_joueurs[j].Add_Mot(motSaisie); //incr�mentation du motSaisie dans les mots trouv�s
                        }
                        else if (duree.TotalSeconds <= tempsSec && res == false) //si la saisie est fausse mais dans le temps impartie
                        {
                            Console.WriteLine("Le mot n'est pas dans la grille!"); //message d'erreur
                        }
                        else //le temps imparti est d�pass�
                        {
                            break; //quitte la boucle
                        }
                        Console.Clear(); //reset l'affichage
                    }
                    Console.Clear();
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

            Console.Write("     "); //affichage indices + cadre brouillon
            for(int i=0; i<grille.GetLength(1); i++)
            {
                if ((i + 1) > 9) //gestion de l'espace d'affichage pour les nb � 2 chiffres
                {
                    Console.Write(" " + (i + 1));
                }
                else
                {
                    Console.Write(" " + (i + 1) + " ");
                }
            }

            Console.WriteLine();

            for (int i = 0; i < grille.GetLength(0); i++) //affichage de la grille
            {
                if ((i + 1) > 9) //gestion de l'espace d'affichage 
                {
                    Console.Write(" " + (i + 1) + " | ");
                }
                else
                {
                    Console.Write(" " + (i + 1) + "  | ");
                }

                for (int k = 0; k < grille.GetLength(1); k++)
                {
                    Console.Write(grille[i, k] + "  ");
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }

    }
}