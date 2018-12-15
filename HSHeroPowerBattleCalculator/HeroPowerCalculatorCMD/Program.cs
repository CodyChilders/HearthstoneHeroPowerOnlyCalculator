using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroPowerBattleCalculator;
using System.Diagnostics;

namespace HeroPowerCalculatorCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            bool includeFatigue = true;

            Hero p1 = new Hunter();
            Hero p2 = new Hunter();
            Hero winner = null;

            for(int turn = 1; turn < 50; turn++)
            {
                int mana = Math.Min(turn, Hero.MaxMana);

                if(p1.IsDead)
                {
                    winner = p2;
                    break;
                }

                int damageToP2 = p1.TakeTurn(mana, includeFatigue);
                p2.ReceiveDamage(damageToP2);

                if(p2.IsDead)
                {
                    winner = p1;
                    break;
                }

                int damageToP1 = p2.TakeTurn(mana, includeFatigue);
                p1.ReceiveDamage(damageToP1);
            }

            string winningPlayer = null;
            if (winner == p1)
            {
                winningPlayer = "Player 1";
            }
            else if (winner == p2)
            {
                winningPlayer = "Player 2";
            }
            else
            {
                throw new InvalidOperationException("Unknown winner.");
            }

            //in the form HeroPowerBattleCalculator.CLASSNAME
            string className = winner.GetType().ToString();
            string[] separatedNamespaceAndClass = className.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(separatedNamespaceAndClass.Length == 2);
            Console.WriteLine($"{winningPlayer} ({separatedNamespaceAndClass[1]}) wins!.");
        }
    }
}
