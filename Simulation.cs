using Hamnen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hamnen
{
    [System.Serializable]
    public class Simulation
    {
        static int BoatsADay = 5;
        static Harbor myHarbor { get; set; }     

        static List<Boat> DockedBoats { get; set; }

        static List<Boat> RejectedBoatsList = new List<Boat>();
        static int OccupiedSlots { get; set; }
        static int SimulatedDays { get; set; }

        static bool Running = true;
        public static List<string> LastBoatArrivals { get; set; }
        public static List<string> LastBoatDepartures { get; set; }

        static int HarborDocks { get; set; }

        public void Run()
        {
            Console.SetBufferSize(200, 200);
            SaveLoadMenu();
            while (Running)
            {
                Console.WriteLine("[Enter] för att forstätta\n[Esc] för att avsluta");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        LastBoatDepartures.Clear();
                        ProceedSimulation(myHarbor);
                        break;

                    case ConsoleKey.Escape:
                        CloseSimulation();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SaveLoadMenu()
        {
            Console.WriteLine("[L] för att ladda en sparad simulering.");
            Console.WriteLine("[N] för att skapa en ny simulering.");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.L:
                    Console.Clear();
                    LoadSaveData();
                    DisplayHarborInfo();
                    break;
                case ConsoleKey.N:
                    Console.Clear();
                    StartNewSimulation();
                    break;
                default:
                    break;
            }
        }

        private static void StartNewSimulation()
        {
            myHarbor = new Harbor();
            SaveSystem.SetNewHarbor(myHarbor);           
            DockedBoats = new List<Boat>();
            RejectedBoatsList = new List<Boat>();
            LastBoatArrivals = new List<string>();
            LastBoatDepartures = new List<string>();
            OccupiedSlots = 0;
            SimulatedDays = 0;
            HarborDocks = myHarbor.Docks * 2;

        }

        private static void LoadSaveData()
        {
            HarborData loadedSim = SaveSystem.LoadData();

            SimulatedDays = loadedSim.DaysSimulatedSave;
            OccupiedSlots = loadedSim.OccupiedSlotsSave;
            LastBoatArrivals = loadedSim.LastBoatArrivalsSave;
            LastBoatDepartures = loadedSim.LastBoatDeparturesSave;
            DockedBoats = loadedSim.DockedBoatsSave;
            RejectedBoatsList = loadedSim.RejectedBoatsSave;
            myHarbor = loadedSim.HarborSave;
            myHarbor.SlotList = loadedSim.HarborDockListSave;
            HarborDocks = myHarbor.Docks * 2;
        }

        private void CloseSimulation()
        {
            Running = false;
        }

        private void ProceedSimulation(Harbor harbor)
        {
            SimulatedDays++;
            HandleDepartures();
            LastBoatArrivals.Clear();
            for (int b = 0; b < BoatsADay; b++)
            {
                Boat newBoat = GetBoat((BoatType)Utils.Rng.Next(1, 5));
                AssignBoat(newBoat, harbor.SlotList);
                //HandleArrivals(newBoat, harbor.HarborDockList);
            }
            DisplayHarborInfo();
            SaveSimulation();
        }

        private void SaveSimulation()
        {
            HarborData saveSimulation = new HarborData(SimulatedDays, OccupiedSlots, LastBoatArrivals, LastBoatDepartures, DockedBoats, RejectedBoatsList, myHarbor, myHarbor.SlotList);
            SaveSystem.SaveData(saveSimulation);
        }

        private static void DisplayHarborInfo()
        {
            //DisplayDockList();
            DisplayLogistics();
            DisplayDepartures();
            DisplayStats();
        }

        private static void DisplayLogistics()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Day {SimulatedDays}");
            Console.WriteLine();
            Console.SetCursorPosition(0, 3);

            //foreach (var message in LastBoatArrivals)
            //{
            //    Console.WriteLine($"{message}");
            //}

            for (int i = 0; i < LastBoatArrivals.Count; i++)
            {
                Console.WriteLine(LastBoatArrivals.ElementAt(i));
                if (i % 2 == 1)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            string[] headers = { "Dock", "Boat Type", "Reg-Nr", "Weight", "Max Speed (Km/h)", "Misc." };
            for (int i = 0; i < headers.Length; i++)
            {
                Utils.HeaderPlacer(20, i, headers[i]);
            }
            Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------");

            int row = 23;
            var harborSlotList = myHarbor.SlotList.OrderBy(s => s.SlotNrID);
            var dockedBoatsList = DockedBoats.OrderBy(b => b.DockNr);
            var availableDocks = harborSlotList.Where(s => s.slotEmpty == true);
            foreach (var boat in dockedBoatsList)
            {
                
                Utils.InfoPlacer(row, boat);
                row++;
            }
            Console.SetCursorPosition(0, row += 3);
            Console.WriteLine("****** Empty Docks *******");
            row += 3;
            foreach (var slot in availableDocks)
            {

                Utils.EmptyInfoPlacer(row , "Empty", slot);
                Console.WriteLine();
                row++;
            }
            
            
        }

        //private static void DisplayDockList()
        //{
        //    Console.SetCursorPosition(0, 0);
        //    Console.WriteLine($"Day {SimulatedDays}");
        //    Console.WriteLine();
        //    Console.SetCursorPosition(0, 3);
        //    foreach (var message in LastBoatArrivals)
        //    {
        //        Console.WriteLine(message);
        //    }
        //    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

        //    string[] headers = { "Dock", "Boat Type", "Reg-Nr", "Weight", "Max Speed (Km/h)", "Misc." };
        //    for (int i = 0; i < headers.Length; i++)
        //    {
        //        Utils.HeaderPlacer(20, i, headers[i]);
        //    }
        //    Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------");

        //    IEnumerable<Boat> query1 = DockedBoats.OrderBy(b => b.DockID);

        //    IEnumerable<Slot> query2 = myHarbor.SlotList.OrderBy(s => s.SlotNrID);

        //    foreach (var slot in query2)
        //    {
        //        int col = 23;
        //        if (slot.slotEmpty == true)
        //        {
        //            Utils.EmptyInfoPlacer(col + slot.SlotNrID, "Empty", slot);
        //        }
        //        else if (slot.slotEmpty == false)
        //        {
        //            if (slot.SlotNrID < DockedBoats.Count)
        //            {
        //                for (int i = 0; i < 6; i++)
        //                {
        //                    Utils.InfoPlacer(col + slot.SlotNrID, query1.ElementAt(slot.SlotNrID));
        //                }
        //            }
        //        }
        //        Console.WriteLine();
        //    }
        //}

        private static void DisplayDepartures()
        {
            int cursorPosX = 140;
            int cursorPosY = 3;

            foreach (var message in LastBoatDepartures)
            {
                Console.SetCursorPosition(cursorPosX, cursorPosY++);
                Console.WriteLine(message);
            }
        }

        private static void DisplayStats()
        {
            var rowboatCount = DockedBoats.Count(b => b.Type == BoatType.Rowboat);
            var motorboatCount = DockedBoats.Count(b => b.Type == BoatType.Motorboat);
            var sailboatCount = DockedBoats.Count(b => b.Type == BoatType.Sailboat);
            var cargoshipCount = DockedBoats.Count(b => b.Type == BoatType.Cargoship);

            var totalBoatWeight = DockedBoats.Sum(b => b.Weight);
            var averageMaxSpeed = Math.Round((DockedBoats.Sum(b => b.MaxSpeed) / DockedBoats.Count) * 1.852, 2);

            var availableDocks = myHarbor.SlotList.Where(d => d.slotEmpty == true);
            var rejectedBoats = RejectedBoatsList.Count;

            Console.SetCursorPosition(0, 70);

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Amount of rowboats docked: {rowboatCount}");
            Console.WriteLine($"Amount of motorboats docked: {motorboatCount}");
            Console.WriteLine($"Amount of sailboats docked: {sailboatCount}");
            Console.WriteLine($"Amount of cargoships docked: {cargoshipCount}");
            Console.WriteLine();
            Console.WriteLine($"Total weight of all docked vessels: {totalBoatWeight} Kg");
            Console.WriteLine($"Average maximum speed of {averageMaxSpeed} Km/h");
            Console.WriteLine($"Number of available docks: {availableDocks.Count()}");
            Console.WriteLine($"Number of boats rejected: {rejectedBoats}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
        }

        private void HandleDepartures()
        {
            foreach (Boat boat in DockedBoats)
            {
                boat.DaysStaying--;
            }
            if (DockedBoats.Any(b => b.DaysStaying <= 0))
            {
                foreach (var boat in DockedBoats)
                {
                    if (boat.DaysStaying <= 0)
                    {
                        Slot slot = myHarbor.SlotList.ElementAt(boat.DockNr);
                        slot.slotEmpty = true;
                        slot.Capacity -= boat.DockingSize;
                        OccupiedSlots -= boat.DockingSize;
                        string boatHasDepartedMsg = $"{boat.BoatID} lämnar hamnen";
                        Utils.AddNewMessage(boatHasDepartedMsg, LastBoatDepartures);
                    }
                }
                DockedBoats.RemoveAll(b => b.DaysStaying <= 0);             
            }
        }

        private void AssignBoat(Boat boat, List<Slot> harborSlotList)
        {
            string newArrivalMsg = $"A {boat.Type} with the ID: {boat.BoatID} has just arrived to {myHarbor.Name} Harbor.";

            var normalBoatDocks = harborSlotList.Where(s => s.Type == SlotType.NormalBoatDock);
            var sailboatDocks = harborSlotList.Where(s => s.Type == SlotType.SailboatDock);
            var cargoshipDocks = harborSlotList.Where(s => s.Type == SlotType.CargoshipDock);

            if (OccupiedSlots + boat.DockingSize < HarborDocks)
            {                      
                Utils.AddNewMessage(newArrivalMsg, LastBoatArrivals);
                
                switch (boat.Type)
                {
                    case BoatType.Rowboat:
                        if (normalBoatDocks.Any(s => s.slotEmpty == true))
                        {
                            Slot slot = normalBoatDocks.First(s => s.slotEmpty == true);
                            boat.DockID = slot.DockID;
                            boat.DockNr = slot.SlotNrID;
                            slot.Capacity += boat.DockingSize;
                            if(slot.Capacity >= slot.SlotSize)
                            {
                                slot.slotEmpty = false;
                            }
                            string boatWasDockedMsg = $"Space was available: {boat.Type} {boat.BoatID} was docked.  Docked at: Dock {boat.DockID}";

                            Utils.AddNewMessage(boatWasDockedMsg, LastBoatArrivals);
                            DockedBoats.Add(boat);
                            OccupiedSlots += boat.DockingSize;
                        }
                        else
                        {
                            RejectBoat(boat);
                        }
                        //boat.StringDockID = harborSlotList.First(s => s.Type == SlotType.NormalBoatDock && s.slotEmpty == true).DockID;
                        break;

                    case BoatType.Motorboat:
                        if (normalBoatDocks.Any(s => s.Capacity == 0 && s.slotEmpty == true))
                        {
                            Slot slot = normalBoatDocks.First(s => s.Capacity == 0 && s.slotEmpty == true);
                            boat.DockID = slot.DockID;
                            boat.DockNr = slot.SlotNrID;
                            
                            slot.Capacity += boat.DockingSize;
                            slot.slotEmpty = false;

                            string boatWasDockedMsg = $"Space was available: {boat.Type} {boat.BoatID} was docked.  Docked at: Dock {boat.DockID}";
                            Utils.AddNewMessage(boatWasDockedMsg, LastBoatArrivals);
                            DockedBoats.Add(boat);
                            OccupiedSlots += boat.DockingSize;
                        }
                        else
                        {
                            RejectBoat(boat);
                        }
                        break;

                    case BoatType.Sailboat:
                        if (sailboatDocks.Any(s => s.slotEmpty == true))
                        {
                            Slot slot = sailboatDocks.First(s => s.slotEmpty == true);
                            boat.DockID = slot.DockID;
                            boat.DockNr = slot.SlotNrID;
                            slot.Capacity += boat.DockingSize;
                            slot.slotEmpty = false;

                            string boatWasDockedMsg = $"Space was available: {boat.Type} {boat.BoatID} was docked.  Docked at: Dock {boat.DockID}";
                            Utils.AddNewMessage(boatWasDockedMsg, LastBoatArrivals);
                            DockedBoats.Add(boat);
                            OccupiedSlots += boat.DockingSize;
                        }
                        else
                        {
                            RejectBoat(boat);
                        }
                        break;
                    case BoatType.Cargoship:
                        if (cargoshipDocks.Any(s => s.slotEmpty == true))
                        {
                            Slot slot = cargoshipDocks.First(s => s.slotEmpty == true);
                            boat.DockID = slot.DockID;
                            boat.DockNr = slot.SlotNrID;
                            slot.Capacity += boat.DockingSize;
                            slot.slotEmpty = false;

                            string boatWasDockedMsg = $"Space was available: {boat.Type} {boat.BoatID} was docked.  Docked at: Dock {boat.DockID}";
                            Utils.AddNewMessage(boatWasDockedMsg, LastBoatArrivals);
                            DockedBoats.Add(boat);
                            OccupiedSlots += boat.DockingSize;
                        }
                        else
                        {
                            RejectBoat(boat);
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }

        private void RejectBoat(Boat boat)
        {
            string boatWasRejectedMsg = $"Space was NOT available: {boat.Type} {boat.BoatID} was rejected from docking at {myHarbor.Name}";
            Utils.AddNewMessage(boatWasRejectedMsg, LastBoatArrivals);          
            RejectedBoatsList.Add(boat);
        }

        public Boat GetBoat(BoatType boatType)
        {
            switch (boatType)
            {
                case BoatType.Rowboat:
                    var newRowboat = new Rowboat();
                    newRowboat.Type = boatType;
                    return newRowboat;
                case BoatType.Motorboat:
                    var newMotorboat = new Motorboat();
                    newMotorboat.Type = boatType;
                    return newMotorboat;
                case BoatType.Sailboat:
                    var newSailboat = new Sailboat();
                    newSailboat.Type = boatType;
                    return newSailboat;
                case BoatType.Cargoship:
                    var newCargoship = new Cargoship();
                    newCargoship.Type = boatType;
                    return newCargoship;
                default:
                    return null;
            }
        }
    }
}
