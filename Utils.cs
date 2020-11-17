using Hamnen;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hamnen
{
    [System.Serializable]
    class Utils
    {
        public static Random Rng = new Random();
        
        public static void HeaderPlacer(int row, int i, string header)
        {
            switch (i)
            {
                case 0:
                    Console.SetCursorPosition(0, row);
                    Console.Write($"{header}");
                    break;

                case 1:
                    Console.SetCursorPosition(20, row);
                    Console.Write($"{header}");

                    break;
                case 2:
                    Console.SetCursorPosition(40, row);
                    Console.Write($"{header}");
                    break;

                case 3:
                    Console.SetCursorPosition(60, row);
                    Console.Write($"{header}");
                    break;

                case 4:
                    Console.SetCursorPosition(80, row);
                    Console.Write($"{header}");
                    break;

                case 5:
                    Console.SetCursorPosition(100, row);
                    Console.Write($"{header}");
                    break;

                default:
                    break;
            }
        }

        public static void EmptyInfoPlacer(int row, string header, Slot slot)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.SetCursorPosition(0, row);
                        Console.Write($"{slot.DockID}");
                        break;

                    case 1:
                        Console.SetCursorPosition(20, row);
                        Console.Write($"{header}");

                        break;
                    case 2:
                        Console.SetCursorPosition(40, row);
                        Console.Write($"{header}");
                        break;

                    case 3:
                        Console.SetCursorPosition(60, row);
                        Console.Write($"{header}");
                        break;

                    case 4:
                        Console.SetCursorPosition(80, row);
                        Console.Write($"{header}");
                        break;

                    case 5:
                        Console.SetCursorPosition(100, row);
                        Console.Write($"{header}");
                        break;

                    default:
                        break;
                }
            }
        }

        public static void InfoPlacer(int row, Boat boat)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.SetCursorPosition(0, row);
                        Console.Write($"{boat.DockID}");
                        break;
                    case 1:
                        Console.SetCursorPosition(20, row);
                        Console.Write($"{boat.Type}");

                        break;
                    case 2:
                        Console.SetCursorPosition(40, row);
                        Console.Write($"{boat.BoatID}");

                        break;
                    case 3:
                        Console.SetCursorPosition(60, row);
                        Console.Write($"{boat.Weight} kg");
                        break;
                    case 4:
                        Console.SetCursorPosition(80, row);
                        Console.Write($"{Math.Round(boat.MaxSpeed * 1.852, 2)} km/h");
                        break;
                    case 5:
                        Console.SetCursorPosition(100, row);
                        Console.Write($"{boat.UniqueProperty()}");
                        break;
                    default:
                        break;
                }
            }         
        }

        internal static void AddNewMessage(string newMessage, List<string> newMessageList)
        {
            newMessageList.Add(newMessage);
        }
    }
}
