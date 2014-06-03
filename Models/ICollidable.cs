namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public interface ICollidable
    {
        /// <summary>
        /// Gets the collision box of the entity.
        /// </summary>
        Rectangle CollisionBox { get; }

        /// <summary>
        /// Returns whether or not the entity collides with another collidable entity.
        /// </summary>
        /// <param name="other">another collidable entity</param>
        /// <returns></returns>
        bool CollidesWith(ICollidable other);
    }
}
