namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public interface ICollidable
    {
        Rectangle ColisionBox { get; }
        bool CollidesWith(ICollidable other);
    }
}
