using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class EnemyShip : Ship
    {
        public EnemyShip(int speed, int life)
        {
            this.Life = life; // viata
            this.speed = speed; // viteza de deplasare
        }

        public void Move()
        {
            this.XPos -= speed; // nava se misca spre stanga
        }
    }
}
