using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Druid : Hero
    {
        public const int WildshapeDamage = 1;
        public const int WildshapeArmor = 1;

        public Druid()
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

            if (currentMana >= HeroPowerCost)
            {
                ++currentArmor;
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
