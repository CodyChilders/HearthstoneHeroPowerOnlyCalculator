using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Priest : Hero
    {
        public const int LesserHealRecovery = 2;

        public Priest()
        {

        }

        public override int TakeTurn(int currentMana, bool includeFatigue)
        {
            if (IsDead)
                throw new HeroAlreadyDeadException();

            if (includeFatigue)
            {
                CalculateFatigue();
                if (IsDead)
                {
                    return 0;
                }
            }

            if(currentMana >= HeroPowerCost)
            {
                currentHealth = Math.Min(currentHealth + 2, MaxHealth);
            }

            return 0;
        }
    }
}
