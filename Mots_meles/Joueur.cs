using System;
namespace Mots_meles
{
    public class Joueur
    {
        private string[] motsTrouves;
        private int score;
        private string nom;


        public string Nom
        {
            get { return this.nom; }
        }
        public Joueur(string nom)
        {
            /*Ce constructeur est utilisé si le joueur vient de lancer la partie, la liste
             * mots trouvés aura donc aucun élément
             */
            if (nom != null)
            {
                this.nom = nom;
                this.score = 0;
                this.motsTrouves = new string[0]; //!!!!!!!!revoir si on peut mettre un nul
            }
            else
            {
                Console.WriteLine("La création du joueur n'est pas possible car il n'a pas de nom");
            }
        }

        public Joueur(string nom, int score, string[] motsTrouves)
        /*Ce constructeur est utilisé si le joueur reprend une partie, la liste 
         *de mots trouvés possède déjà des élements
         */
        {
            this.nom = nom;
            this.score = score;
            this.motsTrouves = motsTrouves;
        }

        public void Add_Mot(string mot)
        {
            //Création d'une nouvelle liste avec un élément de plus que la liste motsTrouvés
            string[] listeUpdate = new string[motsTrouves.Length + 1];
            //On copie la liste motsTrouvés dans la nouvelle liste
            for(int i = 0; i < motsTrouves.Length; i++)
            {
                listeUpdate[i] = motsTrouves[i];
            }
            //On ajoute le mot dans la liste
            listeUpdate[motsTrouves.Length] = mot;
            /*On remplace la liste motsTrouves avec listeUpdate c'est a dire la 
             * même liste mais en ajoutant le mot à la fin
             */
            this.motsTrouves = listeUpdate;
        }

        public void Add_Score(int val)
        {
            this.score += val;
        }

        public string MotsTrouvesText()
        {
            /*Cette méthode utilise le tableau motsTrouvés pour en faire une chaine de caracteres,
            *elle a pour but d'être utilisée dans la méthode toString
            */
            string res = "[";
            for(int i = 0; i < motsTrouves.Length; i++)
            {
                res += motsTrouves[i] + ",";
            }
            res += "]";
            return res;
        }

        public string ToString()
        {
            string res = "Nom : " + this.nom + "\nScore : " + this.score + "\nMots trouvés : " + MotsTrouvesText();
            return res;
        }
    }
}
