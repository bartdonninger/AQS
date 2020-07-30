using System;
using System.Collections.Generic;
using System.Linq;

namespace Animals
{
    class Program
    {
        static void Main(string[] args)
        {
            var animals = new List<IAnimal>();

            animals.Add(new Dog { name_ = "Bart", sexMale_ = true, bark_ = "Wooooof!" });
            animals.Add(new Dog { name_ = "Woytek", sexMale_ = true, bark_ = "Wroof!" });
            animals.Add(new Cat { name_ = "Robin", sexMale_ = false});

            PrintAnimals(animals);
            animals.First(item => item.name_ == "Robin").Brush();
            PrintAnimals(animals);
        }

        private static void PrintAnimals(IEnumerable<IAnimal> animals)
        {
            foreach (IAnimal animal in animals)
            {
                string brushedString;
                if (animal.brushed_)
                {
                    brushedString = "brushed";
                }
                else
                {
                    brushedString = "not brushed";
                }

                Console.WriteLine("{0} says: {1}, is {2} and is {3}", animal.name_, animal.Talk(), animal.GetSex(), brushedString);
            }
        }
    }
}
