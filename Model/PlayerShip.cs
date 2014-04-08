using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class PlayerShip : Ship, IOffensive
    {
        public PlayerShip(int speed, int life, int power)
        {
            this.speed = speed;
            this.Life = life;
            this.Power = power;
        }

        public void Move(double dx, double dy)
        {
            Utils.Normalise(ref dx, ref dy);

            this.XPos += Convert.ToInt32(dx * this.speed);
            this.YPos += Convert.ToInt32(dy * this.speed);
        }

        public Projectyle Shoot()
        {
            // produce obiecte de tip PlayerProjectyle cu puterea = puterea proprie. Ceva de genul:
            // return new PlayerProjectyle(10, this.Power);
            throw new NotImplementedException();
        }

        public int Power { get; protected set; }

        // ar trebui sa avem o clasa cu utils
    }
}
