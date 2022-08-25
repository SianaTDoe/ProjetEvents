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
        /// Récupère, désérialise, énumère les events
        /// Vérifie les anomalies (doublons et types non existants)
        /// 
        /// </summary>
        public static void GetEvents()
        {
            string file = @"C:\Users\AnaïsTORRES\source\repos\ProjetEvents\eventdescriptors.json";
            string fichierEvents = File.ReadAllText(file);

            if (fichierEvents == null)
                Console.Write("Pas de fichier json");

            List<EventContentRoot> eventsRoot = JsonSerializer.Deserialize<List<EventContentRoot>>(fichierEvents);

            List<string> listeTypes = new List<string>
            {
                "bool",
                "bool?",
                "byte",
                "byte?",
                "sbyte",
                "sbyte?",
                "char",
                "char?",
                "decimal",
                "decimal?",
                "double",
                "double?",
                "float",
                "float?",
                "int",
                "int?",
                "uint",
                "uint?",
                "nint",
                "nint?",
                "long",
                "long?",
                "ulong",
                "ulong?",
                "short",
                "short?",
                "ushort",
                "ushort?",
                "string",
                "Guid",
                "Guid?",
                "DateTime",
                "DateTime?",
                "DateTimeOffset",
                "DateTimeOffset?",
                "null"
            };

            //Récupération des events
            List<string> listeEvents = new List<string>();

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
                            Console.WriteLine(" ---- " + d.Code);

                            string path = (c.Name + "/" + g.Name + "/" + d.Code).ToLower().Trim();

                            //Vérification des doublons
                            if (listeEvents.Contains(path) == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Doublon");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                listeEvents.Add(path);
                                Console.WriteLine(path);
                            }

                            //Récupération des types définis
                            foreach (AideContentDataObject t in d.Types)
                            {
                                string[] ty = t.Declaration.Split(" ");
                                string[] typ = ty[1].ToString().Split("\r");
                                string typeDefini = typ[0];
                                listeTypes.Add(typeDefini);

                                foreach (AideContentDataObjectMember m in t.Details.Members)
                                {
                                    //Vérification des types (si définis ou pas)
                                    if (listeTypes.Contains(m.FullTypeName))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(m.FullTypeName);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Le type n'est pas défini");
                                    }
                                }
                            }
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

}