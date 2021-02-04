using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    public enum WeaponType
    {
        Sword,
        Polearm,
        Claymore,
        Catalyst,
        Bow,
        None
    }

    public class Weapon
    {
        // Name,Type,Rarity,BaseAttack
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public int Rarity { get; set; }
        public int BaseAttack { get; set; }
        public string Image { get; set; }
        public string SecondaryStat { get; set; }
        public string Passive { get; set; }


        /// <summary>
        /// The Comparator function to check for name
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByName(Weapon left, Weapon right)
        {
            return left.Name.CompareTo(right.Name);
        }

        // TODO: add sort for each property:
        // CompareByType
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByType(Weapon left, Weapon right)
        {
            return left.Type.CompareTo(right.Type);
        }

        // CompareByRarity
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByRarity(Weapon left, Weapon right)
        {
            return left.Rarity.CompareTo(right.Rarity);
        }

        // CompareByBaseAttack
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByBaseAttack(Weapon left, Weapon right)
        {
            return left.BaseAttack.CompareTo(right.BaseAttack);
        }

        // CompareByImage
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByImage(Weapon left, Weapon right)
        {
            return left.Image.CompareTo(right.Image);
        }

        // CompareBySecondaryStat
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareBySecondaryStat(Weapon left, Weapon right)
        {
            return left.SecondaryStat.CompareTo(right.SecondaryStat);
        }

        // CompareByPassive
        /// <summary>
        /// The Comparator function to check for type
        /// </summary>
        /// <param name="left">Left side Weapon</param>
        /// <param name="right">Right side Weapon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>
        public static int CompareByPassive(Weapon left, Weapon right)
        {
            return left.Passive.CompareTo(right.Passive);
        }

        /// <summary>
        /// The Weapon string with all the properties
        /// </summary>
        /// <returns>The Weapon formated string</returns>
        public override string ToString()
        {
            // TODO: construct a comma seperated value string
            // Name,Type,Image,Rarity,BaseAttack,SecondaryStat,Passive
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", Name, Type, Image, Rarity, BaseAttack, SecondaryStat, Passive);
        }

        public static bool TryParse(string rawData, out Weapon weapon)
        {
            // Name,Type,Image,Rarity,BaseAttack,SecondaryStat,Passive
            string[] values = rawData.Split(',');
            weapon = new Weapon();
            try
            {
                if (values.Length == 7)
                {
                    weapon.Name = values[0];
                    weapon.Type = (WeaponType)Enum.Parse(typeof(WeaponType), values[1]);
                    weapon.Image = values[2];
                    weapon.Rarity = int.Parse(values[3]);
                    weapon.BaseAttack = int.Parse(values[4]);
                    weapon.SecondaryStat = values[5];
                    weapon.Passive = values[6];
                    //Console.WriteLine(weapon);
                }
                else
                {
                    weapon = null;
                    return false;
                }
            }
            catch (Exception)
            {
                weapon = null;
                return false;
            }

            return true;
        }
    }
}
