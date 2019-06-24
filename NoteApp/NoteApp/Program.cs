using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace NoteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory(NoteDirectory);
            CommandAvailable();
            Console.WriteLine("Ready! ");
            Console.Write("Your command:  ");
            ReadCommand();
            Console.ReadLine();
        }

        private static string NoteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\Notes\";
        private static void ReadCommand()
        {
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "read":
                    ReadNote();
                    Main(null);
                    break;
                case "new":
                    WriteNote();
                    Main(null);
                    break;
                case "edit":
                    EditNote();
                    Main(null);
                    break;
                case "show":
                    ShowNote();
                    Main(null);
                    break;
                case "delete":
                    DeleteNote();
                    Main(null);
                    break;
                case "dir":
                    Console.WriteLine("Open Note directory !!!");
                    ShowNoteDirectory();
                    Main(null);
                    break;
                case "cls":
                    Console.Clear();
                    Main(null);
                    break;
                case "end":
                    Exit();
                    break;
                default:
                    Main(null);
                    break;
            }
        }

        private static void WriteNote()
        {
            try
            {
                Console.WriteLine("Enter note: ");
                string input = Console.ReadLine();
                Console.WriteLine("Enter file name: ");
                string filename = Console.ReadLine() + ".txt";
                // write the text file
                using (StreamWriter writer = File.CreateText(NoteDirectory + filename))
                {
                    writer.WriteLine(input);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private static void EditNote()
        {
            Console.WriteLine("Please enter file name:  ");
            string fileName = Console.ReadLine().ToLower() + ".txt";
            Console.WriteLine("Content: ");
            if (File.Exists(NoteDirectory + fileName))
            {
                try
                {
                    List<string> lines = File.ReadAllLines(NoteDirectory + fileName).ToList();
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("Input more text: ");
                    string input = Console.ReadLine();
                    lines.Add(input);
                    File.WriteAllLines(NoteDirectory + fileName, lines);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist !");
            }
        }

        private static void ReadNote()
        {
            Console.WriteLine("Enter file name you wish to read: ");
            string readfilename = Console.ReadLine().ToLower() + ".txt";
            Console.WriteLine("Content: ");
            String line;
            if(File.Exists(NoteDirectory + readfilename))
            {
                try
                {
                    StreamReader sr = new StreamReader(NoteDirectory + readfilename);
                    line = sr.ReadLine();

                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist !");
            }
            
        }

        private static void DeleteNote()
        {
            Console.WriteLine("Enter file name you wish to delete: ");
            string filename = Console.ReadLine().ToLower() + ".txt";
            if (File.Exists(NoteDirectory + filename))
            {
                Console.Write("Are you sure you wish to delete this file?? Y/N: ");
                string confirmation = Console.ReadLine().ToLower();
                if(confirmation == "y")
                {
                    try
                    {
                        File.Delete(NoteDirectory + filename);
                        Console.WriteLine("File has been deleted !");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " +e.Message);
                    }
                }
                else if (confirmation == "n")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Invalid command, please re-enter!");
                    DeleteNote();
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist !");
            }
        }

        private static void ShowNote()
        {
            string noteLocation = NoteDirectory;
            DirectoryInfo dir = new DirectoryInfo(noteLocation);
            try
            {
                if(Directory.Exists(noteLocation))
                {
                    FileInfo[] noteFiles = dir.GetFiles("*.txt");
                    if(noteFiles.Count() != 0)
                    {
                        Console.WriteLine("+------------+");
                        foreach (var item in noteFiles)
                        {
                            Console.WriteLine("   " + item.Name);
                        }
                        Console.WriteLine("+------------+");
                    }
                    else
                    {
                        Console.WriteLine("No notes found !");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
        private static void CommandAvailable()
        {
            Console.WriteLine("  end - Close the application\n" +
                "  new - Write a new note\n" +
                "  read - Read a note\n" +
                "  edit - Edit note\n" +
                "  delete - Delete a note"+
                "  show - Show all notes\n" +
                "  dir - Open note directory\n" +
                "  cls - Clear screen\n");
        }
        private static void ShowNoteDirectory()
        {
            Process.Start("explorer.exe", NoteDirectory);
        }
    }
}




