﻿using System;
namespace Mots_meles
{
    public class Dictionnaire
    {
        private int longueur;
        private string langue;
        private string[] dico;

        public Dictionnaire(int longueur, string langue)
        {
            this.longueur = longueur;
            this.langue = langue;

        }

        public override string ToString()
        {
            string res = "longueur : " + this.longueur + " langue : " + this.langue;
            return res;
        }

        public bool RechDichoRecursif(string mot)
        {
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
        }
    }
}