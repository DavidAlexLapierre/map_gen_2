using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using GameContent.Models;
using GameContent.Utils;

namespace GameContent.UI {
    class WorldDataController {
        const string CONFIG_FILE_NAME = "config.xml";
        const string DATA_PATH = "./Data";
        const string WORLD_PATH = "/Worlds/";
        const int MAX_WORLDS = 3;
        static int WorldCount;

        public static bool CreateWorld(string worldName) {
            if (!CheckForDirectory(worldName)) return false;

            var data = new WorldData();
            data.CreationDate = DateTime.Now.ToString("g");
            data.Name = worldName;
            data.Seed = Generator.GetSeed();
            data.Version = Macros.VERSION;

            XmlSerializer write = new XmlSerializer(typeof(WorldData));

            var path = DATA_PATH + WORLD_PATH + worldName + "/" + CONFIG_FILE_NAME;
            var file = File.Create(path);
            write.Serialize(file, data);
            file.Close();

            return true;
        }

        public static void DeleteWorld(string name) {
            if (Directory.Exists(DATA_PATH + WORLD_PATH + name)) {
                var dir = new DirectoryInfo(DATA_PATH + WORLD_PATH + name);
                foreach (var file in dir.GetFiles()) { file.Delete(); }
                foreach (var subDir in dir.GetDirectories()) { subDir.Delete(); }
                dir.Delete();
            }
        }

        static bool CheckForDirectory(string worldName) {
            if (!Directory.Exists(DATA_PATH)) {
                Directory.CreateDirectory(DATA_PATH);
            }

            if (!Directory.Exists(DATA_PATH + WORLD_PATH)) {
                Directory.CreateDirectory(DATA_PATH + WORLD_PATH);
            }

            if (!Directory.Exists(DATA_PATH + WORLD_PATH + worldName + "/")) {
                Directory.CreateDirectory(DATA_PATH + WORLD_PATH + worldName + "/");
            }
            
            if (File.Exists(DATA_PATH + WORLD_PATH + worldName + "/" + CONFIG_FILE_NAME)) return false;

            return true;
        }

        public static bool CanCreateWorld() {
            return WorldCount < MAX_WORLDS;
        }

        public static List<WorldData> LoadWorlds() {
            List<WorldData> worlds = new List<WorldData>();
            if (Directory.Exists(DATA_PATH + WORLD_PATH)) {
                var dirInfo = new DirectoryInfo(DATA_PATH + WORLD_PATH);
                foreach (var dir in dirInfo.GetDirectories()) {
                    FileInfo[] files = dir.GetFiles(CONFIG_FILE_NAME);
                    XmlSerializer serializer = new XmlSerializer(typeof(WorldData));
                    foreach(var fileInfo in files) {
                        var name = dir.FullName + "/" + fileInfo.Name;
                        using (Stream reader = new FileStream(name, FileMode.Open)) {
                            var world = (WorldData)serializer.Deserialize(reader);
                            worlds.Add(world);
                        }
                    }
                }
            }

            WorldCount = worlds.Count;

            return worlds;
        }

        public static WorldData GetDataFromName(string worldName) {
            var fileName = DATA_PATH + WORLD_PATH + worldName + "/" + CONFIG_FILE_NAME;
            if (File.Exists(fileName)) {
                XmlSerializer serializer = new XmlSerializer(typeof(WorldData));
                using (Stream reader = new FileStream(fileName, FileMode.Open)) {
                    return (WorldData)serializer.Deserialize(reader);
                }
            }

            return null;
        }
    }
}