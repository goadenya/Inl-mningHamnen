using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    [System.Serializable]
    public enum BoatType
    {
        Rowboat = 1,
        Motorboat,
        Sailboat,
        Cargoship,
        Dummy
    }
    [System.Serializable]
    public abstract class Boat
    {        
        public string BoatID { get; set; }
        public int DockingSize { get; set; }
        public int Weight { get; set; }
        public double MaxSpeed { get; set; }
        public BoatType Type { get; set; }

        public int DaysStaying { get; set; }     
        
        public int DockNr { get; set; }
        public string DockID { get; set; }

        public string PrefixGenerator()
        {
            char[] letterArray = new char[3];
            for (int i = 0; i < 3; i++)
            {
                letterArray[i] = (char)(Rng().Next(65, 90));
            }
            string prefix = new string(letterArray);
            return prefix;
        }

        public string BoatInfo()
        {
                return $" {Type}     {BoatID}     {Weight}     {MaxSpeed}     {UniqueProperty()}";
        }

        public Random Rng()
        {
            return Utils.Rng;
        }

        abstract public string UniqueProperty();
    }   
}
