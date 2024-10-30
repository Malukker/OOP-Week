using System;

namespace TU_Challenge.Heritage
{

    public abstract class Animal
    {
        public event Action OnDie;

        protected bool _isFed = false;

        protected bool _isFedPoisson = false;
        internal bool IsFedPoisson => _isFedPoisson;

        Animalerie _animalerie;

        public void Subscribe(Animalerie animalerie)
        {
            animalerie.OnAddAnimal += FeedFishOnEvent;
        }

        public void Feed()
        {
            _isFed = true;
        }

        public void FeedPoisson(Animal poisson)
        {
            if (poisson is Poisson)
                if (this is Chat)
                    _isFedPoisson = true;
        }

        protected void FeedFishOnEvent(Animal animal)
        {
            if (animal is Poisson) 
                if (this is Chat && this is not ChatQuiBoite)
                {
                    animal.Die();
                    _isFedPoisson = true;
                }
            if (animal is Chat && animal is not ChatQuiBoite)
                if (this is Poisson)
                {
                    animal.FeedPoisson(this);
                    Die();
                }
        }

        protected bool _isAlive = true;

        public bool IsAlive()
        {
            return _isAlive;
        }

        public abstract string Crier();

        public void Die()
        {
            _isAlive = false;
            OnDie?.Invoke();
        }
    }
}
