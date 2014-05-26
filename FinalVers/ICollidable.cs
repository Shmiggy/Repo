using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    interface ICollidable
    {
        Rectangle ColisionBox { get; set; }
        bool CollidesWith(ICollidable other);
    }
}
