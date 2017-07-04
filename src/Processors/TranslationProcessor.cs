using System.Collections.Generic;
using GTA;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class TranslationProcessor
    {
        public static void ProcessWeaponLanguage(IEnumerable<WeaponData> weapons)
        {
            foreach (var weaponData in weapons) ProcessWeaponLanguage(weaponData);
        }

        public static void ProcessWeaponLanguage(WeaponData weapon)
        {
            weapon.localizedName         = Game.GetLocalizedString(weapon.displayName);
            weapon.localizedDescription  = Game.GetLocalizedString(weapon.displayDescription);
            
            ProcessComponentLanguage(weapon.components.Values);
        }

        public static void ProcessComponentLanguage(IEnumerable<WeaponComponentData> components)
        {
            foreach (var component in components) ProcessComponentLanguage(component);
        }

        public static void ProcessComponentLanguage(WeaponComponentData component)
        {
            component.localizedName         = Game.GetLocalizedString(component.displayName);
            component.localizedDescription  = Game.GetLocalizedString(component.displayDescription);
        }
    }
}