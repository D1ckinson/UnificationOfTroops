using System;
using System.Collections.Generic;
using System.Linq;

namespace UnificationOfTroops
{
    internal class Program
    {
        static void Main()
        {
            ArmyFactory armyFactory = new ArmyFactory();
            Army army = armyFactory.Create();

            army.WriteInfo();
            army.MoveSoldiers();

            Console.WriteLine();
            army.WriteInfo();
        }
    }

    class Army
    {
        private List<Soldier> _soldiers1;
        private List<Soldier> _soldiers2;

        public Army(List<Soldier> soldiers1, List<Soldier> soldiers2)
        {
            _soldiers1 = soldiers1;
            _soldiers2 = soldiers2;
        }

        public void WriteInfo()
        {
            Console.WriteLine("\nПервый взвод:");
            _soldiers1.ForEach(soldier => soldier.WriteInfo());

            Console.WriteLine("\nВторой взвод:");
            _soldiers2.ForEach(soldier => soldier.WriteInfo());
        }

        public void MoveSoldiers()
        {
            string letter = "б";
            List<Soldier> tempSoldiers = _soldiers1.Where(soldier => soldier.Surname.ToLower().StartsWith(letter)).ToList();

            _soldiers1 = _soldiers1.Except(tempSoldiers).ToList();
            _soldiers2 = _soldiers2.Union(tempSoldiers).ToList();
        }
    }

    class ArmyFactory
    {
        public Army Create()
        {
            SoldierFactory soldiersFactory = new SoldierFactory();

            List<Soldier> soldiers1 = soldiersFactory.Create();
            List<Soldier> soldiers2 = soldiersFactory.Create();

            return new Army(soldiers1, soldiers2);
        }
    }

    class Soldier
    {
        public Soldier(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }

        public void WriteInfo()
        {
            const int Offset = -10;

            Console.WriteLine($"{Name,Offset}{Surname,Offset}");
        }
    }

    class SoldierFactory
    {
        public List<Soldier> Create()
        {
            Random random = new Random();
            List<string> names = GetNames();
            List<string> surnames = GetSurnames();

            int soldiersQuantity = 10;
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < soldiersQuantity; i++)
            {
                string name = names[random.Next(names.Count)];
                string surname = surnames[random.Next(surnames.Count)];

                soldiers.Add(new Soldier(name, surname));
            }

            return soldiers;
        }

        private List<string> GetNames() =>
            new List<string> { "Геннадий", "Дмитрий", "Максим", "Александр" };

        private List<string> GetSurnames() =>
            new List<string> { "Бемичев", "Андреев", "Бузнецов", "Киррилов", "Бамонов" };
    }
}
