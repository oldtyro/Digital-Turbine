using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace People.Models
{
    public enum Races { Angles, Saxons, Jutes, Asians}
    public class Person
    {
        private int _Age;
        private Races _Race;

        public string Name { get; set; }
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
                Height = GetHeight(_Age, _Race);
            }
        }
        public Races Race
        {
            get
            {
                return _Race;
            }
            set
            {
                _Race = value;
                Height = GetHeight(_Age, _Race);
            }
        }
        public double Height { get; set; }

        public override string ToString()
        {
            return "My name is '" + Name + "' and I am " + Age + " years old.";
        }

        private double GetHeight(int age, Races race)
        {
            double height;
            switch (race)
            {
                case Races.Angles:
                    height = 1.5 + age * 0.45;
                    break;
                case Races.Saxons:
                    height = 1.5 + age * 0.45;
                    break;
                case Races.Jutes:
                    height = age * 1.6 / 2;
                    break;
                case Races.Asians:
                    height = 1.7 + (age + 2) * 0.23;
                    break;
                default:
                    height = 0;
                    break;
            }
            return Math.Round(height, 2);
        }
    }
}