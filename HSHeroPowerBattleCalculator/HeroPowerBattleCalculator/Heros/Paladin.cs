using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Paladin : Hero
    {
        public const int SilverHandRecruitDamage = 1;

        public Paladin()
        {

        }

        protected void SummonSilverHandRecruit()
        {
            if(currentMinions < MaxMinions)
            {
                ++currentMinions;
            }
        }

        protected int SilverHandRecruitTotalDamage()
        {
            return currentMinions * SilverHandRecruitDamage;
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
                SummonSilverHandRecruit();
                return SilverHandRecruitTotalDamage();
            }
            else
            {
                return 0;
            }
        }
    }
}
