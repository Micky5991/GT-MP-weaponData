using System.Collections.Generic;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class HashProcessor
    {
        public static void HashWeapons(IEnumerable<WeaponData> weaponDatas)
        {
            foreach (var weaponData in weaponDatas) weaponData.Hash();
        }
        
    }
}