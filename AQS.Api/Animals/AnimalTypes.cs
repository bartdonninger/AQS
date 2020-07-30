using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    interface IAnimal
    {
        string name_ { get; set; }
        bool sexMale_ { get; set; }
        bool brushed_ { get; set; }
        string Talk();
        string GetSex();
        void Brush();
    }

    class Dog : IAnimal
    {
        public string name_ { get; set; }
        public bool sexMale_ { get; set; }
        public bool brushed_ { get; set; }
        public string bark_ { get; set; }
        public string Talk()
        {
            return bark_;
        }
        public string GetSex()
        {
            if (sexMale_)
            {
                return "male";
            }
            else
            {
                return "female";
            }
        }
        public void Brush()
        {
            brushed_ = true;
        }
    }

    class Cat : IAnimal
    {
        public string name_ { get; set; }
        public bool sexMale_ { get; set; }
        public bool brushed_ { get; set; }
        public string Talk()
        {
            return "Meow!";
        }
        public string GetSex()
        {
            if (sexMale_)
            {
                return "male";
            }
            else
            {
                return "female";
            }
        }
        public void Brush()
        {
            brushed_ = true;
        }
    }
}
