using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract class GameEntity : ICollidable
    {
        // dimensiunile coincid cu cele ale assetului
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        // pozitia in lume, coincide cu cea de pe ecran. Referinta: stanga-sus
        public int XPos { get; protected set; }
        public int YPos { get; protected set; }

        // daca e activ sau nu in joc
        public bool IsActive { get; protected set; }

        // metoda apelata in Controller, la fiecare frame, dupa tratarea evenimentelor
        public abstract void Update();

        // implementata aici pentru ca are aceeasi implementare pt toate entitatile
        public bool IsCollidingWith(ICollidable other)
        {
            return !(other.Left > this.Right ||
                        other.Right < this.Left ||
                        other.Top > this.Bottom ||
                        other.Bottom < this.Top);
        }

        public int Top
        {
            get { return YPos; }
        }

        public int Bottom
        {
            get { return YPos + Height; }
        }

        public int Left
        {
            get { return XPos; }
        }

        public int Right
        {
            get { return XPos + Width; }
        }
    }
}
