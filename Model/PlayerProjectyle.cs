using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class PlayerProjectyle : Projectyle
    {
        public PlayerProjectyle(int speed, int power)
        {
            this.speed = speed; // viteza de deplasare
            this.Power = power; // puterea proiectitului. Probabil o sa coincida cu cea a navei care il produce
            this.dx = 1; // se deplaseaza spre dreapta;
            this.dy = 0; // nu se deplaseaza pe verticala;
        }
    }
}
