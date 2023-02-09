using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using GTANetworkMethods;
using Roleplay.Utils;
using Player = GTANetworkAPI.Player;
using Vehicle = GTANetworkAPI.Vehicle;

namespace Roleplay.Commands
{
    class PlayerCommands : Script
    {
        // Genel Me ve Do komutları
        [Command("me", "~b~ Kullanım: /me [Hareket]", GreedyArg = true)]
        public void CMD_me(Player player, string action)
        {
            action = action.Trim();

            List<Player> nearbyPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(15, player);

            foreach (Player item in nearbyPlayers)
            {
                item.SendChatMessage($"{Colors.COLOR_MEDO} * {player.Name} {action}");
            }
        }
        [Command("do", "~b~ Kullanım: /do [Durum]", GreedyArg = true)]
        public void CMD_do(Player player, string action)
        {
            action = action.Trim();

            List<Player> nearbyPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(15, player);

            foreach (Player item in nearbyPlayers)
            {
                item.SendChatMessage($"{Colors.COLOR_MEDO} * "+action+" (( "+player.Name+" ))");
            }
        }
        // Pos Çekme Komutu
        [Command("pos")]
        public void CMD_pos(Player player)
        {
            player.SendChatMessage("~c~ Position: " + NAPI.Entity.GetEntityPosition(player));
        }
        [Command("createveh", "~b~ Kullanım: /createveh [Araç Modeli]", GreedyArg = true)]
        public void CMD_createveh(Player player, string model)
        {
            model = model.Trim();

            VehicleHash carModel = NAPI.Util.VehicleNameToModel(model); // Aracın hash çektik
            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(carModel, player.Position, 0, 0, 0); // Aracı oyuncunun olduğu yerde oluşturduk
            NAPI.Player.SetPlayerIntoVehicle(player, vehicle, 0); // Oyuncuyu araca oturttuk
        }
        [Command("fixcar")]
        public void CMD_fixcar(Player player)
        {   
            if (NAPI.Player.IsPlayerInAnyVehicle(player))
            {
                NAPI.Vehicle.RepairVehicle(NAPI.Player.GetPlayerVehicle(player));
                player.SendChatMessage($"{Colors.COLOR_GREEN} Bulunduğunuz araç tamir edildi.");
            }
            else
            {
                player.SendChatMessage("~r~ Araç içerisinde değilsiniz.");
            }
        }
        [Command("deletevehicle")]
        public void CMD_deletevehicle(Player player)
        {
            if (NAPI.Player.IsPlayerInAnyVehicle(player))
            {
                Vehicle vehicle = NAPI.Player.GetPlayerVehicle(player);
                vehicle.Delete();
                player.SendChatMessage($"{Colors.COLOR_GREEN} Araç başarıyla silindi.");
            }
            else
            {
                player.SendChatMessage("~r~ Araç içerisinde değilsiniz.");
            }
        }
        // Dimension Görüntüleme
        [Command("dimension")]
        public void CMD_dimension(Player player)
        {
            player.SendChatMessage("~c~ Dimension: " + player.Dimension);
        }
        // Kill ve Revive Komutu
        [Command("kill")]
        public void CMD_kill(Player player)
        {
            player.Kill();
        }
        [Command("revive", "~b~ Kullanım: /revive [Oyuncu ID]", GreedyArg = true)]
        public void CMD_revive(Player player, string targetValue)
        {
            Player target = Functions.GetPlayerByNameOrId(targetValue);
            if (target == null)
            {
                player.SendChatMessage("~r~ Belirtilen oyuncu bulunamadı.");
                return;
            }
            // Yetki Kontrol Kısmı
            if (Players.PlayerData.ReturnPlayerData(player).HelperLevel > 0 || Players.PlayerData.ReturnPlayerData(player).AdminLevel > 0)
            {
                target.Health = 100;
                player.SendChatMessage($"{Colors.COLOR_GREEN} {target.Name} adlı oyuncu canlandırıldı.");
            }
            else
            {
                player.SendChatMessage("~r~ Yeterli yetkiniz yok.");
            }
        }
        // Admin ve Helper Verme Komutu
        [Command("setadmin", "~b~Kullanım: /setadmin [Oyuncu ID] [Seviye]")]
        public void CMD_setadmin(Player player, string targetValue, int level)
        {
            Player target = Functions.GetPlayerByNameOrId(targetValue);
            if (target == null)
            {
                player.SendChatMessage("~r~ Belirtilen oyuncu bulunamadı.");
                return;
            }
            if (level < 0 || level > 5)
            {
                player.SendChatMessage("~r~ Seviye 0 ile 5 arasında olabilir.");
                return;
            }
            Players.PlayerData.ReturnPlayerData(target).AdminLevel = level;
        }
    }
}
