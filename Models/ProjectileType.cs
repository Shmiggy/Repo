namespace SSSG.Models
{
    using SSSG.Utils.Annotations;

    public enum ProjectileType
    {
        None,
        [ClassPath("SSSG.Models.BeamProjectile")]
        Beam,
        [ClassPath("SSSG.Models.RocketProjectile")]
        Rocket
    }
}
