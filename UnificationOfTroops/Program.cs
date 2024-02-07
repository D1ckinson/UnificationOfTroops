using System;
using System.Collections.Generic;
using System.Linq;

namespace UnificationOfTroops
{
    internal class Program
    {
        static void Main()
        {
            char letter = 'б';
            int soldiersQuantity = 5;
            List<Soldier> army1 = new List<Soldier>();
            List<Soldier> army2 = new List<Soldier>();

            FillArmy(army1, soldiersQuantity);
            FillArmy(army2, soldiersQuantity);

            WriteArmies(army1);
            WriteArmies(army2);

            IEnumerable<Soldier> tempList = army1.Where(soldier =>
                soldier.SurName.ToLower().StartsWith(letter.ToString().ToLower()));

            army1 = army1.Except(tempList).ToList();
            army2 = army2.Union(tempList).ToList();

            WriteArmies(army1);
            WriteArmies(army2);
        }

        static void FillArmy(List<Soldier> army, int soldiersQuantity)
        {
            SoldierFabrik soldierFabrik = new SoldierFabrik();

            for (int i = 0; i < soldiersQuantity; i++)
                army.Add(soldierFabrik.CreateSoldier());
        }

        static void WriteArmies(List<Soldier> army1)
        {
            Console.WriteLine();
            army1.ForEach(soldier => Console.WriteLine($"{soldier.Name} {soldier.SurName}"));
        }
    }

    class Soldier
    {
        public Soldier(string fullName, string nationality)
        {
            Name = fullName;
            SurName = nationality;
        }

        public string Name { get; private set; }
        public string SurName { get; private set; }
    }

    class SoldierFabrik
    {
        private List<string> _names;
        private List<string> _surnames;

        private Random _random = new Random();

        public SoldierFabrik()
        {
            FillNames();
            FillSurnames();
        }

        public Soldier CreateSoldier()
        {
            string name = _names[_random.Next(0, _names.Count)];
            string surname = _surnames[_random.Next(_surnames.Count)];

            return new Soldier(name, surname);
        }

        private void FillNames() =>
            _names = new List<string> { "Геннадий", "Дмитрий", "Максим", "Александр" };

        private void FillSurnames() =>
            _surnames = new List<string> { "Бемичев", "Андреев", "Бузнецов", "Киррилов", "Бамонов" };
    }
}
