namespace SSSG.Models
{
    public class RocketProjectile : Projectile
    {
		private static int projSpawnDirection = 1;
        private int projSelfDirection;
		
		public RocketProjectile(Vector2 spawnPosition)
			:base(Vector2 spawnPosition)
		{
			Speed = -5;
			projDamage = 100;
			projSelfDirection = projSpawnDirection;
			projSpawnDirection *= -1;
		}
		
		public void Update()
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
}
