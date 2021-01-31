using System;
using System.Collections.Generic;
using System.IO;

// TODO: Fill in your name and student number.
// Assignment 1
// NAME: Zixiao Wang
// STUDENT NUMBER: 2022599
// Marks: 99/100
// Comments: Great job!
// You can search for these comments by going to each file and type Ctrl + F and search for TODO_COMMENT or TODO ERROR.

namespace Assignment2a
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Variables and flags

            // The path to the input file to load.
            string inputFile = string.Empty;

            // The path of the output file to save.
            string outputFile = string.Empty;

            // The flag to determine if we overwrite the output file or append to it.
            bool appendToFile = false;

            // The flag to determine if we need to display the number of entries
            bool displayCount = false;

            // The flag to determine if we need to sort the results via name.
            bool sortEnabled = false;

            // The column name to be used to determine which sort comparison function to use.
            string sortColumnName = string.Empty;

            // The results to be output to a file or to the console
            WeaponCollection results = new WeaponCollection();

            for (int i = 0; i < args.Length; i++)
            {
                // h or --help for help to output the instructions on how to use it
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");

                    // TODO: include help info for count
                    //"-c or --count : displays the number of entries in the input file (optional).";
                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional)");

                    // TODO: include help info for append
                    //"-a or --append : enables append mode when writing to an existing output file (optional)";
                    Console.WriteLine("-a or --append : enables append mode when writing to an existing output file (optional)");

                    // TODO: include help info for sort
                    //"-s or --sort <column name> : outputs the results sorted by column name";
                    Console.WriteLine("-s or --sort <column name> : outputs the results sorted by column name");

                    break;
                }
                else if (args[i] == "-i" || args[i] == "--input")
                {
                    // Check to make sure there's a second argument for the file name.
                    if (args.Length > i + 1)
                    {
                        // stores the file name in the next argument to inputFile
                        ++i;
                        inputFile = args[i];

                        if (!results.Load(inputFile))
                        {
                            Console.WriteLine("Fail to load data from file {0}", inputFile);
                            return;
                        }
                    }
                }
                else if (args[i] == "-s" || args[i] == "--sort")
                {
                    // TODO: set the sortEnabled flag and see if the next argument is set for the column name
                    // TODO: set the sortColumnName string used for determining if there's another sort function.

                    // Check to make sure there's a second argument for the sort column.
                    if (args.Length > i + 1)
                    {
                        // stores the column name in the next argument
                        ++i;
                        sortColumnName = args[i].ToLower();

                        if (string.IsNullOrEmpty(sortColumnName))
                        {
                            Console.WriteLine("No column name specified");
                        }
                        else if (sortColumnName == "name" || sortColumnName == "type" || sortColumnName == "rarity" || sortColumnName == "baseattack" || sortColumnName == "image" || sortColumnName == "secondarystat" || sortColumnName == "passive")
                        {
                            sortEnabled = true;
                        }
                        else
                        {
                            sortEnabled = false;
                            sortColumnName = string.Empty;
                            Console.WriteLine("The column name specified does not exist");
                        }
                    }
                }
                else if (args[i] == "-c" || args[i] == "--count")
                {
                    displayCount = true;
                }
                else if (args[i] == "-a" || args[i] == "--append")
                {
                    // TODO: set the appendToFile flag
                    appendToFile = true;
                }
                else if (args[i] == "-o" || args[i] == "--output")
                {
                    // validation to make sure we do have an argument after the flag
                    if (args.Length > i + 1)
                    {
                        // increment the index.
                        ++i;
                        string filePath = args[i];
                        if (string.IsNullOrEmpty(filePath))
                        {
                            // TODO: print No output file specified.
                            Console.WriteLine("No output file specified");
                        }
                        else
                        {
                            // TODO: set the output file to the outputFile
                            outputFile = filePath;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The argument Arg[{0}] = [{1}] is invalid", i, args[i]);
                }
            }

            if (sortEnabled)
            {
                Console.WriteLine("Sorting by {0}", sortColumnName);
                results.SortBy(sortColumnName);
            }

            if (displayCount)
            {
                Console.WriteLine("There are {0} entries", results.Size());
            }

            results.Save(outputFile);
            Console.WriteLine("Done!");
        }
    }
}
