namespace Hamnen
{
    [System.Serializable]
    class Rowboat : Boat
    {
        public int MaxPassengerCount { get; set; }

        public Rowboat()
        {
            BoatID = $"R-{PrefixGenerator()}";
            Weight = Rng().Next(100, 300);
            DockingSize = 1;
            MaxSpeed = Rng().Next(1, 3);
            DaysStaying = 1;

            MaxPassengerCount = Rng().Next(1, 6);
        }

        public override string UniqueProperty()
        {
            return $"Max Antal pers: {MaxPassengerCount}";
        }
    }
}
