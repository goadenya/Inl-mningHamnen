using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    public enum SlotType
    {
        NormalBoatDock = 1,
        SailboatDock,
        CargoshipDock
    }
    [System.Serializable]
    public class Slot
    {
        public int Capacity { get; set; }

        public int SlotNrID { get; set; }
        public string DockID { get; set; }
        public int SlotSize { get; set; }

        public bool slotEmpty = true;

        public SlotType Type { get; set; }     
    }
}
