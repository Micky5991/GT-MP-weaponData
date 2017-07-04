using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using WeaponDataGenerator.Entities;
using Console = GTA.Console;

namespace WeaponDataGenerator.Processors
{
    public class MissingAttachmentsProcessor
    {

        public static void Process()
        {
            Process(Main.WeaponStorage.weaponData.Values);
        }

        public static void Process(IEnumerable<WeaponData> weapons)
        {
            foreach (var weaponData in weapons) Process(weaponData);
        }

        public static void Process(WeaponData weapon)
        {
            var components = WeaponComponentCollection.GetComponentsFromHash(weapon.hash);

            foreach (var component in components)
            {
                if(weapon.components.ContainsKey((uint) component)) continue;

                var displayName = WeaponComponent.GetComponentDisplayNameFromHash(weapon.hash, component);
                var name = "COMPONENT_" +
                           Enum.GetName(typeof(WeaponComponentHash), component)?.ToUnderscoreCase().ToUpper();
                name = FixNameProcessor.FixWeaponName(name);
                
                var comp = new WeaponComponentData
                {
                    name                 = name,
                    displayName          = displayName,
                    displayDescription   = displayName.ReplaceAt(2, 'D')
                };
                comp.Hash();
                
                if(comp.hash != component) Console.Error("WRONG HASH! -> " + name);

                Main.WeaponStorage.AddWeaponComponent(weapon, comp);
            }
        }
        
        
    }
    
    public static class ExtensionMethods {
        public static string ToUnderscoreCase(this string str) {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}