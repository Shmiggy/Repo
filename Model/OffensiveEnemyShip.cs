using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class OffensiveEnemyShip : EnemyShip, IOffensive
    {
        public OffensiveEnemyShip(int speed, int life, int power)
            : base(speed, life)
        { 
            this.Power = Power;
        }

        public Projectyle Shoot()
        {
            // produce obiecte de tip EnemyProjectyle, cu puterea = puterea proprie. Ceva de genul:
            // return new EnemyProjectyle(10, this.Power);
            throw new NotImplementedException();
        }

        public int Power { get; set; }

    }
}
