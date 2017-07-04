using GTA;

namespace WeaponDataGenerator.Entities
{
    public class WeaponComponentData : IHashable
    {
        public WeaponComponentHash hash;
        public string name;
        public string attachmentPoint;
        public string displayName;
        public string displayDescription;

        public string localizedName;
        public string localizedDescription;
        
        public void Hash()
        {
            this.hash = (WeaponComponentHash) Game.GenerateHash(this.name);
        }
    }
}