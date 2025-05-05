using System;
using System.Linq;

namespace coursework;

class Program
{
    static void Main()
    {
        var ceo = new TreeNodeIER(new Employee("Иванов Иван", "Генеральный директор"));

        var financeDept = new TreeNodeIER(new Employee("Петрова Мария", "Финансовый директор"));
        var chiefAccountant = new TreeNodeIER(new Employee("Сидорова Елена", "Главный бухгалтер"));
        var accountant1 = new TreeNodeIER(new Employee("Кузнецов Алексей", "Бухгалтер"));

        financeDept.AddChild(chiefAccountant);
        financeDept.AddChild(accountant1);
        ceo.AddChild(financeDept);

        var devDept = new TreeNodeIER(new Employee("Николаев Артём", "Технический директор"));
        var developer1 = new TreeNodeIER(new Employee("Смирнов Дмитрий", "Разработчик C#"));
        var developer2 = new TreeNodeIER(new Employee("Васильев Павел", "Разработчик Python"));

        devDept.AddChild(developer1);
        devDept.AddChild(developer2);
        ceo.AddChild(devDept);

        var hrDept = new TreeNodeIER(new Employee("Михайлова Ольга", "HR-директор"));
        var hrManager = new TreeNodeIER(new Employee("Алексеева Наталья", "HR-менеджер"));

        hrDept.AddChild(hrManager);
        ceo.AddChild(hrDept);

        Console.WriteLine("Текущая структура компании:");
        ceo.Print();
        Console.WriteLine();

        var foundByName = ceo.Find(d => ((Employee)d).Name == "Смирнов Дмитрий");
        Console.WriteLine($"Найден сотрудник: {foundByName?.Data}");

        Console.WriteLine("\n=== Поиск сотрудника по должности ===");
        var foundByRole = ceo.Find(d => ((Employee)d).Role.Contains("HR"));
        Console.WriteLine($"Найден сотрудник: {foundByRole?.Data}");
        Console.WriteLine();

        Console.WriteLine("=== Получение сотрудника по ID ===");
        Guid idToFind = developer1.Id;
        var foundById = TreeNodeIER.GetById(idToFind);
        Console.WriteLine($"Найден сотрудник по ID: {foundById?.Data}");
        Console.WriteLine();

        Console.WriteLine("=== Обход дерева в ширину ===");
        int index = 0;
        foreach (var node in ceo.TraverseBreadthFirst())
        {
            Console.WriteLine($"Node[{index++}] = {node.Data}");
        }
        Console.WriteLine();

        Console.WriteLine("=== Удаление HR-отдела ===");
        hrDept.Remove();

        Console.WriteLine("Структура после удаления HR-отдела:");
        ceo.Print();
        Console.WriteLine();

        Console.WriteLine("=== Уровень сотрудников ===");
        Console.WriteLine($"{((Employee)developer1.Data).Name} находится на уровне: {developer1.Level}");
        Console.WriteLine($"{((Employee)ceo.Data).Name} находится на уровне: {ceo.Level}");
        Console.WriteLine();

        Console.WriteLine("=== Является ли сотрудник листом? ===");
        Console.WriteLine($"{((Employee)developer1.Data).Name} — лист? {developer1.IsLeaf}");
        Console.WriteLine($"{((Employee)devDept.Data).Name} — лист? {devDept.IsLeaf}");
    }
}

public class Employee
{
    public string Name { get; set; }
    public string Role { get; set; }

    public Employee(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public override string ToString()
    {
        return $"{Name}, {Role}";
    }
}