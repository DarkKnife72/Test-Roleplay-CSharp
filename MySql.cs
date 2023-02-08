using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace Roleplay
{
    class MySql
    {
        public static bool IsConnectionSetup = false;
        public static MySqlConnection conn;

        public String Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }

        public MySql() { 
            this.Host= "localhost";
            this.Username= "root";
            this.Password= "";
            this.Database = "ragemp_db";
        }

        public static void InitConnection()
        {
            String FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQLInfo.json");
            MySql sql = new MySql();

            if (File.Exists(FilePath)) // Dosya bulunduysa
            {
                String SQLData = File.ReadAllText(FilePath);
                sql = NAPI.Util.FromJson<MySql>(SQLData);
                String SQLConnection = $"SERVER={sql.Host};PASSWORD={sql.Password};UID={sql.Username};DATABASE={sql.Database}";
                conn = new MySqlConnection(SQLConnection);

                try
                {
                    conn.Open();
                    IsConnectionSetup = true;
                }
                catch
                {

                }
            }
            else
            {
                String SQLData = NAPI.Util.ToJson(sql);
                using (StreamWriter writer  = new StreamWriter(FilePath))
                {
                    writer.WriteLine(SQLData);
                }
                InitConnection();
            }
        }
    }
}
