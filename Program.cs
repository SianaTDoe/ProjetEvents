// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                "System.Int32",
                "System.Int32?",
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
                null
            };

            // Récupération des events
            List<string> listeEvents = new List<string>();

            foreach (EventContentRoot e in eventsRoot)
            {
                foreach (EventContentCategory c in e.Categories)
                {
                    c.Name = ClearString(c.Name);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" " + c.Name);

                    foreach (EventGroup g in c.Groups)
                    {
                        g.Name = ClearString(g.Name);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" -- " + g.Name);

                        foreach (EventDetails d in g.Events)
                        {
                            d.Code = ClearString(d.Code);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" ---- " + d.Code);

                            // Création du chemin 
                            string path = c.Name + "." + g.Name + "." + d.Code;

                            // Vérification des doublons
                            if (listeEvents.Contains(path) == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Event en double");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                listeEvents.Add(path);
                                Console.WriteLine(path);
                            }

                            // Récupération des types définis
                            foreach (AideContentDataObject t in d.Types)
                            {
                                string[] ty = t.Declaration.Split(" ");
                                string[] typ = ty[1].ToString().Split("\r");
                                listeTypes.Add(typ[0]);
                                listeTypes.Add(typ[0] + "[]");
                                listeTypes.Add(t.ClassName);
                                listeTypes.Add(t.ClassName + "[]");

                                foreach (AideContentDataObjectMember m in t.Details.Members)
                                {
                                    // Vérification des types (si définis ou pas)
                                    if (listeTypes.Contains(m.FullTypeName))
                                    {
                                        //Console.ForegroundColor = ConsoleColor.Green;
                                        //Console.WriteLine(m.FullTypeName);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Type non défini" + m.FullTypeName);
                                    }

                                    //Création dossier, sous-dossier, fichier
                                    GenerateFileAndFolder(c.Name, g.Name, d.Code);
                                }
                            }
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Enlève les espaces vides et les accents d'une string
        /// </summary>
        /// <param name="str">La string à clean</param>
        /// <returns>La string nettoyée</returns>
        public static string ClearString(string str)
        {
            if (str == null)
                return null;

            str = Regex.Replace(str, @"[ÀÄÂ]", "A");
            str = Regex.Replace(str, @"[Ààâä]", "a");
            str = Regex.Replace(str, @"[Ç]", "C");
            str = Regex.Replace(str, @"[ç]", "c");
            str = Regex.Replace(str, @"[ÊÉÈ]", "E");
            str = Regex.Replace(str, @"[éèê]", "e");
            str = Regex.Replace(str, @"[ÏÎ]", "i");
            str = Regex.Replace(str, @"[îï]", "i");
            str = Regex.Replace(str, @"[ÔÖ]", "o");
            str = Regex.Replace(str, @"[öô]", "o");
            str = Regex.Replace(str, @"[Ù]", "U");
            str = Regex.Replace(str, @"[Ù]", "u");

            return str.Trim();
        }

        /// <summary>
        /// Crée un dossier, un sous-dossier et un fichier 
        /// </summary>
        /// <param name="dossier"></param>
        /// <param name="sousDossier"></param>
        /// <param name="fichier"></param>
        public static void GenerateFileAndFolder(string dossier, string sousDossier, string fichier)
        {
            //Création dossier
            string folderName = @"C:\Users\AnaïsTORRES\source\repos\ProjetEvents\ProjetEvents\" + dossier;
            Directory.CreateDirectory(folderName);

            //Création sous-dossier
            string subFolderName = Path.Combine(folderName, sousDossier);
            Directory.CreateDirectory(subFolderName);

            //Création fichier
            string fileName = Path.Combine(subFolderName, fichier + ".cs");
            if (!File.Exists(fileName))
            {
                //Création et remplissage fichier
                StreamWriter file = new StreamWriter(fileName);

                //string constUsing = "using";
                //file.Write("test");
                file.Close();
            }
            else
            {
                // Fichier déjà existant
            }
            
        }
    }

}