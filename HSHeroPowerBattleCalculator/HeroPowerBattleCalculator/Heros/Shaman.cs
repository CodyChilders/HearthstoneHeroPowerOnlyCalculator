using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Shaman : Hero
    {
        //use these as named array indices
        enum TotemTypes : int
        {
            healingTotem = 0,
            searingTotem = 1,
            wrathOfAirTotem = 2,
            stoneclawTotem = 3
        }

        bool[] ownedTotems = new bool[4];
        int totalTotems = 0;

        static Random sharedRand = new Random();
        Random rand;

        public Shaman()
        {
            for (int i = 0; i < ownedTotems.Length; i++)
                ownedTotems[i] = false;
            //this mitigates a Shaman v. Shaman game from initializing on the same seed.
            rand = new Random(sharedRand.Next());
        }

        void SummonRandomTotem()
        {
            //If they have all totems, this can't be activated.
            //Thus, just return.
            if(totalTotems == ownedTotems.Length)
            {
                return;
            }
            //We're going to get a random offset from 0-3, and loop through elements in the array
            //starting at that index. If the totem there is already owned, move on to the next.
            //This should still simulate randomness of the hero power.
            int startingIndex = rand.Next() % ownedTotems.Length;
            for(int i = 0; i < ownedTotems.Length; i++)
            {
                int checkThisIndex = (i + startingIndex) % ownedTotems.Length;
                if (!ownedTotems[checkThisIndex])
                {
                    ownedTotems[checkThisIndex] = true;
                    break;
                }
            }

            ++totalTotems;
        }

        int CalculateTotemDamage()
        {
            return ownedTotems[(int)TotemTypes.searingTotem] ? 1 : 0;
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
                SummonRandomTotem();
                return CalculateTotemDamage();
            }
            else
            {
                return 0;
            }
        }
    }
}
