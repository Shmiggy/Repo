namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using SSSG.Utils.Annotations;

    public abstract class ProjectileFactory
    {
        /// <summary>
        /// Creates a new Projectile object.
        /// </summary>
        /// <param name="position">the location on the screen where the projectile should be placed</param>
        /// <returns>the newly created projectile</returns>
        protected abstract Projectile create(Vector2 position);

        // here will be registered all projectile factories.
        private static Dictionary<ProjectileType, ProjectileFactory> factories = new Dictionary<ProjectileType, ProjectileFactory>();

        /// <summary>
        /// Registers a new Projectile factory.
        /// </summary>
        /// <param name="type">projectile type to be created</param>
        /// <param name="factory">projectile factory to be registered</param>
        public static void AddFactory(ProjectileType type, ProjectileFactory factory)
        {
            if ( !factories.ContainsKey(type) )
            {
                factories.Add(type, factory);
            }
        }

        /// <summary>
        /// Creates a new Projectile object using the proper factory.
        /// </summary>
        /// <param name="type">projectile type to created</param>
        /// <param name="position">the location on the screen where the projectile should be placed</param>
        /// <returns>the newly created projectile</returns>
        public static Projectile CreateProjectile(ProjectileType type, Vector2 position)
        {
            if ( !factories.ContainsKey(type) )
            {
                try
                {
                    ClassPath attribute = type.GetAttributeFromField<ClassPath>(type.ToString());
                    Type reflectedType = Type.GetType(attribute.Value);
                    // calls the static constructor
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(reflectedType.TypeHandle);
                }
                catch
                {
                }

                if ( !factories.ContainsKey(type) )
                {
                    return null;
                }
            }

            return factories[type].create(position);
        }
    }
}
