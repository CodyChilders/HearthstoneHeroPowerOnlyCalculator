﻿using System;
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
        const int MaxTurns = 50;

        static void Main(string[] args)
        {
            
            bool includeFatigue = true;

            Hero p1 = new Shaman();
            Hero p2 = new Shaman();
            Hero winner = null;

            for(int turn = 1; turn < MaxTurns; turn++)
            {
                int mana = Math.Min(turn, Hero.MaxMana);

                int damageToP2 = p1.TakeTurn(mana, includeFatigue);
                if (p1.IsDead)
                {
                    winner = p2;
                    break;
                }

                p2.ReceiveDamage(damageToP2, p1.GetDamageType());
                if(p2.IsDead)
                {
                    winner = p1;
                    break;
                }

                int damageToP1 = p2.TakeTurn(mana, includeFatigue);
                if (p2.IsDead)
                {
                    winner = p1;
                    break;
                }

                p1.ReceiveDamage(damageToP1, p2.GetDamageType());
                if (p1.IsDead)
                {
                    winner = p2;
                    break;
                }
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
                winningPlayer = "Neither player";
            }

            //in the form "HeroPowerBattleCalculator.CLASSNAME"
            string className = winner?.GetType().ToString() ?? "No class";
            string[] separatedNamespaceAndClass = className.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(separatedNamespaceAndClass.Length == 2);
            Console.WriteLine($"{winningPlayer} ({separatedNamespaceAndClass[1]}) wins!");
        }
    }
}
