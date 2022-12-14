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
