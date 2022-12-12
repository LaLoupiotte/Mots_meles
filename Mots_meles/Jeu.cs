using System;
namespace Mots_meles
{
	public class Jeu
	{
		public Jeu()
		{
            //déclarations
            int nb_joueur = 0;
            Joueur[] tab_joueurs = new Joueur[nb_joueur];
            string langue_dic = null;
            string nom_temp = null;

            //Affichage menu + saisie des caractéristiques des joueurs 
            Console.WriteLine("=-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");
            do
            {
                Console.WriteLine("\nInserer le nombre de joueurs ");
                nb_joueur = int.Parse(Console.ReadLine()); //saisie par le joueur du nb de joueurs
            } while (nb_joueur < 0 || nb_joueur == 0);

            Console.WriteLine("Vous avez selectionne " + nb_joueur + " joueurs");

            for (int i = 0; i < nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\nSaisir le nom du joueur " + (i + 1));
                nom_temp = Console.ReadLine();
                Console.WriteLine(nom_temp);

                //tab_joueurs[i] = new Joueur(nom_temp);
            }

            do
            {
                Console.WriteLine("Saisir une langue : \nFR : Francais\nAN : Anglais"); //saisie de la langue du dico
                langue_dic = Console.ReadLine();
            } while (langue_dic != "FR" || langue_dic != "AN");



            ///LANCEMENT D UNE PARTIE
            ///
        }
    }
}