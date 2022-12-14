using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mots_meles
{
    public class Dictionnaire
    {
        private int difficulte;
        private string langue; 
        private List<string> words;

        public List<string> Words
        {
            get { return this.words; }
        }

        public Dictionnaire(int difficulte, string langue)
        {
            this.difficulte = difficulte;
            this.langue = langue;
            //On appelle la fonction ReadFile afin d'avoir une liste avec tout les mots possibles
            if (langue == "EN")
            {
                this.words = ReadFile("fichiersTexte/MotsPossiblesEN.txt", difficulte);
            }
            else
            {
                this.words = ReadFile("fichiersTexte/MotsPossiblesFR.txt", difficulte);
            }
        }

        public override string ToString()
        {
            string res = "longueur : " + this.difficulte + " langue : " + this.langue;
            return res;
        }

        public bool RechDichoRecursif(string mot, List<string> liste = null, int depart=0, int fin=-1)
        {
            //Attribue les valeurs par defaut pour liste et words
            if(liste == null)
            {
                liste = words;
                fin = words.Count - 1;
            }
            //On vérifie que le départ est bien inférieur à l'arrivée
            if (depart > fin)
            {
                return false;
            }
            //Récupéartion de l'indice du milieu
            int mid = (depart + fin) / 2;
            //Si l'élement présent à l'indice mid dans la liste correspond au mot on retourne true
            if (liste[mid] == mot)
            {
                return true;
            }
            //Si le mot se situe avant le compareTo vaudra -1
            if (mot.CompareTo(liste[mid]) < 0)
            {
                //On rappelle la fonction mais la fin sera mid-1
                return RechDichoRecursif(mot, liste, depart, mid - 1);
            }
            else
            {
                //On rappelle la fonction mais le début sera mid-1
                return RechDichoRecursif(mot, liste, mid + 1, fin);
            }
        }

        public static List<string> ReadFile(string filePath, int difficulte)
        {
            //Création d'une liste lignes
            List<string> lines = new List<string>();
            //On vérifie que le fichier existe bien
            try
            {
                string text = File.ReadAllText(filePath);
                lines.AddRange(text.Split('\n'));
            }
            catch (Exception exe)
            {
                Console.WriteLine("Erreur dans la lecture du fichier");
            }
            //Création de la liste contenant tout les mots
            List<string> words = new List<string>();
            if (difficulte <= 15)
            {
                //Cet entier correspond a ligne dans le fichier ou ce trouve la ligne correspondant a la difficulte
                //Par exemple pour une difficulte 2, cela va correspondre à la liste de l'indice ou se trouve la ligne des mots de deux lettres
                int lineOfDifficulte = (difficulte - 1) * 2 - 1;
                string[] line = lines[lineOfDifficulte].ToString().Split(" ");
                for (int j = 0; j < line.Length; j++)
                {
                    words.Add(line[j]);
                }
            }
            else
            {
                Console.WriteLine("Erreur: la longeur maximale d'un mot est de 15 lettres");
                return null;
            }
            return words;
        }
    }
}
