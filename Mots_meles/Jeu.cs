using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Threading;

namespace Mots_meles
{
	public class Jeu
	{
        private Joueur[] tab_joueurs;
        private int diff;
        private Plateau plateau;
		public Jeu()
		{
            string langue_dic = null;
            string motSaisie;
            int ligne;
            int colonne;
            string direction;
            int nb_joueur;
            diff = 4; //difficulté initialisé à la plus forte
            int tempsSec = 20; //temps imparti de 1 minute


            //Affichage menu + saisie des caractéristiques des joueurs 
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n" + "\nBienvenue dans ce mots meles Devinci !"); 
            Console.WriteLine("Le but du jeu est de trouver tous les mots d'une grille dans le temps imparti (1 min), en indiquant le mot, son sens de lecture, et sa position.");
            Console.WriteLine("Pour plus de challenge, les grilles augmenteront de niveau a chaque tour !");
            

            do //blindage + saisie du nb de joueurs
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

            Console.WriteLine("\nSaisir une langue : \nFR : Francais\nEN : Anglais \nSi la saisie est mauvaise, le francais sera mis par defaut"); //saisie de la langue du dico
            langue_dic = Console.ReadLine();
            langue_dic = langue_dic.ToUpper();

            if(Equals(langue_dic, "FR")!=true && Equals(langue_dic, "EN")!=true)
            {
                langue_dic = "FR"; //la langue est automatiquement mise en français
            }

            Console.WriteLine("NOMBRE DE JOUEURS : " + nb_joueur); //affichage des infos saisies
            Console.WriteLine("LANGUE DU PLATEAU : " + langue_dic+"\n");

            for (int i=0; i<nb_joueur; i++) //création du tableau de joueurs avec les pseudos respectifs
            {
                Console.WriteLine("\nJOUEUR" + (i+1) + "\n" + tab_joueurs[i].ToString());
            }

            Console.Clear(); //on supprime l'affichage courant
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");


            ///LANCEMENT D UNE PARTIE
            
            for(int level=1; level<diff+1; level++) //boucle de tours
            {
                for(int j=0; j<nb_joueur; j++) //tours de joueurs
                {
                    //déclarations
                    bool res = true;
                    string motEnMoins = null;
                    DateTime startTime=DateTime.Now; //on initialise le chrono de départ à la minute à laquelle le chrono commence (now)


                    //affichage
                    Console.WriteLine("\n=-=-=-= Au tour de " + tab_joueurs[j].Nom + " =-=-=-=\n");
                    Console.WriteLine("Difficulte : " + level);

                    plateau = new Plateau(level, langue_dic); //génération d'un nouveau plateau à la fin du tour 
                    char[,] grille = plateau.Grid; //la grille du plateau est stockée dans le tableau "grille"
                    List<string> mots; 
                    mots = plateau.MotsAjoutes; //liste des mots contenus dans la grille à trouver


                    ///DEBUT DU JEU

                    while (true) //on boucle l'affichage de la grille + le jeu tant que le temps n'est pas écoulé
                    {
                        DateTime currentTime = DateTime.Now; 
                        TimeSpan duree = currentTime.Subtract(startTime); //la différence entre le temps courant et le temps de départ est stocké dans "duration"

                        Console.WriteLine("\nScore : " + tab_joueurs[j].Score); //affichage du score du joueur
                        Console.WriteLine("Mots trouves : " + tab_joueurs[j].MotsTrouvesText());//affichage des mots trouves du joueur
                        AffichagePlateau(grille, mots, level, langue_dic, motEnMoins); //affichage du plateau courant

                        Console.WriteLine("\nSaisir le mot trouve : ");
                        motSaisie = Console.ReadLine(); //saisie du mot trouvé
                        motSaisie = motSaisie.ToUpper(); //mise en majuscule du mot trouvé

                        do 
                        {
                            Console.WriteLine("Saisir la ligne : "); //blindage et saisie du numéro de la ligne
                            ligne = int.Parse(Console.ReadLine());
                        }while(ligne<0 && ligne>grille.GetLength(0));

                        do
                        {
                            Console.WriteLine("Saisir la colonne : "); //blindage et saisie du numéro de la colonne
                            colonne = int.Parse(Console.ReadLine());
                        }while (colonne < 0 && colonne > grille.GetLength(1));

                        do
                        {
                            Console.WriteLine("Saisir la direction (N,S,E,O,NE,NO,SE,SO) : "); //blindage et saisie de la direction
                            direction = Console.ReadLine();
                            direction = direction.ToUpper();
                        } while (Equals(direction, "N") != true && Equals(direction, "S") != true && Equals(direction, "E") != true && Equals(direction, "O") != true && Equals(direction, "NE") != true && Equals(direction, "NO") != true && Equals(direction, "SE") != true && Equals(direction, "S0") != true);

                        

                        res = plateau.Test_Plateau(motSaisie, ligne - 1, colonne - 1, direction); //appell de la fonction de test

                        if (duree.TotalSeconds <= tempsSec && res == true) //si la réponse est bonne et dans le temps imparti
                        {
                            tab_joueurs[j].Score += motSaisie.Length; //incrémentation du nb de lettre du mots trouvés dans le score
                            motEnMoins = motSaisie;
                            tab_joueurs[j].Add_Mot(motSaisie); //incrémentation du motSaisie dans les mots trouvés
                        }
                        else if (duree.TotalSeconds <= tempsSec && res == false) //si la saisie est fausse mais dans le temps impartie
                        {
                            Console.WriteLine("Le mot n'est pas dans la grille!"); //message d'erreur
                        }
                        else //le temps imparti est dépassé
                        {
                            break; //quitte la boucle
                        }
                        Console.Clear(); //reset l'affichage
                    }
                    Console.Clear();
                }
            }


        }

        /*public void AffichageTimer()
        {
            int seconds = tempsSec;

            for(int i=seconds; i>=0; i--)
            {
                Console.Write("\r{0} seconds remaining...", i);
                Thread.Sleep(1000);
            }

        }*/
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
            for(int i=0; i<grille.GetLength(0); i++)
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

    }
}