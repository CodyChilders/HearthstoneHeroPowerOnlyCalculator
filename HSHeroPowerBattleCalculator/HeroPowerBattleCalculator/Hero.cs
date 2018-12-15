using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public abstract class Hero
    {
        public const int MaxHealth = 30;
        public const int MinHealth = 1;
        public const int HeroPowerCost = 2;
        public const int MaxMana = 10;
        public const int MaxMinions = 7;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
        }

        public int TotalHealthAndArmor
        {
            get
            {
                return currentHealth + currentArmor;
            }
        }

        public bool IsDead
        {
            get
            {
                return CurrentHealth < MinHealth;
            }
        }

        protected int currentHealth;
        protected int currentArmor;
        protected int nextFatigueDamage;
        protected int currentMinions;

        public Hero()
        {
            currentHealth = MaxHealth;
            currentArmor = 0;
            nextFatigueDamage = 0;
            currentMinions = 0;
        }

        /// <summary>
        /// Updates internal state (current health, damage from fatigue, etc), and then returns
        /// how much damage was dealt to the enemy hero.
        /// </summary>
        /// <param name="currentMana">Current mana, in range [1, 10]</param>
        /// <param name="includeFatigue">Whether to include damage from failing to draw cards.</param>
        /// <returns>How much damage to deal to the enemy hero.</returns>
        public abstract int TakeTurn(int currentMana, bool includeFatigue);

        public void ReceiveDamage(int damage)
        {
            //armor introduces a lot of extra cases
            //subtract damage from armor first.
            //if there is still armor left, we're done
            //otherwise, let the damage spill over into health.
            if (currentArmor > 0)
            {
                currentArmor -= damage;
                if (currentArmor < 0)
                {
                    damage = -currentArmor;
                    currentArmor = 0;
                }
                else if (currentArmor >= 0)
                {
                    return;
                }
            }

            //default case - deduct damage (or leftovers from insufficient armor) from health.
            currentHealth -= damage;
        }

        protected void CalculateFatigue()
        {
            ReceiveDamage(nextFatigueDamage);
            ++nextFatigueDamage;
        }
    }
}
