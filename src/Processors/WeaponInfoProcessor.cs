using GTA;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class WeaponInfoProcessor
    {

        public static void Process()
        {
            foreach (var weapons in Main.WeaponStorage.weaponData.Values) Process(weapons);
        }

        private static void Process(WeaponData weapon)
        {
            Weapon wep = Game.Player.Character.Weapons.Give(weapon.hash, 1000, true, true);
            weapon.type = wep.Group.ToString();
            weapon.model = wep.Model.Hash;
        }
        
    }
}