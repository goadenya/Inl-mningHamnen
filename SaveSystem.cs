using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hamnen
{
    [System.Serializable]
    class SaveSystem
    {
        public static Random Rng = new Random();
        public static string dir = @"C:\Temp";

        public static string HamnenSaveData = Path.Combine(dir, "HamnenSaveData.json");


        public static HarborData LoadData()
        {
            using (Stream stream = File.Open(HamnenSaveData, FileMode.Open))
            {
                try
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    HarborData loadedSim = (HarborData)bformatter.Deserialize(stream);
                    stream.Close();
                    return loadedSim;
                }
                catch (Exception)
                {                   
                    throw;
                }
            }

        }

        public static void SaveData(HarborData saveData)
        {
            using (Stream stream = File.Open(HamnenSaveData, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, saveData);
                stream.Close();
            }
        }

        public static void SetNewHarbor(Harbor harbor)
        {
            harbor.Name = "Havsviken";
            harbor.Docks = 64;
            harbor.SlotList = Harbor.SetSlotList();
        }
    }
}
