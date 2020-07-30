using System;
using System.Collections.Generic;

namespace Animals
{
    class Program
    {
        static void Main(string[] args)
        {
            var animals = new List<IAnimal>();

            animals.Add(new Dog { name_ = "Bart", bark_ = "Wooooof!" });
            animals.Add(new Dog { name_ = "Woytek", bark_ = "Wroof!" });
            animals.Add(new Cat { name_ = "Robin"});

            PrintAnimals(animals);
        }

        private static void PrintAnimals(IEnumerable<IAnimal> animals)
        {
            foreach (IAnimal animal in animals)
            {
                Console.WriteLine("Here is {0}: {1}", animal.name_, animal.Talk());
            }
        }
    }
}
