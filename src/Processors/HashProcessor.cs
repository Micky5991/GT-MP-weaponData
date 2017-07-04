using System.Collections.Generic;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class HashProcessor
    {
        public static void HashData(IEnumerable<IHashable> hashables)
        {
            foreach (var hashable in hashables) hashable.Hash();
        }
        
    }
}