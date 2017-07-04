using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using GTA;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class WeaponMetaProcessor
    {

        private readonly string _weaponMetaPath;
        private readonly string _weaponComponentMetaPath;
        
        private readonly List<XElement> _createdWeapons = new List<XElement>();

        public WeaponMetaProcessor(string weaponMetaPath, string weaponComponentMetaPath)
        {
            this._weaponMetaPath             = Path.Combine(Main.basepath, weaponMetaPath);
            this._weaponComponentMetaPath    = Path.Combine(Main.basepath, weaponComponentMetaPath);
            
            this.LoadWeaponMeta();
            this.LoadWeaponComponentMeta();
            this.AddComponentsToWeapons();
        }

        private void LoadWeaponMeta()
        {
            XDocument document = XDocument.Load(this._weaponMetaPath);
            List<XElement> elements = new List<XElement>();
            foreach (var element in document.Descendants("Infos").Elements("Item"))
            {
                if (element.Attribute("type") != null && element.Attribute("type").Value == "CWeaponInfo")
                {
                    var name = element.Element("Name").Value;
                    if(Enum.IsDefined(typeof(WeaponHash), (uint) Game.GenerateHash(name))) elements.Add(element);
                }
            }
            
            this.MakeWeapons(elements);
        }

        private void LoadWeaponComponentMeta()
        {
            XDocument document = XDocument.Load(this._weaponComponentMetaPath);
            List<XElement> elements = new List<XElement>();
            foreach (var element in document.Descendants("Infos").Elements("Item"))
            {
                if (element.Attribute("type") != null)
                {
                    if(element.Element("Model") == null) continue;
                    elements.Add(element);
                }
            }
            
            this.MakeComponents(elements);
        }

        private void MakeWeapons(IEnumerable<XElement> elements) 
        {
            foreach (var element in elements)
            {
                var name                 = element.Element("Name").Value;
                var displayName          = element.Element("HumanNameHash").Value;
                var displayDescription   = displayName.Insert(2, "D");

                Main.WeaponStorage.AddWeapon(new WeaponData
                {
                    name = name,
                    displayName = displayName,
                    displayDescription = displayDescription
                });
                
                this._createdWeapons.Add(element);
            }
        }

        private void MakeComponents(IEnumerable<XElement> elements)
        {
            foreach (var element in elements)
            {
                var name                 = element.Element("Name").Value;
                var displayName          = element.Element("LocName").Value;
                var displayDescription   = element.Element("LocDesc").Value;

                Main.WeaponStorage.AddWeaponComponent(new WeaponComponentData
                {
                    name = name,
                    displayName = displayName,
                    displayDescription = displayDescription
                });
            }
        }

        private void AddComponentsToWeapons()
        {
            foreach (var weapon in this._createdWeapons)
            {
                WeaponData weaponData =
                    Main.WeaponStorage.weaponData[Game.GenerateHash(weapon.Element("Name").Value)];

                foreach (var item in weapon.Element("AttachPoints").Elements("Item"))
                {
                    foreach (var component in item.Element("Components").Elements("Item"))
                    {
                        Main.WeaponStorage.AddWeaponComponent(weaponData, (WeaponComponentHash) Game.GenerateHash(component.Element("Name").Value));
                    }
                }
                
            }
        }
        
        
    }
}