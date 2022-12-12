using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mots_meles

/*
        //IL FAUT TROUVER UN MOYEN DE NE PAS CHANGER LE TABLEAU THIS.DICO


        //Test si le dictionnaire est vide
        if(dico.Length == 0 || dico == null)
        {
            return false;
        }
        //Teste si le premier élement du tableau est le bon mot
        else if(dico[0] == mot)
        {
            return true;
        }
        //Si ce n'est pas le bon mot on rappelle la fonction RechDichoRecursif
        //avec le même tableau mais qui commence à l'indice un
        else
        {
            //Création d'un nouveau dictionnaire commencant à l'indice un
            string[] newDico = new string[dico.Length];
            for(int i = 1; i < dico.Length; i++)
            {
                newDico[i - 1] = dico[i];
            }
            //On copie le nouveau tableau dans this.dico
            dico = new string[newDico.Length];
            for(int i = 0; i < newDico.Length; i++)
            {
                dico[0] = newDico[0];
            }
            return RechDichoRecursif(mot);
        }
        */
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

        public bool RechDichoRecursif(string mot, string dicoInit = null)
        {
            {
                if (mot == null || mot == "")
                {
                    return false;
                }
                int start = 0;
                int end = this.words.Count - 1;
                int middle;

                while (start <= end)
                {
                    middle = (start + end) / 2;
                    if (this.words[middle] == mot)
                        return true;
                    else if (string.Compare(mot, this.words[middle]) < 0)
                        end = middle - 1;
                    else
                        start = middle + 1;
                }
                return false;
            }
        }

        public static List<string> ReadFile(string filePath, int difficulte)
        {
            ArrayList lines = new ArrayList();
            try
            {
                string text = File.ReadAllText(filePath);
                lines.AddRange(text.Split('\n'));
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            int lineOfDifficulte = (difficulte-1)*2-1;
            string[] line = lines[lineOfDifficulte ].ToString().Split(" ");
            List<string> words = new List<string>();
            for (int j = 0; j < line.Length; j++)
            {
                words.Add(line[j]);
            }
            return words;
        }
    }
}
