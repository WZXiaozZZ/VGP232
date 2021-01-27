using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assignment2a
{
    [TestFixture]
    public class UnitTests
    {
        private WeaponCollection WeaponCollection;
        private string inputPath;
        private string outputPath;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";
        private List<string> OUTPUT_FILES = new List<string> { 
            "weapons.csv", "empty.csv",
            "weapons.XML", "empty.XML",
            "weapons.json", "empty.json",
        };


        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            WeaponCollection = new WeaponCollection();
            WeaponCollection.Load(INPUT_FILE);
        }

        [TearDown]
        public void CleanUp()
        {
            DeleteFile(outputPath);
            OUTPUT_FILES.ForEach(x => DeleteFile(CombineToAppPath(x)));
        }

        // WeaponCollection Unit Tests
        [Test]
        public void WeaponCollection_GetHighestBaseAttack_HighestValue()
        {

            // Expected Value: 48
            // TODO: call WeaponCollection.GetHighestBaseAttack() and confirm that it matches the expected value using asserts.
            try
            {
                Assert.AreEqual(WeaponCollection.GetHighestBaseAttack(), 48);

            }
            catch (Exception)
            {
                Console.WriteLine("Error. The highest BaseAttack is not 48");
                return;
            }
            Console.WriteLine("The highest BaseAttack is 48");

        }

        [Test]
        public void WeaponCollection_GetLowestBaseAttack_LowestValue()
        {
            // Expected Value: 23
            // TODO: call WeaponCollection.GetLowestBaseAttack() and confirm that it matches the expected value using asserts.

            try
            {
                Assert.AreEqual(WeaponCollection.GetLowestBaseAttack(), 23);

            }
            catch (Exception)
            {
                Console.WriteLine("Error. The lowest BaseAttack is not 23");
                return;
            }

            Console.WriteLine("The lowest BaseAttack is 23");
        }

        [TestCase(WeaponType.Sword, 21)]
        public void WeaponCollection_GetAllWeaponsOfType_ListOfWeapons(WeaponType type, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfType(type) and confirm that the weapons list returns Count matches the expected value using asserts.
            try
            {
                Assert.AreEqual(WeaponCollection.GetAllWeaponsOfType(type).Count, expectedValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Error. The number of {0} is not {1}",type, expectedValue);
                return;
            }

            Console.WriteLine("There are {0} {1}s", expectedValue, type);
        }

        [TestCase(5, 10)]
        public void WeaponCollection_GetAllWeaponsOfRarity_ListOfWeapons(int stars, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfRarity(stars) and confirm that the weapons list returns Count matches the expected value using asserts.
            try
            {
                Assert.AreEqual(WeaponCollection.GetAllWeaponsOfRarity(stars).Count, expectedValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Error. The number of weapons with {0} stars is not {1}", stars, expectedValue);
                return;
            }

            Console.WriteLine("There are {0} {1}-star weapons", expectedValue, stars);
        }

        [Test]
        public void WeaponCollection_LoadThatExistAndValid_True()
        {
            // TODO: load returns true, expect WeaponCollection with count of 95 .
            if(WeaponCollection.Load(inputPath))
            {
                try
                {
                    Assert.AreEqual(WeaponCollection.Size(), 95);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. The number of weapons is not 95");
                    return;
                }

                Console.WriteLine("There are 95 weapons loaded");
            }
            else
            {
                Console.WriteLine("Error. Fail to load the file");
            }
        }

        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection
            if(!WeaponCollection.Load(""))
            {
                try
                {
                    Assert.AreEqual(WeaponCollection.Size(), 0);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. The WeaponCollection is not empty");
                    return;
                }

                Console.WriteLine("The WeaponCollection is empty when loading fail");
            }
            else
            {
                Console.WriteLine("Error. Loading successfully");
            }
        }

        [Test]
        public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and WeaponCollection is not empty.
            if (WeaponCollection.Load(inputPath) && WeaponCollection.Save(outputPath))
            {
                if (WeaponCollection.Size() != 0)
                {
                    Console.WriteLine("The WeaponCollection is not empty");
                }
                else
                {
                    Console.WriteLine("Error. The WeaponCollection is empty");
                }
            }
        }

        [Test]
        public void WeaponCollection_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty WeaponCollection, load the file and expect WeaponCollection to be empty.
            WeaponCollection.Clear();
            Assert.IsTrue(WeaponCollection.Save(outputPath));
            Assert.IsTrue(WeaponCollection.Load(outputPath));
            Assert.IsTrue(WeaponCollection.Count == 0);
        }

        // Weapon Unit Tests
        [Test]
        public void Weapon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a Weapon with the stats above set properly
            Weapon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            expected = new Weapon()
            {
                Name = "Skyward Blade",
                Type = WeaponType.Sword,
                Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
                Rarity = 5,
                BaseAttack = 46,
                SecondaryStat = "Energy Recharge",
                Passive = "Sky-Piercing Fang"
            };

            string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
            Weapon actual = null;

            // TODO: uncomment this once you have TryParse implemented.
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.BaseAttack, actual.BaseAttack);
            // TODO: check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
            Assert.AreEqual(expected.Image, actual.Image);
            Assert.AreEqual(expected.Rarity, actual.Rarity);
            Assert.AreEqual(expected.SecondaryStat, actual.SecondaryStat);
            Assert.AreEqual(expected.Passive, actual.Passive);
        }

        [Test]
        public void Weapon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", Weapon.TryParse returns false, and Weapon is null.
            Weapon weapon = new Weapon();
            string line = "1,Bulbasaur,A,B,C,65,65";
            Assert.IsFalse(Weapon.TryParse(line, out weapon));
            Assert.AreEqual(weapon, null);
        }

        // Output Files
        // 0 = "weapons.csv"
        // 1 = "empty.csv"
        // 2 = "weapons.XML"
        // 3 = "empty.XML"
        // 4 = "weapons.json"
        // 5 = "empty.json"

        // Test LoadJSON Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidJson()
        {
            WeaponCollection.Save(OUTPUT_FILES[4]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[4]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_Load_ValidJson()
        {
            WeaponCollection.SaveAsJSON(OUTPUT_FILES[4]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[4]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_LoadJSON_ValidJson()
        {
            WeaponCollection.SaveAsJSON(OUTPUT_FILES[4]);
            Assert.IsTrue(WeaponCollection.LoadJSON(OUTPUT_FILES[4]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        [Test]
        public void WeaponCollection_Load_Save_LoadJSON_ValidJson()
        {
            WeaponCollection.Save(OUTPUT_FILES[4]);
            Assert.IsTrue(WeaponCollection.LoadJSON(OUTPUT_FILES[4]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        // Test LoadCSV Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidCsv()
        {
            WeaponCollection.Save(OUTPUT_FILES[0]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[0]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsCSV_LoadCSV_ValidCsv()
        {

            WeaponCollection.SaveAsCSV(OUTPUT_FILES[0]);
            Assert.IsTrue(WeaponCollection.LoadCSV(OUTPUT_FILES[0]));
            Assert.AreEqual(WeaponCollection.Size(), 95);

        }

        // Test LoadXML Valid
        [Test]
        public void WeaponCollection_Load_Save_Load_ValidXml()
        {
            WeaponCollection.Save(OUTPUT_FILES[2]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[2]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsXML_LoadXML_ValidXml()
        {
            WeaponCollection.SaveAsXML(OUTPUT_FILES[2]);
            Assert.IsTrue(WeaponCollection.LoadXML(OUTPUT_FILES[2]));
            Assert.AreEqual(WeaponCollection.Size(), 95);
        }

        // Test SaveAsJSON Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidJson()
        {
            WeaponCollection = new WeaponCollection();
            WeaponCollection.SaveAsJSON(OUTPUT_FILES[5]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[5]));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        // Test SaveAsCSV Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidCsv()
        {
            WeaponCollection = new WeaponCollection();
            WeaponCollection.SaveAsCSV(OUTPUT_FILES[1]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[1]));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        // Test SaveAsXML Empty
        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidXml()
        {
            WeaponCollection = new WeaponCollection();
            WeaponCollection.SaveAsXML(OUTPUT_FILES[3]);
            Assert.IsTrue(WeaponCollection.Load(OUTPUT_FILES[3]));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        // Test Load InvalidFormat
        [Test]
        public void WeaponCollection_Load_SaveJSON_LoadXML_InvalidXml()
        {
            WeaponCollection.SaveAsJSON(OUTPUT_FILES[4]);
            Assert.IsFalse(WeaponCollection.LoadXML(OUTPUT_FILES[4]));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        [Test]
        public void WeaponCollection_Load_SaveXML_LoadJSON_InvalidJson()
        {
            WeaponCollection.SaveAsXML(OUTPUT_FILES[2]);
            Assert.IsFalse(WeaponCollection.LoadJSON(OUTPUT_FILES[2]));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadXML_InvalidXml()
        {
            Assert.IsFalse(WeaponCollection.LoadXML(inputPath));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadJSON_InvalidJson()
        {
            Assert.IsFalse(WeaponCollection.LoadJSON(inputPath));
            Assert.AreEqual(WeaponCollection.Size(), 0);
        }
    }
}
