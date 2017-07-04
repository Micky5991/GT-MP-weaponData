using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class WepShopProcessor
    {
        private readonly string _path;

        private readonly List<XElement> _elements = new List<XElement>();
        
        public WepShopProcessor(string path)
        {
            this._path = Path.Combine(Main.basepath, path);
            this.Load();
            this.MakeWeaponData();
        }

        private void Load()
        {
            string[] files = Directory.GetFiles(this._path, "*.meta");
            foreach (string file in files)
            {
                XDocument doc = XDocument.Load(file);
                foreach (var element in doc.Descendants("weaponShopItems").Elements())
                {
                    this._elements.Add(element);
                }
            }
        }

        private void MakeWeaponData()
        {
            foreach (var element in this._elements)
            {
                var name                = element.Element("nameHash")?.Value;
                var displayName         = element.Element("textLabel")?.Value ?? "";
                var displayDescription  = element.Element("weaponDesc")?.Value ?? "";
                
                var weaponData = Main.WeaponStorage.AddWeapon(new WeaponData
                {
                    name = name,
                    displayName = displayName,
                    displayDescription = displayDescription
                });

                Main.WeaponStorage.AddWeaponComponent(weaponData, ProcessComponents(element.Element("weaponComponents")));
            }
        }

        private static IEnumerable<WeaponComponentData> ProcessComponents(XElement elements)
        {
            if(!elements.Elements().Any()) return new List<WeaponComponentData>();
            var components = new List<WeaponComponentData>();

            foreach (XElement element in elements.Elements())
            {
                var name                = element.Element("componentName")?.Value;
                var displayName         = element.Element("textLabel")?.Value ?? "";
                var displayDescription  = element.Element("componentDesc")?.Value ?? "";
                
                components.Add(new WeaponComponentData
                {
                    name = name,
                    displayName = displayName,
                    displayDescription = displayDescription
                });
            }
            
            return components;
        }
    }
}