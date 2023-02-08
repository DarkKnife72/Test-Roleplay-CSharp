using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace Roleplay.Utils
{
    class Functions
    {
        public static Player GetPlayerByNameOrId(string value)
        {
            // If value is number, we look in pool by ID
            if (int.TryParse(value, out int targetId))
            {
                return NAPI.Pools.GetAllPlayers().FirstOrDefault(X => X.Id == targetId);
            }
            // If value is not a number, we look by players name
            else
            {
                return NAPI.Pools.GetAllPlayers().FirstOrDefault(X => X.Name.Contains(value));
            }
        }
    }
}
