namespace Pet
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Pet> pets = new List<Pet>();

            pets.Add(new Pet("Red", 1, "Abyssinian Cat"));
            pets.Add(new Pet("Orange", 2, "American Bobtail Cat Breed"));
            pets.Add(new Pet("Yellow", 3, "Bengal Cat"));
            pets.Add(new Pet("Green", 4, "Manx Cat"));

            //3. Use some of the methods
            pets[0].SetOwner("Ivan");
            pets[1].SetOwner("Billy");
            pets[2].SetOwner("Ivan");

            for (int i = 0; i < pets.Count; i++)
            {
                if (i % 2 != 0)
                {
                    pets[i].Train();
                }
            }

            //4. Display all the objects
            foreach (var pet in pets)
            {
                Console.WriteLine(pet + "\n");
            }

            //5.	Prompt the user for an owner’s name and then display only the pets belonging to a particular person.
            Console.WriteLine("Type a name of owner");
            string owner = Console.ReadLine();
            for (int i = 0; i < pets.Count; i++)
            {
                if (pets[i].Owner == owner)
                {
                    Console.Write($"pet:{pets[i].Name} ");
                }
            }
            Console.WriteLine($"belong(s) to owner:{owner}");

        }

        class Pet
        {
            public string Name { get; }
            public string Owner { get; private set; }
            public int Age { get; }
            public string Description { get; set; }
            public bool IsHouseTrained { get; private set; }


            public Pet(string name, int age, string description)
            {
                Name = name;
                Age = age;
                Description = description;
                Owner = "no one";
                IsHouseTrained = false;
            }

            public void SetOwner(string owner)
            {
                Owner = owner;
            }

            public void Train()
            {
                IsHouseTrained = true;
            }

            public override string ToString()
            {
                return ("Name of Pet: " + Name + "\nAge: " + Age + "\nOwner: " + Owner + "\nDescription: " + Description + "\nTrained:" + IsHouseTrained);
            }
        }
    }
}
