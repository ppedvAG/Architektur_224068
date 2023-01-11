namespace HalloBuilder
{
    internal class Schrank
    {
        public int AnzTüren { get; private set; }
        public int AnzBöden { get; private set; }
        public string Farbe { get; private set; }
        public Oberfläche Oberfläche { get; private set; }
        public bool Metallschienen { get; private set; }
        public bool Kleiderstange { get; private set; }

        private Schrank() { }

       internal class Builder
        {
            private readonly Schrank _schrank = new Schrank();

            public Builder SetTüren(int anzTüren)
            {
                if (anzTüren < 2 || anzTüren > 7)
                    throw new ArgumentException("Zuviele oder zuwenige Türen");

                _schrank.AnzTüren = anzTüren;
                return this;
            }

            public Builder SetBöden(int anzBöden)
            {
                if (anzBöden < 1 || anzBöden > 6)
                    throw new ArgumentException("Zuviele oder zuwenige Böden");

                _schrank.AnzBöden = anzBöden;
                return this;
            }

            public Builder SetFarbe(string farbe)
            {
                if (string.IsNullOrWhiteSpace(farbe))
                    throw new ArgumentException("Eine Farbe muss angegeben werden");
                if (_schrank.Oberfläche != Oberfläche.Lackiert)
                    throw new ArgumentException("Farbe nur bei Lackierung");

                _schrank.Farbe = farbe;
                return this;
            }

            public Builder SetOberfläche(Oberfläche oberfläche)
            {
                _schrank.Oberfläche = oberfläche;
                return this;
            }

            public Schrank Create()
            {
                return _schrank;
            }
        }

    }

    public enum Oberfläche
    {
        Unbehndelt,
        Gewachst,
        Lackiert
    }
}
