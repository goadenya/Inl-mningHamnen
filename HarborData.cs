using Hamnen;
using System.Collections.Generic;

namespace Hamnen
{
    [System.Serializable]
    public class HarborData
    {
        public int DaysSimulatedSave { get; set; }
        public int OccupiedSlotsSave { get; set; }
        public List<string> LastBoatArrivalsSave { get; set; }
        public List<string> LastBoatDeparturesSave { get; set; }
        public List<Boat> DockedBoatsSave { get; set; }
        public List<Boat> RejectedBoatsSave { get; set; }
        public List<Slot> HarborDockListSave { get; set; }
        public Harbor HarborSave { get; set; }


        public HarborData(int daysSimulated, int occupiedSlots, List<string> lastBoatArrivals,List<string> lastBoatDepartures, List<Boat> dockedBoats, List<Boat> rejectedBoats, Harbor harbor, List<Slot> HarborDockList)
        {
            DaysSimulatedSave = daysSimulated;
            OccupiedSlotsSave = occupiedSlots;
            LastBoatArrivalsSave = lastBoatArrivals;
            LastBoatDeparturesSave = lastBoatDepartures;
            DockedBoatsSave = dockedBoats;
            RejectedBoatsSave = rejectedBoats;
            HarborDockListSave = HarborDockList;
            HarborSave = harbor;
        }
    }
}
