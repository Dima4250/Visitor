using System;
using System.Collections.Generic;

// Интерфейс товара Еlement
public interface IProduct
{
    void Accept(IShopVisitor visitor);
}

// 2. Конкретные классы товаров ConcreteElement
public class Laptop : IProduct
{
    public string Model { get; set; }
    public decimal Price { get; set; }

    public void Accept(IShopVisitor visitor) => visitor.VisitLaptop(this); // Вызываем соответствующий метод посетителя
}

public class Smartphone : IProduct
{
    public string Brand { get; set; }
    public decimal Price { get; set; }

    public void Accept(IShopVisitor visitor) => visitor.VisitSmartphone(this);
}

// 3. Интерфейс посетителя Visitor
public interface IShopVisitor
{
    void VisitLaptop(Laptop laptop); // Посещение ноутов
    void VisitSmartphone(Smartphone phone); // Посещение телефонов
}

// 4. Конкретные посетители ConcreteVisitor

// Посетитель-кассир
public class CashierVisitor : IShopVisitor
{
    private decimal _total;

    public void VisitLaptop(Laptop laptop)
    {
        _total += laptop.Price;
        Console.WriteLine($"Кассовый чек: Ноутбук {laptop.Model} - {laptop.Price} руб.");
    }

    public void VisitSmartphone(Smartphone phone)
    {
        _total += phone.Price;
        Console.WriteLine($"Кассовый чек: Смартфон {phone.Brand} - {phone.Price} руб.");
    }

    public void PrintTotal() => Console.WriteLine($"ИТОГО: {_total} руб.");
}

// Посетитель-инженер
public class EngineerVisitor : IShopVisitor
{
    public void VisitLaptop(Laptop laptop)
        => Console.WriteLine($"Ремонтируем ноутбук {laptop.Model}: ОК");

    public void VisitSmartphone(Smartphone phone)
        => Console.WriteLine($"Ремонтируем смартфон {phone.Brand}: СЛОМАН");
}

class Program
{
    static void Main()
    {
        // Создаем корзину с товарами
        var cart = new List<IProduct>
        {
            new Laptop { Model = "Asus ZenBook", Price = 75000 },
            new Smartphone { Brand = "iPhone 15", Price = 90000 },
            new Laptop { Model = "MacBook AIR", Price = 150000 }
        };

        // Расчет стоимости через посетителя- кассира CashierVisitor
        var cashier = new CashierVisitor();
        foreach (var product in cart)
            product.Accept(cashier);
        cashier.PrintTotal();

        Console.WriteLine();

        // Проверка техники через посетителя-инженера EngineerVisitor
        var engineer = new EngineerVisitor();
        foreach (var product in cart)
            product.Accept(engineer);
    }
}