using System.Collections.Generic;
using GTA;
using WeaponDataGenerator.Entities;
using WeaponDataGenerator.Processors;

namespace WeaponDataGenerator
{
    public class WeaponStorage
    {
        
        public readonly Dictionary<int, WeaponData> weaponData = new Dictionary<int, WeaponData>();
        public readonly Dictionary<WeaponComponentHash, WeaponComponentData> componentData = new Dictionary<WeaponComponentHash, WeaponComponentData>();

        public WeaponData AddWeapon(WeaponData data)
        {
            data.Hash();
            if (this.weaponData.ContainsKey((int) data.hash)) return this.weaponData[(int) data.hash];
            
            TranslationProcessor.ProcessWeaponLanguage(data);
            
            this.weaponData.Add((int) data.hash, data);
            return data;
        }

        public void AddWeaponComponent(WeaponHash weapon, WeaponComponentData data)
        {
            AddWeaponComponent(this.weaponData[(int) weapon], data);
        }

        public void AddWeaponComponent(WeaponHash weapon, IEnumerable<WeaponComponentData> data)
        {
            AddWeaponComponent(this.weaponData[(int) weapon], data);
        }

        public void AddWeaponComponent(WeaponData weapon, IEnumerable<WeaponComponentData> data)
        {
            foreach (var component in data) AddWeaponComponent(weapon, component);
        }

        public void AddWeaponComponent(WeaponData weapon, WeaponComponentData data)
        {
            data.Hash();
            if (weapon.components.ContainsKey((uint)data.hash)) return;
            data = AddWeaponComponent(data);
            
            TranslationProcessor.ProcessComponentLanguage(data);
            AttachmentPointProcessor.Process(weapon.hash, data);
           
            weapon.components.Add((uint)data.hash, data);
        }

        public void AddWeaponComponent(WeaponData weapon, WeaponComponentHash hash)
        {
            if(!this.componentData.ContainsKey(hash)) return;
            AddWeaponComponent(weapon, this.componentData[hash]);
        }

        public WeaponComponentData AddWeaponComponent(WeaponComponentData data)
        {
            data.Hash();
            if (this.componentData.ContainsKey(data.hash)) return this.componentData[data.hash];
            
            this.componentData.Add(data.hash, data);
            return data;
        }
        
        
    }
}