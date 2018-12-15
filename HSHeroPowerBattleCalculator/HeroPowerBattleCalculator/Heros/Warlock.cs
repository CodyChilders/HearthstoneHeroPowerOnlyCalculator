using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Warlock : Hero
    {
        public const int LifeTapDamage = 2;

        public Warlock()
        {

        }

        public override int TakeTurn(int currentMana, bool includeFatigue)
        {
            if (IsDead)
                throw new HeroAlreadyDeadException();

            if(includeFatigue)
            {
                CalculateFatigue();
                if(IsDead)
                {
                    return 0;
                }
            }

            if(currentMana >= HeroPowerCost)
            {
                ReceiveDamage(LifeTapDamage);
                CalculateFatigue();
            }

            return 0;
        }
    }
}
