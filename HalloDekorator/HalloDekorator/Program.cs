// See https://aka.ms/new-console-template for more information
using HalloDekorator;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Hello, World!");

var pizza = new Käse(new Salami(new Käse(new Pizza())));
Console.WriteLine($"{pizza.Beschreibung} {pizza.Preis:c}");
