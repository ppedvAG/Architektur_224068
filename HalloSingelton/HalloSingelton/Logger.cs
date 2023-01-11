namespace HalloSingelton
{
    public class Logger
    {
        private static Logger _instance;
        private static object _sync = new object();

        public static Logger Instance
        {
            get
            {
                lock (_sync)
                {
                    if (_instance == null)
                        _instance = new Logger();
                }

                return _instance;
            }
        }

        private Logger()
        {
            Log("Neuer Logger");
        }

        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:F}] {message}");
        }
    }
}
