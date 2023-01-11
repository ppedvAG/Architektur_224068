// See https://aka.ms/new-console-template for more information
using HalloSingelton;

Console.WriteLine("Hello, World!");

//var logger = new Logger();
//logger.Log("Hallo");

for (int i = 0; i < 10; i++)
{
    Task.Run(() => Logger.Instance.Log($"Hallo {i}"));
}


Logger.Instance.Log("Hallo 1");
Logger.Instance.Log("Hallo 2");
Logger.Instance.Log("Hallo 3");
