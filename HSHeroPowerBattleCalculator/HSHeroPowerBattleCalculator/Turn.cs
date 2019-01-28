using HeroPowerBattleCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSHeroPowerBattleCalculator
{
    class Turn
    {
        public int P1Health { get; private set; }
        public int P1Armor { get; private set; }
        public int P1Fatigue { get; private set; }

        public int P2Health { get; private set; }
        public int P2Armor { get; private set; }
        public int P2Fatigue { get; private set; }

        public int Mana { get; private set; }

        public int TurnNumber { get; private set; }

        public Turn(Hero p1, Hero p2, int mana, int turnNumber)
        {
            P1Health = p1.CurrentHealth;
            P1Armor = p1.CurrentArmor;
            P1Fatigue = p1.NextFatigue;

            P2Health = p2.CurrentHealth;
            P2Armor = p2.CurrentArmor;
            P2Fatigue = p2.NextFatigue;

            Mana = mana;
            TurnNumber = turnNumber;
        }
    }
}
