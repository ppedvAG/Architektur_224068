namespace HalloDekorator
{
    internal interface IComponent
    {
        string Beschreibung { get; }
        decimal Preis { get; }
    }

    class Pizza : IComponent
    {
        public string Beschreibung => "Pizza";

        public decimal Preis => 7m;
    }

    class Brot : IComponent
    {
        public string Beschreibung => "Brot";

        public decimal Preis => 4m;
    }

    abstract class Deko : IComponent
    {
        protected readonly IComponent parent;

        public Deko(IComponent parent)
        {
            this.parent = parent;
        }

        public abstract string Beschreibung { get; }

        public abstract decimal Preis { get; }
    }

    class Salami : Deko
    {
        public Salami(IComponent parent) : base(parent)
        { }

        public override string Beschreibung => parent.Beschreibung + " Salami";

        public override decimal Preis => parent.Preis + 2.3m;
    }

    class Käse : Deko
    {
        public Käse(IComponent parent) : base(parent)
        { }

        public override string Beschreibung => parent.Beschreibung + " Käse";

        public override decimal Preis => parent.Preis + 1.5m;
    }
}
