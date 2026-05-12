using System;
using System.Collections.Generic;

namespace PrototypePattern
{
    // Клас реалізує інтерфейс ICloneable для підтримки патерну Прототип
    public class Virus : ICloneable
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public List<Virus> Children { get; set; }

        public Virus(double weight, int age, string name, string species)
        {
            Weight = weight;
            Age = age;
            Name = name;
            Species = species;
            Children = new List<Virus>();
        }

        // Реалізація патерну Прототип (Глибоке клонування)
        public object Clone()
        {
            // MemberwiseClone робить "поверхневу" копію (копіює вагу, вік та посилання на рядки)
            Virus clone = (Virus)this.MemberwiseClone();

            // А тепер робимо ГЛИБОКУ копію списку дітей
            // Це гарантує, що при зміні дітей клону, оригінал не зміниться
            clone.Children = new List<Virus>();
            foreach (var child in this.Children)
            {
                // Рекурсивно викликаємо Clone() для кожної дитини
                clone.Children.Add((Virus)child.Clone());
            }

            return clone;
        }

        // Допоміжний метод для красивого виводу дерева вірусів у консоль
        public void Print(int indent = 0)
        {
            Console.WriteLine($"{new string('-', indent)}> {Name} ({Species}) - Вага: {Weight}, Вік: {Age}");
            foreach (var child in Children)
            {
                child.Print(indent + 2);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- 1. Створюємо 3 покоління вірусів ---

            // 1-ше покоління (Дідусь)
            Virus grandParent = new Virus(1.5, 10, "COVID-19", "Coronavirus");

            // 2-ге покоління (Батько)
            Virus parent = new Virus(1.2, 5, "Omicron", "Coronavirus");
            grandParent.Children.Add(parent);

            // 3-тє покоління (Діти)
            Virus child1 = new Virus(0.8, 2, "Kraken", "Coronavirus");
            Virus child2 = new Virus(0.9, 1, "Eris", "Coronavirus");
            parent.Children.Add(child1);
            parent.Children.Add(child2);

            Console.WriteLine("=== ОРИГІНАЛЬНЕ СІМЕЙСТВО ВІРУСІВ ===");
            grandParent.Print();

            // --- 2. Демонстрація патерну Прототип ---
            Console.WriteLine("\nСтворюємо клон всього сімейства...");
            Virus clonedFamily = (Virus)grandParent.Clone();

            // --- 3. Доводимо, що це глибока копія ---
            Console.WriteLine("\n[!] Змінюємо оригінальні віруси (мутація) [!]");
            grandParent.Name = "COVID-19_MUTATED";
            parent.Children[0].Name = "Kraken_DEADLY";
            parent.Children[0].Weight = 99.9;

            Console.WriteLine("\n=== ОРИГІНАЛ ПІСЛЯ МУТАЦІЇ ===");
            grandParent.Print();

            Console.WriteLine("\n=== КЛОН (МАЄ ЗАЛИШИТИСЯ БЕЗ ЗМІН) ===");
            clonedFamily.Print();

            Console.ReadLine();
        }
    }
}
