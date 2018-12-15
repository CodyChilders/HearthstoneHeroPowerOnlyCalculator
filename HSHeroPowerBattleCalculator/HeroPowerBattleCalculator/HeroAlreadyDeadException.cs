using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroPowerBattleCalculator
{
    public class HeroAlreadyDeadException : Exception
    {
        public HeroAlreadyDeadException(string msg) : base(msg) { }
        public HeroAlreadyDeadException() : base("This hero has already died.") { }
    }
}
