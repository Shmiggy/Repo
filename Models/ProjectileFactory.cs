namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using SSSG.Utils.Annotations;

    public abstract class ProjectileFactory
    {
        protected abstract Projectile create(Vector2 position);

        private static Dictionary<ProjectileType, ProjectileFactory> factories = new Dictionary<ProjectileType, ProjectileFactory>();

        public static void AddFactory(ProjectileType type, ProjectileFactory factory)
        {
            if ( !factories.ContainsKey(type) )
            {
                factories.Add(type, factory);
            }
        }

        public static Projectile CreateProjectile(ProjectileType type, Vector2 position)
        {
            if ( !factories.ContainsKey(type) )
            {
                try
                {
                    StringValue attribute = type.GetAttributeFromField<StringValue>(type.ToString());
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
