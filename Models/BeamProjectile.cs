namespace SSSG.Models
{
    public class BeamProjectile : Projectile
    {
        public BeamProjectile(Vector2 spawnPosition)
			:base(spawnPosition)
        
		{
			projDamage = 10;
			Speed = 10;
        }
    }
}
