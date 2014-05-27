namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public class Projectile : BaseEntity
    {
        public Vector2 projPoz;
        public int projDamage, projType;
        private static int projSpawnDirection = 1;
        int projSelfDirection;

        public Projectile(int type, Vector2 spawnPosition)
        {
            Health = 1;
            projType = type;
            projDamage = (int)Math.Pow(10, type);
            projPoz = spawnPosition;
            if (type == 1)
            {
                Speed = 10;
            }
            else
            {
                Speed = -5;
                projSelfDirection = projSpawnDirection;
                projSpawnDirection *= -1;
            }
        }

        public void Update()
        {
            if (projType == 1)
            {
                projPoz.X += Speed;
            }
            else
            {
                projPoz.X += Speed;
                Speed += 0.25f;

                if ( Speed < 0 )
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
                else if ( Speed >= 5 && Speed <= 11.75 )
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
        }

    }
}
