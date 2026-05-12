using System;
using System.Threading;

namespace SingletonPattern
{
    public sealed class Authenticator
    {
        // Статичне поле для зберігання єдиного екземпляра
        private static Authenticator _instance;

        // Об'єкт для блокування потоків (щоб зробити Singleton потокобезпечним)
        private static readonly object _lock = new object();

        // Властивість для перевірки стану автентифікації
        public string CurrentUser { get; private set; }

        // Приватний конструктор гарантує, що об'єкт не можна створити через 'new'
        private Authenticator()
        {
            Console.WriteLine("--- Ініціалізація Authenticator (це має відбутися лише один раз!) ---");
        }

        // Глобальна точка доступу до екземпляра
        public static Authenticator GetInstance()
        {
            // Подвійна перевірка блокування (Double-Check Locking) для безпеки в багатопотоковому середовищі
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Authenticator();
                    }
                }
            }
            return _instance;
        }

        // Метод для імітації логіну
        public void Login(string username)
        {
            CurrentUser = username;
            Console.WriteLine($"Користувач '{username}' успішно увійшов у систему.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Спроба 1: Отримуємо екземпляр Authenticator...");
            Authenticator auth1 = Authenticator.GetInstance();
            auth1.Login("Bogdan_Admin");

            Console.WriteLine("\nСпроба 2: Намагаємося отримати ІНШИЙ екземпляр...");
            Authenticator auth2 = Authenticator.GetInstance();

            Console.WriteLine($"\nЧи auth1 та auth2 це один і той самий об'єкт? -> {ReferenceEquals(auth1, auth2)}");
            Console.WriteLine($"Поточний користувач у auth2: {auth2.CurrentUser}");

            // Демонстрація багатопотоковості (додатковий доказ для викладача)
            Console.WriteLine("\n--- Тестування в кількох потоках ---");
            Thread thread1 = new Thread(() =>
            {
                Authenticator tAuth = Authenticator.GetInstance();
                Console.WriteLine($"Потік 1 отримав доступ. Користувач: {tAuth.CurrentUser}");
            });

            Thread thread2 = new Thread(() =>
            {
                Authenticator tAuth = Authenticator.GetInstance();
                Console.WriteLine($"Потік 2 отримав доступ. Користувач: {tAuth.CurrentUser}");
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.ReadLine();
        }
    }
}
