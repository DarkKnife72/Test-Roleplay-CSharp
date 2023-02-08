using System;
using System.Numerics;
using GTANetworkAPI;
using GTANetworkMethods;
using Roleplay.Utils;
using Vector3 = GTANetworkAPI.Vector3;
using static System.Collections.Specialized.BitVector32;
using Player = GTANetworkAPI.Player;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.X509;

namespace Roleplay
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("Oyun Modu Başlatıldı.");
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnect(Player player) {
            player.SendChatMessage($"~y~ Sunucuya hoş geldin. {player.Name}");
            NAPI.Server.SetDefaultSpawnLocation(new Vector3(107.735275, -1940.8029, 20.8072));

            if (player.HasData(Players.PlayerData.DataIdentifier))
                player.ResetData(Players.PlayerData.DataIdentifier);
            player.SetData<Players.PlayerData>(Players.PlayerData.DataIdentifier, new Players.PlayerData());
        }
        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(GTANetworkAPI.Player player, string message)
        {
            List<GTANetworkAPI.Player> nearbyPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(15, player);

            foreach (GTANetworkAPI.Player item in nearbyPlayers)
            {
                if (item.Dimension != player.Dimension)
                    continue;
                else
                    item.SendChatMessage($"{player.Name}: {message}");
            }
        }
        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Player player, Player killer, uint reason)
        {
            player.SendChatMessage("Yaralı konumdasınız.");
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", true);
        }
    }
}