using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract class Ship : GameEntity
    {
        // Metoda apelata de controller la coliziunea cu un proiectil.
        //Parametrul damage probabil va fi egal cu puterea proiectilului
        public void TakeDamage(int damage)
        {
            this.Life -= damage;
        }

        // metoda apelata de controller cand intra in coliziune cu o alta nava
        public void Kill()
        {
            this.Life = 0;
        }

        public override void Update()
        {
            // update activitate:
            if (this.Life <= 0)
            {
                this.IsActive = false;
            }

            // Miscarea navei
            this.XPos += this.speed * this.dx;
            this.YPos += this.speed * this.dy;

            throw new NotImplementedException();
        }

        // viata
        public int Life { get; protected set; }

        // viteza cu care se deplaseaza.
        protected int speed;

        // directia de desplasare, apartine {-1, 0, 1}
        protected int dx;
        protected int dy;
    }
}
