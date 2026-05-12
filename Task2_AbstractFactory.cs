using System;

namespace AbstractFactoryPattern
{
    // --- 1. Абстрактні продукти (Інтерфейси девайсів) ---
    public interface ILaptop { void Boot(); }
    public interface INetbook { void OpenBrowser(); }
    public interface IEBook { void TurnPage(); }
    public interface ISmartphone { void Call(); }

    // --- 2. Конкретні продукти для бренду IProne ---
    public class IProneLaptop : ILaptop { public void Boot() => Console.WriteLine("IProne Laptop завантажується..."); }
    public class IProneNetbook : INetbook { public void OpenBrowser() => Console.WriteLine("IProne Netbook відкриває Saphari..."); }
    public class IProneEBook : IEBook { public void TurnPage() => Console.WriteLine("IProne EBook гортає сторінку..."); }
    public class IProneSmartphone : ISmartphone { public void Call() => Console.WriteLine("IProne Smartphone телефонує..."); }

    // --- 3. Конкретні продукти для бренду Kiaomi ---
    public class KiaomiLaptop : ILaptop { public void Boot() => Console.WriteLine("Kiaomi Laptop завантажується швидко..."); }
    public class KiaomiNetbook : INetbook { public void OpenBrowser() => Console.WriteLine("Kiaomi Netbook відкриває Хром..."); }
    public class KiaomiEBook : IEBook { public void TurnPage() => Console.WriteLine("Kiaomi EBook гортає сторінку з підсвіткою..."); }
    public class KiaomiSmartphone : ISmartphone { public void Call() => Console.WriteLine("Kiaomi Smartphone телефонує дуже гучно..."); }

    // --- 4. Конкретні продукти для бренду Balaxy ---
    public class BalaxyLaptop : ILaptop { public void Boot() => Console.WriteLine("Balaxy Laptop завантажує Windows..."); }
    public class BalaxyNetbook : INetbook { public void OpenBrowser() => Console.WriteLine("Balaxy Netbook відкриває браузер Edge..."); }
    public class BalaxyEBook : IEBook { public void TurnPage() => Console.WriteLine("Balaxy EBook гортає сторінку на e-ink екрані..."); }
    public class BalaxySmartphone : ISmartphone { public void Call() => Console.WriteLine("Balaxy Smartphone телефонує і складається навпіл..."); }

    // --- 5. Абстрактна фабрика ---
    public interface IDeviceFactory
    {
        ILaptop CreateLaptop();
        INetbook CreateNetbook();
        IEBook CreateEBook();
        ISmartphone CreateSmartphone();
    }

    // --- 6. Конкретні фабрики для кожного бренду ---
    public class IProneFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new IProneLaptop();
        public INetbook CreateNetbook() => new IProneNetbook();
        public IEBook CreateEBook() => new IProneEBook();
        public ISmartphone CreateSmartphone() => new IProneSmartphone();
    }

    public class KiaomiFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new KiaomiLaptop();
        public INetbook CreateNetbook() => new KiaomiNetbook();
        public IEBook CreateEBook() => new KiaomiEBook();
        public ISmartphone CreateSmartphone() => new KiaomiSmartphone();
    }

    public class BalaxyFactory : IDeviceFactory
    {
        public ILaptop CreateLaptop() => new BalaxyLaptop();
        public INetbook CreateNetbook() => new BalaxyNetbook();
        public IEBook CreateEBook() => new BalaxyEBook();
        public ISmartphone CreateSmartphone() => new BalaxySmartphone();
    }

    // --- 7. Клієнтський клас, який використовує фабрику ---
    public class TechStore
    {
        private ILaptop laptop;
        private ISmartphone smartphone;

        // Магазину неважливо, який бренд, він працює через абстрактну фабрику
        public TechStore(IDeviceFactory factory)
        {
            laptop = factory.CreateLaptop();
            smartphone = factory.CreateSmartphone();
        }

        public void TestDevices()
        {
            laptop.Boot();
            smartphone.Call();
            Console.WriteLine("-----------------------");
        }
    }

    // --- 8. Головний метод ---
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Магазин замовляє партію IProne ---");
            IDeviceFactory iproneFactory = new IProneFactory();
            TechStore store1 = new TechStore(iproneFactory);
            store1.TestDevices();

            Console.WriteLine("--- Магазин замовляє партію Kiaomi ---");
            IDeviceFactory kiaomiFactory = new KiaomiFactory();
            TechStore store2 = new TechStore(kiaomiFactory);
            store2.TestDevices();

            Console.WriteLine("--- Магазин замовляє партію Balaxy ---");
            IDeviceFactory balaxyFactory = new BalaxyFactory();
            TechStore store3 = new TechStore(balaxyFactory);
            store3.TestDevices();

            Console.ReadLine();
        }
    }
}
