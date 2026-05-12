using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    // 1. Продукт: Персонаж гри
    public class Character
    {
        public string Name { get; set; } = "Невідомий";
        public string Height { get; set; } = "Середній";
        public string Build { get; set; } = "Звичайна";
        public string HairColor { get; set; } = "Лисий";
        public string EyeColor { get; set; } = "Карі";
        public string Clothing { get; set; } = "Лохміття";
        public List<string> Inventory { get; set; } = new List<string>();

        // Специфічні поля для добра і зла
        public List<string> GoodDeeds { get; set; } = new List<string>();
        public List<string> EvilDeeds { get; set; } = new List<string>();

        public void ShowInfo()
        {
            Console.WriteLine($"--- Персонаж: {Name} ---");
            Console.WriteLine($"Зріст: {Height}, Статура: {Build}");
            Console.WriteLine($"Волосся: {HairColor}, Очі: {EyeColor}");
            Console.WriteLine($"Одяг: {Clothing}");
            Console.WriteLine($"Інвентар: {string.Join(", ", Inventory)}");

            if (GoodDeeds.Count > 0)
                Console.WriteLine($"Добрі справи: {string.Join(", ", GoodDeeds)}");
            if (EvilDeeds.Count > 0)
                Console.WriteLine($"Злі справи: {string.Join(", ", EvilDeeds)}");
            Console.WriteLine();
        }
    }

    // 2. Єдиний інтерфейс Будівельника (з текучим інтерфейсом)
    public interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetHeight(string height);
        ICharacterBuilder SetBuild(string build);
        ICharacterBuilder SetHairColor(string hairColor);
        ICharacterBuilder SetEyeColor(string eyeColor);
        ICharacterBuilder SetClothing(string clothing);
        ICharacterBuilder AddInventoryItem(string item);
        Character Build();
    }

    // 3. Конкретний будівельник Героя
    public class HeroBuilder : ICharacterBuilder
    {
        private Character _hero = new Character();

        public ICharacterBuilder SetName(string name) { _hero.Name = name; return this; }
        public ICharacterBuilder SetHeight(string height) { _hero.Height = height; return this; }
        public ICharacterBuilder SetBuild(string build) { _hero.Build = build; return this; }
        public ICharacterBuilder SetHairColor(string hairColor) { _hero.HairColor = hairColor; return this; }
        public ICharacterBuilder SetEyeColor(string eyeColor) { _hero.EyeColor = eyeColor; return this; }
        public ICharacterBuilder SetClothing(string clothing) { _hero.Clothing = clothing; return this; }
        public ICharacterBuilder AddInventoryItem(string item) { _hero.Inventory.Add(item); return this; }

        // Специфічний метод тільки для Героя
        public HeroBuilder AddGoodDeed(string deed)
        {
            _hero.GoodDeeds.Add(deed);
            return this;
        }

        public Character Build()
        {
            Character result = _hero;
            _hero = new Character(); // Скидаємо стан після створення
            return result;
        }
    }

    // 4. Конкретний будівельник Ворога
    public class EnemyBuilder : ICharacterBuilder
    {
        private Character _enemy = new Character();

        public ICharacterBuilder SetName(string name) { _enemy.Name = name; return this; }
        public ICharacterBuilder SetHeight(string height) { _enemy.Height = height; return this; }
        public ICharacterBuilder SetBuild(string build) { _enemy.Build = build; return this; }
        public ICharacterBuilder SetHairColor(string hairColor) { _enemy.HairColor = hairColor; return this; }
        public ICharacterBuilder SetEyeColor(string eyeColor) { _enemy.EyeColor = eyeColor; return this; }
        public ICharacterBuilder SetClothing(string clothing) { _enemy.Clothing = clothing; return this; }
        public ICharacterBuilder AddInventoryItem(string item) { _enemy.Inventory.Add(item); return this; }

        // Специфічний метод тільки для Ворога
        public EnemyBuilder AddEvilDeed(string deed)
        {
            _enemy.EvilDeeds.Add(deed);
            return this;
        }

        public Character Build()
        {
            Character result = _enemy;
            _enemy = new Character();
            return result;
        }
    }

    // 5. Директор (керує процесом будівництва складних об'єктів)
    public class Director
    {
        public void ConstructDreamHero(HeroBuilder builder)
        {
            // 1. Загальні методи з інтерфейсу викликаємо ланцюжком
            builder.SetName("Король Артур")
                   .SetHeight("185 см")
                   .SetBuild("Атлетична")
                   .SetHairColor("Блондин")
                   .SetEyeColor("Блакитні")
                   .SetClothing("Сяючі лицарські обладунки")
                   .AddInventoryItem("Меч Екскалібур")
                   .AddInventoryItem("Святий Грааль");

            // 2. Специфічні методи викликаємо окремим ланцюжком
            builder.AddGoodDeed("Врятував королівство від дракона")
                   .AddGoodDeed("Нагодував бідних");
        }

        public void ConstructArchenemy(EnemyBuilder builder)
        {
            // 1. Загальні методи з інтерфейсу викликаємо ланцюжком
            builder.SetName("Мордред Зрадник")
                   .SetHeight("190 см")
                   .SetBuild("Худорлява")
                   .SetHairColor("Чорний")
                   .SetEyeColor("Червоні")
                   .SetClothing("Темний плащ та чорні обладунки")
                   .AddInventoryItem("Отруєний кинджал")
                   .AddInventoryItem("Магічний фоліант некромантії");

            // 2. Специфічні методи викликаємо окремим ланцюжком
            builder.AddEvilDeed("Зрадив короля")
                   .AddEvilDeed("Спалив декілька селищ");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Director director = new Director();

            // Збираємо Героя мрії через Директора
            HeroBuilder heroBuilder = new HeroBuilder();
            director.ConstructDreamHero(heroBuilder);
            Character dreamHero = heroBuilder.Build();
            Console.WriteLine("Герой мрії створений:");
            dreamHero.ShowInfo();

            // Збираємо Ворога через Директора
            EnemyBuilder enemyBuilder = new EnemyBuilder();
            director.ConstructArchenemy(enemyBuilder);
            Character archenemy = enemyBuilder.Build();
            Console.WriteLine("Найзапекліший ворог створений:");
            archenemy.ShowInfo();

            // Демонстрація Fluent Interface БЕЗ Директора (ручна збірка)
            Console.WriteLine("Створення простого NPC за допомогою потокового інтерфейсу:");
            Character npc = new HeroBuilder()
                .SetName("Простий селянин")
                .SetClothing("Лляна сорочка")
                .AddInventoryItem("Вила")
                .Build();

            npc.ShowInfo();

            Console.ReadLine();
        }
    }
}
