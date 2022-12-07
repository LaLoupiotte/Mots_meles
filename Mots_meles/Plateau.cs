using System;
namespace Mots_meles
{
    public class Plateau
    {
        private int difficulte;
        private int nbColonnes;
        private int nbLignes;
        private string[] mots;
        private string[] directions = new string[] { "N", "S", "E", "O", "NE", "NO", "SE", "SO" };
        private char[,] tableau;
        

        public Plateau()
        {
            //Constructeur par default avec ces mots par defauts
            //On utilise se constructeur en tant que brouillon
            mots = new string[] {"bonjour", "elysee", "chaussures", "automatismes", "toiture"};
            nbColonnes = 10;
            nbLignes = 15;
            string[,] matriceMotsMeles = matriceFromMots(mots);
        }

        private string[,] matriceFromMots(string[] mots)
        {
            int conteurMots = 0;
            while(conteurMots < mots.Length)
            {
                var rand = new Random();
                int colonne = rand.Next(nbColonnes);
                int ligne = rand.Next(nbLignes);
                string direction = directions[rand.Next(directions.Length)];
            }
        }

        private void rempliN(int x, int y, string mot)
        {
            int cont = 0;
            for(int i = y; i < y+mot.Length; i++)
            {
                this.tableau[x, i] = mot[cont];
                cont += 1;
            }
        }

        private void rempliS(int x, int y, string mot)
        {
            int cont = 0;
            for(int i = y; i > y-mot.Length; i--)
            {
                this.tableau[x, i] = mot[cont];
                cont += 1;
            }
        }

        private void rempliO(int x, int y, string mot)
        {
            int cont = 0;
            for (int i = x; i > x - mot.Length; i--)
            {
                this.tableau[i, y] = mot[cont];
                cont += 1;
            }
        }

        private void rempliE(int x, int y, string mot)
        {
            int cont = 0;
            for (int i = x; i > y - mot.Length; i--)
            {
                this.tableau[i, y] = mot[cont];
                cont += 1;
            }
        }

        private string printMatrice(int[,] matrice)
        {
            string res = "";
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    res += matrice[i, j] + ",";
                }
                res += "\n";
            }
            return res;
        }

    }
}
