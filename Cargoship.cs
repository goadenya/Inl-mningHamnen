using System;

namespace Hamnen
{
    [System.Serializable]
    class Cargoship : Boat
    {
        public int ContainerCount { get; set; }

        public Cargoship()
        {
            BoatID = $"C-{PrefixGenerator()}";
            Weight = Rng().Next(3000, 20000);
            DockingSize = 8;
            MaxSpeed = Rng().Next(1, 20);
            DaysStaying = 6;

            ContainerCount = Rng().Next(0, 500);
        }
        public override string UniqueProperty()
        {
            return $"Containers: : {ContainerCount}";
        }
    }
}
