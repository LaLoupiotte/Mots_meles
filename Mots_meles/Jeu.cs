using System;
namespace Mots_meles
{
	public class Jeu
	{
        private Joueur[] tab_joueurs;
        private string langue_dic;
		public Jeu()
		{

            string langue_dic = null;
            string nom_temp = null;

            //Affichage menu + saisie des caractéristiques des joueurs 
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");
            int nb_joueur;
            do
            {
                Console.WriteLine("\nInserer le nombre de joueurs ");
                nb_joueur = int.Parse(Console.ReadLine()); //saisie par le joueur du nb de joueurs
            }while (nb_joueur < 0 || nb_joueur == 0);

            tab_joueurs = new Joueur[nb_joueur];

            Console.WriteLine("Vous avez selectionne " + nb_joueur + " joueurs");

            for (int i = 0; i < nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\nSaisir le nom du joueur " + (i + 1));

                tab_joueurs[i] = new Joueur(Console.ReadLine());
                Console.WriteLine(tab_joueurs[i].ToString());
            }

            Console.WriteLine("Saisir une langue : \nFR : Francais\nAN : Anglais"); //saisie de la langue du dico
            langue_dic = Console.ReadLine();


            ///LANCEMENT D UNE PARTIE
            ///
        }
    }
}