using System.Collections.Generic;
using System.IO;
using GTA;
using Newtonsoft.Json;
using WeaponDataGenerator.Entities;

namespace WeaponDataGenerator.Processors
{
    public class OutputProcessor
    {

        public static string MakeOutputPayload(Dictionary<int, WeaponData> weaponData)
        {
            return JsonConvert.SerializeObject(weaponData);
        }

        public static void OutputDataFile(string path, string payload)
        {
            try
            {
                string fileName = Path.Combine(Main.basepath, path);
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                File.WriteAllText(fileName, payload);
            }
            catch (IOException e)
            {
                Console.Error("AN ERROR OCCURED: " + e.Message);
            }
        }
        
    }
}