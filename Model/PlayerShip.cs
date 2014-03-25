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

        public void MoveUp()
        {
            this.dy = -1;
        }

        public void MoveDown()
        {
            this.dy = 1;
        }

        public void MoveLeft()
        {
            this.dx = -1;
        }

        public void MoveRight()
        {
            this.dy = 1;
        }

        public Projectyle Shoot()
        {
            // produce obiecte de tip PlayerProjectyle cu puterea = puterea proprie. Ceva de genul:
            return new PlayerProjectyle(10, this.Power);
            throw new NotImplementedException();
        }

        public int Power { get; protected set; }
    }
}
