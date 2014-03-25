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
            this.dx = -1; // se deplaseaza spre stanga
            this.dy =  0; // nu se deplaseaza pe verticala, cel putin momentan
            this.Life = life; // viata
            this.speed = speed; // viteza de deplasare
        }
    }
}
