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
        public void Setup()
        {
            player = new Player();
        }

        [TestCleanup()]
        public void TearDown()
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

        [TestMethod]
        public void PlayerCanShoot()
        {
            int beforeCount = player.Projectiles.Count;
            player.Shoot(ProjectileType.Beam);
            int afterCount = player.Projectiles.Count;
            Assert.AreEqual(afterCount, beforeCount + 1);
        }

        [TestMethod]
        public void PlayerCanShootRockets()
        {
            Projectile firedProjectile = player.Shoot(ProjectileType.Rocket);
            Assert.AreEqual(firedProjectile.GetType(), typeof(RocketProjectile));
        }

        [TestMethod]
        public void PlayerCanShootBeams()
        {
            Projectile firedProjectile = player.Shoot(ProjectileType.Beam);
            Assert.AreEqual(firedProjectile.GetType(), typeof(BeamProjectile));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoInvalidProjectilesAreAllowed()
        {
            player.Shoot(ProjectileType.None);
        }

    }
}