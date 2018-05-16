using Newtonsoft.Json;
using System;
using System.IO;

namespace Auctioneer.Util
{
    public static class Settings
    {
        static string fileDB = "DatabaseSettings.json", fileRun = "Settings.json";
        static DatabaseSettings dbSettings = new DatabaseSettings();
        static RunSettings runSettings = new RunSettings();

        public static string Connection { get; set; }

        public static void Init()
        {
            if (File.Exists(fileDB) && File.Exists(fileRun))
            {
                Load();
            }
            else
            {
                Create();
                Environment.Exit(0);
            }
        }

        public static void Load()
        {
            using (StreamReader reader = new StreamReader(fileDB))
            {
                string json = reader.ReadToEnd();
                dbSettings = JsonConvert.DeserializeObject<DatabaseSettings>(json);
            }

            Connection = "server=" + dbSettings.Ip + ";uid=" + dbSettings.User + ";pwd=" + dbSettings.Password + ";database=" + dbSettings.Database;
        }

        public static void Create()
        {
            string json = JsonConvert.SerializeObject(dbSettings, Formatting.Indented);
            File.WriteAllText(fileDB, json);

            json = JsonConvert.SerializeObject(runSettings, Formatting.Indented);
            File.WriteAllText(fileRun, json);
        }

        public static DatabaseSettings GetDBSettings()
        {
            return dbSettings;
        }

        public static RunSettings GetRunSettings()
        {
            return runSettings;
        }
    }
}
