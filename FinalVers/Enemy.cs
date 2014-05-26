using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    class Enemy : BaseEntity
    {
        public int enemyHealth;
        public int enemyDamage;
        float enemySpeed;
        public Vector2 enemyPosition;
        static readonly Random rng = new Random();
        

        public void Initialize()
        {
            Vector2 startLocation = new Vector2();
            startLocation.X = 1000;
            startLocation.Y = rng.Next(50, 550);

            enemyHealth = 100;
            enemyDamage = 20;
            enemySpeed = 5.0f;
            enemyPosition = startLocation;
        }

        public void UpdatePozition()
        {
            enemyPosition.X -= enemySpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100;
        }

        public void GetDamaged(int damageValue)
        {
            enemyHealth -= damageValue;
        }

        public new bool IsActive // nu asa
        {
            get
            {
               return enemyHealth > 0;
            }
        
        }

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int)enemyPosition.X - 32, (int)enemyPosition.Y - 32, 64, 64);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
