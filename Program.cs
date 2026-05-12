using System;
using System.Collections.Generic;

namespace FactoryMethodPattern
{
    // 1. Спільний інтерфейс для всіх підписок [cite: 79]
    public interface ISubscription
    {
        decimal MonthlyFee { get; }
        int MinPeriodMonths { get; }
        List<string> Features { get; }
    }

    // 2. Конкретні реалізації підписок [cite: 83, 84]
    public class DomesticSubscription : ISubscription
    {
        public decimal MonthlyFee => 9.99m; // Щомісячна плата [cite: 80]
        public int MinPeriodMonths => 1;    // Мінімальний період [cite: 80, 81]
        public List<string> Features => new List<string> { "Базові ТВ канали", "Національні фільми" }; // Список каналів/можливостей [cite: 81]
    }

    public class EducationalSubscription : ISubscription
    {
        public decimal MonthlyFee => 14.99m;
        public int MinPeriodMonths => 6;
        public List<string> Features => new List<string> { "Наукові канали", "Документалки", "Без реклами", "Доступ до лекцій" };
    }

    public class PremiumSubscription : ISubscription
    {
        public decimal MonthlyFee => 29.99m;
        public int MinPeriodMonths => 12;
        public List<string> Features => new List<string> { "Всі канали", "4K якість", "Офлайн завантаження", "Доступ з 5 пристроїв" };
    }

    // 3. Базовий клас творця (Creator), який містить Фабричний метод 
    public abstract class SubscriptionCreator
    {
        // Той самий Фабричний метод
        public abstract ISubscription CreateSubscription();

        // Основна бізнес-логіка, яка використовує фабричний метод
        public void PurchaseSubscription()
        {
            // Викликаємо фабричний метод, щоб отримати об'єкт підписки
            ISubscription subscription = CreateSubscription();

            Console.WriteLine($"--- Оформлено підписку: {subscription.GetType().Name} ---");
            Console.WriteLine($"Ціна: {subscription.MonthlyFee}$/міс");
            Console.WriteLine($"Мін. період: {subscription.MinPeriodMonths} міс.");
            Console.WriteLine($"Можливості: {string.Join(", ", subscription.Features)}\n");
        }
    }

    // 4. Конкретні творці (WebSite, MobileApp, ManagerCall) 
    public class WebSite : SubscriptionCreator
    {
        public override ISubscription CreateSubscription()
        {
            Console.WriteLine("[WebSite] Користувач самостійно обрав базовий тариф на сайті.");
            return new DomesticSubscription();
        }
    }

    public class MobileApp : SubscriptionCreator
    {
        public override ISubscription CreateSubscription()
        {
            Console.WriteLine("[MobileApp] Користувач купив підписку через Google Play/App Store.");
            return new EducationalSubscription();
        }
    }

    public class ManagerCall : SubscriptionCreator
    {
        public override ISubscription CreateSubscription()
        {
            Console.WriteLine("[ManagerCall] Менеджер по телефону переконав клієнта взяти найдорожчий тариф!");
            return new PremiumSubscription();
        }
    }

    // 5. Головний метод програми [cite: 87]
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Купівля через сайт
            SubscriptionCreator website = new WebSite();
            website.PurchaseSubscription();

            // Купівля через мобільний додаток
            SubscriptionCreator app = new MobileApp();
            app.PurchaseSubscription();

            // Купівля через дзвінок менеджеру
            SubscriptionCreator manager = new ManagerCall();
            manager.PurchaseSubscription();

            Console.ReadLine();
        }
    }
}