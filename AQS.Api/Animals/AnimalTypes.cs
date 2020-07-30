using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    interface IAnimal
    {
        string name_ { get; set; }
        string Talk();
    }

    class Dog : IAnimal
    {
        public string name_ { get; set; }
        public string bark_ { get; set; }
        public string Talk()
        {
            return bark_;
        }
    }

    class Cat : IAnimal
    {
        public string name_ { get; set; }
        public string Talk()
        {
            return "Meow!";
        }
    }
}
