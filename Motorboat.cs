namespace Hamnen
{
    [System.Serializable]

    class Motorboat : Boat
    {
        public int HorsePower { get; set; }

        public Motorboat()
        {
            BoatID = $"M-{PrefixGenerator()}";
            Weight = Rng().Next(200, 3000);
            DockingSize = 2;
            MaxSpeed = Rng().Next(1, 60);
            DaysStaying = 3;
            HorsePower = Rng().Next(10, 1000);
        }

        public override string UniqueProperty()
        {
            return $"Antal HK: {HorsePower}";
        }
    }
}
