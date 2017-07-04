using System.Collections.Generic;
using GTA;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class AttachmentPointProcessor
    {
        public static void Process(IEnumerable<WeaponData> weaponData)
        {
            foreach (var weapon in weaponData) Process(weapon.hash, weapon.components.Values);
        }

        public static void Process(WeaponHash weapon, IEnumerable<WeaponComponentData> components)
        {
            foreach (var component in components)
            {
                Process(weapon, component);
            }
        }

        public static void Process(WeaponHash weapon, WeaponComponentData component)
        {
            component.attachmentPoint = WeaponComponent.GetAttachmentPoint(weapon, component.hash).ToString();
        }
    }
}