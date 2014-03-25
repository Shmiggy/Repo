using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    abstract class Projectyle : GameEntity
    {
        // mostenita din clasa abstracta GameEntity
        public override void Update()
        {
            /*
             * Miscarea proiectilului
             */
            this.XPos += this.speed * this.dx;
            this.YPos += this.speed * this.dy;

            throw new NotImplementedException();
        }

        // metoda apleata de controller la coliziunea cu o nava
        public void Kill()
        {
            this.IsActive = false;
        }

        // puterea proiectilului, coincide cu cea a navei care l-a produs
        protected int Power { get; set; }

        // viteza cu care se deplaseaza.
        protected int speed;

        // directia de desplasare, apartine {-1, 0, 1}
        protected int dx;
        protected int dy;
    }
}
