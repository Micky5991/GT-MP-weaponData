namespace WeaponDataGenerator.Processors
{
    public class FixNameProcessor
    {

        public static string FixWeaponName(string name)
        {
            name = name.Replace("HEAVY_", "HEAVY");
            
            name = name.Replace("ASSAULT_", "ASSAULT");
            
            name = name.Replace("_RIFLE", "RIFLE");
            
            name = name.Replace("COMBAT_", "COMBAT");
            
            name = name.Replace("_PISTOL", "PISTOL");
            name = name.Replace("COMPONENTPISTOL", "COMPONENT_PISTOL");
            
            name = name.Replace("_SHOTGUN", "SHOTGUN");
            
            name = name.Replace("SPECIAL_CARBINE", "SPECIALCARBINE");
            name = name.Replace("MICRO_S_M_G", "MICROSMG");
            name = name.Replace("S_M_G", "SMG");
            name = name.Replace("M_G", "MG");
            name = name.Replace("S_N_S", "SNS");
            name = name.Replace("A_P", "AP");
            name = name.Replace("P_D_W", "PDW");
            
            name = name.Replace("SUPP02", "SUPP_02");
            name = name.Replace("SMALL02", "SMALL_02");
            name = name.Replace("MACRO02", "MACRO_02");
            name = name.Replace("COVER01", "COVER_01");
            name = name.Replace("RAIL_COVER", "RAILCOVER");
            
            name = name.Replace("AF_GRIP", "AFGRIP");
            
            
            name = name.Replace("CLIP0", "CLIP_0");

            return name;
        }
        
    }
}