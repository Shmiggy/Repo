using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSSG.Models;

namespace SSSGTests
{
    [TestClass]
    public class PlayerTests
    {
        private Player player;
        
        [TestInitialize()]
        public void setup()
        {
            player = new Player();
        }

        [TestCleanup()]
        public void tearDown()
        {
        }
        
        [TestMethod]
        public void DamageIsSubtracted()
        {
            int beforeLife = player.Health;
            int damageAmount = 50;
            player.TakeDamage(damageAmount);
            int afterLife = player.Health;
            Assert.AreEqual(beforeLife - damageAmount, afterLife);
        }

        [TestMethod]
        public void PlayerCanDie()
        {
            Assert.IsTrue(player.IsAlive);
            int damageAmount = player.Health;
            player.TakeDamage(damageAmount);
            Assert.IsFalse(player.IsAlive);
        }

    }
}