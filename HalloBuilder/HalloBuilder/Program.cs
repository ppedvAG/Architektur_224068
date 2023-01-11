// See https://aka.ms/new-console-template for more information
using HalloBuilder;

Console.WriteLine("Hello, World!");

var schrank1 =  new Schrank.Builder()
                           .SetTüren(4)
                           .SetBöden(4)
                           .SetOberfläche(Oberfläche.Lackiert)
                           .SetFarbe("rot")
                           .Create();
