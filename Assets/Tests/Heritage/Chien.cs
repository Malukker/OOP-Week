using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Chien : Animal
    {
        private string _name;
        private int _pattes = 4;

        internal int Pattes => _pattes;

        public Chien(string name)
        {
            _name = name;
        }

        public override string Crier()
        {
            if (_isFed) return "Ouaf (viens on joue ?)";
            return "Ouaf (j'ai faim)";
        }
    }
}
