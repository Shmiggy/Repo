using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class EnemyProjectyle : Projectyle
    {
        public EnemyProjectyle(int speed, int power)
        {
            this.speed = speed; // viteza de deplasare
            this.Power = power; // puterea proiectilului. Probabil o sa coincida cu cea a navei care il produce
            this.dx = -1; // se deplaseaza spre stanga
            this.dy =  0; // nu se deplaseaza pe verticala
        }
    }
}
