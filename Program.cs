// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;




namespace ProjetEvents
{
    public class Program
    {

        static void Main()
        {
            GetEvents();
        }

        /// <summary>
        /// Récupère, désérialise, énumère les différents events 
        /// </summary>
        public static void GetEvents()
        {
            string file = @"C:\Users\AnaïsTORRES\source\repos\ProjetEvents\eventdescriptors.json";
            string fichierEvents = File.ReadAllText(file);

            if (fichierEvents == null)
                Console.Write("Pas de fichier json");

            List<EventContentRoot> eventsRoot = JsonSerializer.Deserialize<List<EventContentRoot>>(fichierEvents);


            foreach (EventContentRoot e in eventsRoot)
            {
                foreach (EventContentCategory c in e.Categories)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" " + c.Name);

                    foreach (EventGroup g in c.Groups)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" -- " + g.Name);

                        foreach (EventDetails d in g.Events)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" ---- " + d.EventName);


                            //Création dossier
                            string path = (c.Name + "/" + g.Name + "/" + d.EventName).Trim().ToLower().Normalize();
                            Console.WriteLine("path : " + path);


                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

}