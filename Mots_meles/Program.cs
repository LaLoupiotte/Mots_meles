using System;

namespace Mots_meles
{
    class Program
    {
        static void Main(string[] args)
        {
            int nb_joueur=0;
            Console.WriteLine(" =-=-=-=-=-= MOTS MELES DEVINCI =-=-=-=-=-=\n");
            Console.WriteLine("\n Inserer le nombre de joueurs : ");
            nb_joueur=Console.ReadLine(int);

            /*
            Joueur num1 = new Joueur("Jonhattan");
            num1.ToString();
            num1.Add_Score(45);
            num1.Add_Mot("Hello");
            Console.WriteLine(num1.ToString());
            */

            Plateau plat = new Plateau();
        }
    }
}
