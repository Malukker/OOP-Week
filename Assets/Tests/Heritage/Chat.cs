using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace TU_Challenge.Heritage
{
    public class Chat : Animal
    {
        private string _name;
        private int _pattes = 4;

        public Chat(string name)
        {
            _name = name;
        }

        internal string Name => _name;
        internal virtual int Pattes => _pattes;

        public override string Crier()
        {
            if (_isFed) return "Miaou (c'est bon laisse moi tranquille humain)";
            else if (_isFedPoisson) return "Miaou (Le poisson etait bon)";
            return "Miaou (j'ai faim)";
        }
    }
}
