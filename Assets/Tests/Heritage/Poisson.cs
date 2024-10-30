using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Poisson : Animal
    {
        private string _name;
        private int _pattes = 0;

        public Poisson(string name)
        {
            _name = name + " le poisson";
        }

        internal string Name => _name;
        internal virtual int Pattes => _pattes;


        public override string Crier()
        {
            if (_isFed) return "Miaou (c'est bon laisse moi tranquille humain)";
            return "Miaou (j'ai faim)";
        }
    }
}
