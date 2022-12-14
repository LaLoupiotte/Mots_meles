using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mots_meles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mots_meles.Tests
{
    [TestClass()]
    public class DictionnaireTests
    {

        Dictionnaire dico4FR = new Dictionnaire(2, "FR");
        Dictionnaire dico10EN = new Dictionnaire(10, "EN");
        Dictionnaire dico10BL = new Dictionnaire(10, "BL");

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(dico4FR.ToString(), "longueur: 2 langue : FR");
            Assert.AreEqual(dico10EN.ToString(), "longueur: 10 langue : EN");
            Assert.AreEqual(dico10BL.ToString(), "longueur: 10 langue : FR");

        }

        [TestMethod()]
        public void RechDichoRecursifTest()
        {
            Assert.AreEqual(dico4FR.RechDichoRecursif("CHOU"), true);
            Assert.AreEqual(dico10EN.RechDichoRecursif("ABANDONING"), true);
            Assert.AreEqual(dico10EN.RechDichoRecursif("OUI"), false);

        }
    }
}