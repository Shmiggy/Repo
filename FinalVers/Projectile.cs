using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    class Projectile : BaseEntity
    {
        float projSpeed;
        public Vector2 projPoz;
        public int projDamage, projType;
        private static int projSpawnDirection = 1;
        int projSelfDirection;

        public Projectile(int type, Vector2 spawnPosition)
        {
            this.IsActive = true;
            projType = type;
            projDamage = (int)Math.Pow(10, type);
            projPoz = spawnPosition;
            if (type == 1)
            {
                projSpeed = 10;
            }
            else
            {
                projSpeed = -5;
                projSelfDirection = projSpawnDirection;
                projSpawnDirection *= -1;
            }
        }

        public void Update()
        {
            if (projType == 1)
            {
                projPoz.X += projSpeed;
            }
            else
            {
                projPoz.X += projSpeed;
                projSpeed += 0.25f;

                if (projSpeed < 0)
                {
                    if (projSelfDirection == 1)
                    {
                        projPoz.Y += 3;
                    }
                    else
                    {
                        projPoz.Y -= 3;
                    }
                }
                else if (projSpeed >= 5 && projSpeed <= 11.75)
                {
                    if (projSelfDirection == -1)
                    {
                        projPoz.Y += 2;
                    }
                    else
                    {
                        projPoz.Y -= 2;
                    }
                }
            }
        }

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int)projPoz.X-16,(int)projPoz.Y-4,32,8);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
