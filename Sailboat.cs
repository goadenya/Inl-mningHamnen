using System;

namespace Hamnen
{
    [System.Serializable]
    class Sailboat : Boat
    {
        public int BoatLength { get; set; }

        public Sailboat()
        {
            BoatID = $"S-{PrefixGenerator()}";
            Weight = Rng().Next(800, 6000);
            DockingSize = 2;
            MaxSpeed = Rng().Next(1, 12);
            DaysStaying = 4;

            BoatLength = Rng().Next(10, 60);
        }

        public override string UniqueProperty()
        {
            return $"Båtlängd: {BoatLength} fot";

        }
    }
}
