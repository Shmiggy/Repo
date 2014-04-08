using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    interface ICollidable
    {
        int Top { get; }
        int Bottom { get; }
        int Left { get; }
        int Right { get; }

        bool IsCollidingWith(ICollidable other);
    }
}