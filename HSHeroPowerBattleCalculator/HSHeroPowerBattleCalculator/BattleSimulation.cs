using HeroPowerBattleCalculator;
using System;
using System.Collections.Generic;

namespace HSHeroPowerBattleCalculator
{
    static class BattleSimulation
    {
        const int MaxTurns = 50;

        public static Turn[] RunBattle(Hero p1, Hero p2, bool includeFatigue)
        {
            List<Turn> turns = new List<Turn>
            {
                //start by adding in the initial board state, before running the simulation.
                new Turn(p1, p2, 0, 0)
            };

            for (int turn = 1; turn < MaxTurns; ++turn)
            {
                int mana = Math.Min(turn, Hero.MaxMana);

                int damageToP2 = p1.TakeTurn(mana, includeFatigue);
                //turns.Add(new Turn(p1, p2, mana, turn));
                if (p1.IsDead)
                {
                    break;
                }

                p2.ReceiveDamage(damageToP2, p1.GetDamageType());
                turns.Add(new Turn(p1, p2, mana, turn));
                if (p2.IsDead)
                {
                    break;
                }

                int damageToP1 = p2.TakeTurn(mana, includeFatigue);
                //turns.Add(new Turn(p1, p2, mana, turn));
                if (p2.IsDead)
                {
                    break;
                }

                p1.ReceiveDamage(damageToP1, p2.GetDamageType());
                turns.Add(new Turn(p1, p2, mana, turn));
                if (p1.IsDead)
                {
                    break;
                }
            }

            return turns.ToArray();
        }
    }
}
