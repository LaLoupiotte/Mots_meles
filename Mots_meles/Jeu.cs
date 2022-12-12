using System;
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

            //Affichage menu + saisie des caractéristiques des joueurs 
            int nb_joueur;
            diff = 0; //remise à 0 de la difficulté

            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");
            do
            {
                Console.WriteLine("\nInserer le nombre de joueurs ");
                nb_joueur = int.Parse(Console.ReadLine()); //saisie par le joueur du nb de joueurs
            }while (nb_joueur < 0 || nb_joueur == 0);

            tab_joueurs = new Joueur[nb_joueur];


            for (int i = 0; i < nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\nSaisir le nom du joueur " + (i + 1));

                tab_joueurs[i] = new Joueur(Console.ReadLine());
            }

            Console.WriteLine("\nSaisir une langue : \nFR : Francais\nAN : Anglais"); //saisie de la langue du dico
            langue_dic = Console.ReadLine();

            Console.WriteLine("NOMBRE DE JOUEURS : " + nb_joueur);
            Console.WriteLine("LANGUE DU PLATEAU : " + langue_dic);

            for (int i=0; i<nb_joueur; i++)
            {
                Console.WriteLine("\nJOUEUR" + (i+1) + "\n" + tab_joueurs[i].ToString());

            }


            ///LANCEMENT D UNE PARTIE
            ///
            //plateau = new Plateau(diff, langue_dic);


        }
    }
}