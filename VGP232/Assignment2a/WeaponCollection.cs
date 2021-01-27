using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Assignment2a
{
    [Serializable]
    public class WeaponCollection : List<Weapon>, IPeristence, ICsvSerializable, IXmlSerializable, IJsonSerializable
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

        public bool Load(string filename)
        {
            weapons.Clear();

            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No input file specified");
                return false;
            }

            if (!File.Exists(filename))
            {
                Console.WriteLine("The file specified does not exist");
                return false;
            }

            string fileExtension;
            try
            {
                fileExtension = Path.GetExtension(filename);
            }
            catch (Exception)
            {
                Console.WriteLine("The path provided is invalid");
                return false;
            }

            switch (fileExtension)
            {
                case ".csv":
                    return LoadCSV(filename);
                case ".XML":
                    return LoadXML(filename);
                case ".json":
                    return LoadJSON(filename);
                default:
                    Console.WriteLine("Error to load data. Unsupported file extension");
                    return false;
            }
        }

        public bool Save(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No input file specified");
                return false;
            }

            string fileExtension;
            try
            {
                fileExtension = Path.GetExtension(filename);
            }
            catch (Exception)
            {
                Console.WriteLine("The path provided is invalid");
                return false;
            }
            switch (fileExtension)
            {
                case ".csv":
                    return SaveAsCSV(filename);
                case ".XML":
                    return SaveAsXML(filename);
                case ".json":
                    return SaveAsJSON(filename);
                default:
                    Console.WriteLine("Error to save data. Unsupported file extension");
                    return false;
            }
        }

        public bool LoadCSV(string path)
        {
            weapons.Clear();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                int lineNumber = 1;
                Weapon theWeapon = new Weapon();
                while (reader.Peek() > 0)
                {
                    line = reader.ReadLine();
                    if (Weapon.TryParse(line, out theWeapon))
                    {
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
                Console.WriteLine("Load data from csv file");
                return true;
            }
        }
        public bool SaveAsCSV(string path)
        {
            try
            {
                if (File.Exists((path)))
                {
                    FileStream fs = File.Open(path, FileMode.Append);
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        weapons.ForEach(writer.WriteLine);
                    }
                }
                else
                {
                    FileStream fs = File.Open(path, FileMode.Create);
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine("Name,Type,Image,Rarity,BaseAttack,SecondaryStat,Passive");
                        weapons.ForEach(writer.WriteLine);
                    }
                }
                Console.WriteLine("Save data to csv file");
                return true;
            }
            catch (Exception)
            { 
                Console.WriteLine("Error to save data to csv file");
                return false;
            }

        }
        public bool LoadXML(string path)
        {
            weapons.Clear();

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Weapon>));
                    weapons.Clear();
                    weapons = (List<Weapon>)xs.Deserialize(fs);
                }
                Console.WriteLine("Load data from XML file");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error to load data from XML file");
                weapons.Clear();
                return false;
            }

        }
        public bool SaveAsXML(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Weapon>));
                    xs.Serialize(fs, weapons);
                }

                Console.WriteLine("Save data to XML file");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error to save data to XML file");
                return false;
            }
        }
        public bool LoadJSON(string path)
        {
            weapons.Clear();

            try
            {
                using (StreamReader content = new StreamReader(path))
                {
                    string weaponAsJson = content.ReadToEnd();
                    weapons = JsonConvert.DeserializeObject<List<Weapon>>(weaponAsJson);
                }
                Console.WriteLine("Load data from json file");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error to load data from json file");
                weapons.Clear();
                return false;
            }
        }
        public bool SaveAsJSON(string path)
        {
            try
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, weapons);
                }
                Console.WriteLine("Save data to json file");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error to save data to json file");
                return false;
            }
        }
    }
}
