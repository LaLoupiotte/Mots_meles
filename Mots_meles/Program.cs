using System;

namespace Mots_meles
{
    class Program
    {
        static void Main(string[] args)
        {
            //déclarations
            int nb_joueur=0;
            Joueur[] tab_joueurs;
            string langue_dic;

            //Affichage menu + saisie des caractéristiques des joueurs 
            Console.WriteLine(" =-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");
            do { 
                Console.WriteLine("\n Inserer le nombre de joueurs ");
                nb_joueur=int.Parse(Console.ReadLine()); //saisie par le joueur du nb de joueurs
            }while(nb_joueur<0||nb_joueur==0);

            Console.WriteLine("\n Vous avez selectionne " + nb_joueur + "joueurs");
            
            for(int i=0; i<nb_joueur; i++) //remplissage du tableau de joueur
            {
                Console.WriteLine("\n\n Saisir le nom du joueur " + (i+1));
                tab_joueurs[0]=new Joueur(Console.ReadLine());
            }

            do
            {
                Console.WriteLine(" Saisir une langue : \n FR : Francais\n AN : Anglais"); //saisie de la langue du dico
                langue_dic=Console.ReadLine();
            }while(langue_dic!="FR" || langue_dic!="AN");
            


            ///LANCEMENT D UNE PARTIE
            ///

        }
    }
}
