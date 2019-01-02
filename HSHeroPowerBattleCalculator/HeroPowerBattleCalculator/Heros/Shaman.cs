using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class Shaman : Hero
    {
        public const int SearingTotemAttack = 1;
        public const int StoneclawTotemHealth = 2;
        public const int HealingTotemRecovery = 1;

        //use these as named array indices
        enum TotemType : int
        {
            HealingTotem = 0,
            SearingTotem = 1,
            WrathOfAirTotem = 2,
            StoneclawTotem = 3
        }

        bool[] ownedTotems = new bool[4];
        int stoneclawTotemHealth = 0;

        static Random sharedRand = new Random();
        Random rand;

        public Shaman()
        {
            for (int i = 0; i < ownedTotems.Length; i++)
                ownedTotems[i] = false;
            //this mitigates a Shaman v. Shaman game from initializing on the same seed.
            rand = new Random(sharedRand.Next());
        }

        public override DamageType GetDamageType()
        {
            return DamageType.Attack;
        }

        public override void ReceiveDamage(int damage, DamageType type)
        {
            Debug.Assert(damage >= 0);
            if (damage == 0 || type == DamageType.None)
            {
                return;
            }

            if(type == DamageType.Direct)
            {
                base.ReceiveDamage(damage, type);
            }
            else //if type == DamageType.Attack
            {
                //let's account for stoneclaw totem and possible combination with healing totem.

                //if they have the stoneclaw totem, this calculation is complicated, otherwise, it is easy.
                if(ownedTotems[(int) TotemType.StoneclawTotem])
                {
                    if(damage >= stoneclawTotemHealth) //damage was enough to destroy the totem
                    {
                        --currentMinions;
                        ownedTotems[(int) TotemType.StoneclawTotem] = false;
                        stoneclawTotemHealth = 0;
                        damage -= stoneclawTotemHealth;
                        base.ReceiveDamage(damage, type);
                    }
                    else //damage was insufficient to kill the stoneclaw totem
                    {
                        stoneclawTotemHealth -= damage; //this should only be subtracting 1 in our simplified model of Hearthstone.
                        if(ownedTotems[(int)TotemType.HealingTotem])
                        {
                            stoneclawTotemHealth += HealingTotemRecovery;
                            Debug.Assert(stoneclawTotemHealth == 1 || stoneclawTotemHealth == 2);
                        }
                    }
                }
                else
                {
                    base.ReceiveDamage(damage, type);
                }
            }
        }

        void SummonRandomTotem()
        {
            //If they have all totems, this can't be activated.
            //Thus, just return.
            if(currentMinions == ownedTotems.Length)
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
                    if(checkThisIndex == (int) TotemType.StoneclawTotem)
                    {
                        stoneclawTotemHealth = StoneclawTotemHealth;
                    }
                    break;
                }
            }

            ++currentMinions;
        }

        int CalculateTotemDamage()
        {
            return ownedTotems[(int) TotemType.SearingTotem] ? SearingTotemAttack : 0;
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
