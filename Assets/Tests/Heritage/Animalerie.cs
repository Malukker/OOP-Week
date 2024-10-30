using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Android;

namespace TU_Challenge.Heritage
{
    public class Animalerie
    {
        private List<Animal> animals = new List<Animal>();

        public event Action<Animal> OnAddAnimal;

        public void AddAnimal(Animal pet)
        {
            animals.Add(pet);
            pet.Subscribe(this);
            OnAddAnimal?.Invoke(pet);
        }

        public bool Contains(Animal pet)
        {
            return animals.Contains(pet);
        }

        public Animal GetAnimal(int n)
        {
            return animals[n];
        }

        public void FeedAll()
        {
            foreach (Animal pet in animals) pet.Feed();
        }
    }
}
