using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    [System.Serializable]
    public class Harbor
    {
        public string Name { get; set; }
        public int Docks { get; set; }
        public List<Slot> SlotList { get; set; }

        public static List<Slot> SetSlotList()
        {
            List<Slot> harborSlotList = new List<Slot>();
            int slotNrIDCounter = 0;
            int dockIDCounter = 1;
            int firstNr = 0;
            int lastNr = 0;
            for (int i = 0; i < 20; i++)
            {
                var normalSlot = new Slot();
                normalSlot.SlotNrID = slotNrIDCounter;
                normalSlot.DockID = $"{dockIDCounter}";
                normalSlot.Capacity = 0;
                normalSlot.SlotSize = 2;
                normalSlot.Type = SlotType.NormalBoatDock;
                harborSlotList.Add(normalSlot);
                
                dockIDCounter++;
                slotNrIDCounter++;
            }
            firstNr = dockIDCounter;

            for (int i = 0; i < 10; i++)
            {
                lastNr = firstNr + 1;
                var sailboatSlot = new Slot();
                sailboatSlot.SlotNrID = slotNrIDCounter;
                sailboatSlot.DockID = $"{firstNr} - {lastNr}";
                sailboatSlot.Capacity = 0;
                sailboatSlot.SlotSize = 4;
                sailboatSlot.Type = SlotType.SailboatDock;
                harborSlotList.Add(sailboatSlot);
                firstNr += 2;

                dockIDCounter++;
                slotNrIDCounter++;
            }

            for (int i = 0; i < 6; i++)
            {
                lastNr = firstNr + 3;
                var cargoshipSlot = new Slot();
                cargoshipSlot.SlotNrID = slotNrIDCounter;
                cargoshipSlot.DockID = $"{firstNr} - {lastNr}";
                cargoshipSlot.Capacity = 0;
                cargoshipSlot.SlotSize = 8;
                cargoshipSlot.Type = SlotType.CargoshipDock;
                harborSlotList.Add(cargoshipSlot);
                firstNr += 4;

                dockIDCounter++;
                slotNrIDCounter++;
            }
            return harborSlotList;
            
        }
    }
}
