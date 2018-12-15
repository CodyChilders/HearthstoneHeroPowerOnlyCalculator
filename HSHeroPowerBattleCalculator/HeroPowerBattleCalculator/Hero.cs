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

        public int CurrentHealth { get; protected set; }
        protected int CurrentFatigueDamage = 1;

        public bool IsDead
        {
            get
            {
                return CurrentHealth < MinHealth;
            }
        }

        public Hero()
        {
            CurrentHealth = MaxHealth;
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
            CurrentHealth -= damage;
        }

        protected void CalculateFatigue()
        {
            CurrentHealth -= CurrentFatigueDamage;
            ++CurrentFatigueDamage;
        }
    }
}
