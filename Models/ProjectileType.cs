namespace SSSG.Models
{
    using SSSG.Utils.Annotations;

    public enum ProjectileType
    {
        None,
        [StringValue("SSSG.Models.BeamProjectile")]
        Beam,
        [StringValue("SSSG.Models.RocketProjectile")]
        Rocket
    }
}
