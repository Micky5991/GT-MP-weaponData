using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using WeaponDataGenerator.Entities;
using WeaponDataGenerator.Processors;

namespace WeaponDataGenerator
{
    public class Main : Script
    {

        public static string basepath = @"scripts/weapondata/";
        
        public static readonly WeaponStorage WeaponStorage = new WeaponStorage();
        
        public Main()
        {
            this.Tick += onTick;
            this.KeyUp += onKeyUp;
            this.KeyDown += onKeyDown;
        }

        private void onTick(object sender, EventArgs e)
        {
            Game.Player.WantedLevel = 0;
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)
            {
                GTA.UI.Screen.ShowNotification("Starting...");
                
                Game.Player.Character.Weapons.RemoveAll();
                
                new WepShopProcessor("shopdata/");
                new WeaponMetaProcessor(@"gameinfo/weapons.meta", @"gameinfo/weaponcomponents.meta");
                
                MissingAttachmentsProcessor.Process();
                WeaponInfoProcessor.Process();

                OutputProcessor.OutputDataFile(@"output/weaponData.json", OutputProcessor.MakeOutputPayload(WeaponStorage.weaponData));
                GTA.UI.Screen.ShowNotification("~g~weaponData.json created!");
            }
        }
    }
}