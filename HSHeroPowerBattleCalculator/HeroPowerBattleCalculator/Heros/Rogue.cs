using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Rogue : Hero
    {
        public const int DaggerMasteryDamage = 1;

        public Rogue()
        {

        }

        public override DamageType GetDamageType()
        {
            return DamageType.Attack;
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

            //In this simplified use case, the damage dealt per turn makes
            //no difference whether it comes from a weapon or hero power, 
            //so this is a duplicate of the Mage logic. 
            if (currentMana >= HeroPowerCost)
            {
                return DaggerMasteryDamage;
            }
            else
            {
                return 0;
            }
        }
    }
}
