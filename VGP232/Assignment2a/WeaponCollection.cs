using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment2a
{
    public class WeaponCollection : List<Weapon>, IPeristence
    {
        private List<Weapon> weapons = new List<Weapon>();
        
        public int Size()
        {
            return weapons.Count;
        }

        public int GetHighestBaseAttack()
        {
            if (weapons.Count == 0)
            {
                return 0;
            }
            int highest = weapons[0].BaseAttack;
            foreach (Weapon weapon in weapons)
            {
                highest = Math.Max(highest, weapon.BaseAttack);
            }
            return highest;
        }

        public int GetLowestBaseAttack()
        {
            if (weapons.Count == 0)
            {
                return 0;
            }
            int lowest = weapons[0].BaseAttack;

            foreach (Weapon weapon in weapons)
            {
                lowest = Math.Min(lowest, weapon.BaseAttack);
            }
            return lowest;
        }

        public List<Weapon> GetAllWeaponsOfType(WeaponType type)
        {
            return weapons.FindAll(x => x.Type == type);
        }

        public List<Weapon> GetAllWeaponsOfRarity(int stars)
        {
            return weapons.FindAll(x => x.Rarity == stars);
        }

        public void SortBy(string columnName)
        {
            columnName = columnName.ToLower();

            if (columnName == "name")
            {
                // Sorts the list based off of the Weapon name.
                weapons.Sort(Weapon.CompareByName);
            }
            else if (columnName == "type")
            {
                // Sorts the list based off of the Weapon Type.
                weapons.Sort(Weapon.CompareByType);
            }
            else if (columnName == "rarity")
            {
                // Sorts the list based off of the Weapon Rarity.
                weapons.Sort(Weapon.CompareByRarity);
            }
            else if (columnName=="baseattack")
            {
                // Sorts the list based off of the Weapon BaseAttack.
                weapons.Sort(Weapon.CompareByBaseAttack);
            }
            else if(columnName=="image")
            {
                // Sorts the list based off of the Weapon Image.
                weapons.Sort(Weapon.CompareByImage);
            }
            else if (columnName == "secondarystat")
            {
                // Sorts the list based off of the Weapon SecondaryStat.
                weapons.Sort(Weapon.CompareBySecondaryStat);
            }
            else if (columnName == "passive")
            {
                // Sorts the list based off of the Weapon Passive.
                weapons.Sort(Weapon.CompareByPassive);
            }
            else
            {
                Console.WriteLine("The column name specified does not exist");
            }

        }

        // check if sorted by given column
        public void test()
        {
            Console.WriteLine(weapons[0].BaseAttack);
            Console.WriteLine(weapons[1].BaseAttack);
            Console.WriteLine(weapons[2].BaseAttack);
            Console.WriteLine(weapons[3].BaseAttack);
            Console.WriteLine(weapons[4].BaseAttack);
        }

        public bool Load(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No input file specified");
                weapons.Clear();
                return false;
            }
            else if (!File.Exists(filename))
            {
                Console.WriteLine("The file specified does not exist");
                weapons.Clear();
                return false;
            }
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = reader.ReadLine();
                int lineNumber = 1;
                Weapon theWeapon = new Weapon();
                while (reader.Peek() > 0)
                {
                    line = reader.ReadLine();
                    if (Weapon.TryParse(line, out theWeapon))
                    {
                        //Console.WriteLine(theWeapon);
                        weapons.Add(theWeapon);
                    }
                    else
                    {
                        Console.WriteLine("Unable to parse line {0}", lineNumber);
                        weapons.Clear();
                        return false;
                    }
                    ++lineNumber;
                }
                return true;
            }
        }

        public bool Save(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                FileStream fs = File.Open(filename, FileMode.Create);

                // opens a stream writer with the file handle to write to the output file.
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Hint: use writer.WriteLine
                    // TODO: write the header of the output "Name,Type,Rarity,BaseAttack"   `
                    writer.WriteLine("Name,Type,Image,Rarity,BaseAttack,SecondaryStat,Passive");

                    // TODO: use the writer to output the results.
                    weapons.ForEach(writer.WriteLine);

                    // TODO: print out the file has been saved.
                    Console.WriteLine("The file has been saved");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("No output file provided");
                return false;
            }
        }




    }
}
