using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Hunter : Hero
    {
        public const int SteadyShotDamage = 2;

        public Hunter()
        {

        }

        public override int TakeTurn(int currentMana, bool includeFatigue)
        {
            if (IsDead)
                throw new HeroAlreadyDeadException();

            if(includeFatigue)
            {
                CalculateFatigue();
                if (IsDead)
                {
                    return 0;
                }
            }

            if (currentMana >= HeroPowerCost)
            {
                return SteadyShotDamage;
            }
            else
            {
                return 0;
            }
        }
    }
}
