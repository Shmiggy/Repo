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
		
		// added by Alex. Please review them
		[TestMethod]
        public void PlayerCanShootRockets()
        {
            int noProjectiles = player.Projectiles.Count;
            player.Shoot(ProjectileType.Rocket);
            Assert.IsTrue(player.Projectiles.Count == noProjectiles + 1);
        }
		
		[TestMethod]
        public void PlayerCanShootBeams()
        {
            int noProjectiles = player.Projectiles.Count;
            player.Shoot(ProjectileType.Beam);
            Assert.IsTrue(player.Projectiles.Count == noProjectiles + 1);
        }

    }
}