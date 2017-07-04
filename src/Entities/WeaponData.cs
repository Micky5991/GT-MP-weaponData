using System.Collections.Generic;
using GTA;

namespace WeaponDataGenerator.Entities
{
    public class WeaponData : IHashable
    {
        public WeaponHash hash;
        public int model;
        public string name;
        public string displayName;
        public string displayDescription;

        public string localizedName;
        public string localizedDescription;

        public string type;

        public Dictionary<uint, WeaponComponentData> components = new Dictionary<uint, WeaponComponentData>();
        
        public void Hash()
        {
            this.hash = (WeaponHash) Game.GenerateHash(this.name);

            foreach (var component in this.components.Values) component.Hash();       
        }
    }
}