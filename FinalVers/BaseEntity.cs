using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    abstract class BaseEntity : ICollidable
    {
        public bool CollidesWith(ICollidable other)
        {
            return !(other.ColisionBox.Left > this.ColisionBox.Right ||
                       other.ColisionBox.Right < this.ColisionBox.Left ||
                       other.ColisionBox.Top > this.ColisionBox.Bottom ||
                       other.ColisionBox.Bottom < this.ColisionBox.Top);
        }

        public abstract Rectangle ColisionBox { get; set; }
        public bool IsActive { get; set; }
    }
}
