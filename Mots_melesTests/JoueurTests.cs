using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mots_meles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mots_meles.Tests
{
    [TestClass()]
    public class JoueurTests
    {

        Joueur joueur1 = new Joueur("Ju");

        [TestMethod()]
        public void Add_ScoreTest()
        {
            Assert.AreEqual(0, joueur1.Score);
        }

        [TestMethod()]
        public void MotsTrouvesTextTest()
        {
            Assert.AreEqual("[ ]", joueur1.MotsTrouvesText());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(joueur1.ToString(), "Nom : Ju\nScore : 0\nnMots trouvés : [ ]");
        }
    }
}