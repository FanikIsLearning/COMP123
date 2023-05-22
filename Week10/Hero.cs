using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10
{
    enum PowerEnum {Armour, Elemental_Control, Teleporter, Healer, Marksman, Size_Changer, Strength }
    class Hero
    {
        public string Name { get; }
        public int Age { get; private set; }
        public bool IsGood { get; private set; }
        public PowerEnum Power { get; private set; }
        public Hero(string name, int age, bool isGood, PowerEnum power)
        {
            Name = name; Age = age;
            IsGood = isGood;
            Power = power;
        }
        public static List<Hero> CreateHeros()
            => new List<Hero> {
                new Hero("Iron Man",23, true, PowerEnum.Armour),
                new Hero("Iceman",30, true, PowerEnum.Elemental_Control),
                new Hero("Night Crawler",25, false, PowerEnum.Teleporter),
                new Hero("Elixer",23, true, PowerEnum.Healer),
                new Hero("Green Arrow",23, true, PowerEnum.Marksman),
                new Hero("Vector",23, false, PowerEnum.Strength),
                new Hero("Atom Man",23, true, PowerEnum.Size_Changer)
            };

        internal void Update(int age, bool isGood, PowerEnum power)
        {
            Age = age;
            IsGood = isGood;
            Power = power;
        }
    }
}
