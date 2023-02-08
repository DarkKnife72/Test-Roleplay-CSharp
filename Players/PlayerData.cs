using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace Roleplay.Players
{
    class PlayerData
    {
        public const string DataIdentifier = "PlayerData_Identifier";

        public string Isim;
        public string Soyisim;
        public int Level;
        public int Cash;
        public int Health;
        public int Armor;
        public string DogumTarih;
        public string Skin;
        public int AdminLevel;
        public int HelperLevel;
        public int SonX, SonY, SonZ;

        public PlayerData()
        {
            this.AdminLevel = 0;
            this.HelperLevel = 0;
            this.Cash = 1000;
        }

        public static PlayerData ReturnPlayerData(Player player)
        {
            if (!player.HasData(DataIdentifier))
                player.SetData(DataIdentifier, new PlayerData());
            return player.GetData<PlayerData>(DataIdentifier);
        }

        public void SetAdminLevel(int level)
        {
            this.AdminLevel = level;
        }
        public void SetHelperLevel(int level)
        {
            this.HelperLevel = level;
        }
        public void SetHealth(int health)
        {
            this.Health = health;
        }
        public void SetArmor(int armor)
        {
            this.Armor = armor;
        }
    }
}
