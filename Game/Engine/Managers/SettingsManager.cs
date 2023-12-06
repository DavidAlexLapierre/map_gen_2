using System;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Engine.Managers {
    class SettingsManager {
        const string SETTINGS_PATH = "./data/settings.xml";
        public static Point Resolution { get; private set; }
        public static int ResolutionIndex { get; private set; }

        static SettingsManager() {
            LoadSettings();
        }

        public static void SetSetting(string setting, string attribute, string value) {
            XmlDocument file = new XmlDocument();
            file.Load(SETTINGS_PATH);
            XmlNodeList settings = file.SelectNodes("settings");
            foreach (XmlNode settingNode in settings) {
                XmlNodeList nodes = settingNode.SelectNodes(setting);
                foreach (XmlNode node in nodes) {
                    node.Attributes[attribute].Value = value;
                }
            }
            file.Save(SETTINGS_PATH);
        }

        private static void LoadSettings() {
            if (File.Exists(SETTINGS_PATH)) {
                XmlDocument file = new XmlDocument();
                file.Load(SETTINGS_PATH);
                XmlNodeList settings = file.SelectNodes("settings");
                foreach (XmlNode setting in settings) {
                    XmlNodeList resolution = setting.SelectNodes("resolution");
                    foreach (XmlNode node in resolution) {
                        var width = Int32.Parse(node.Attributes["width"].Value);
                        var height = Int32.Parse(node.Attributes["height"].Value);
                        ResolutionIndex = Int32.Parse(node.Attributes["index"].Value);
                        Resolution = new Point(width, height);
                    }
                }
            } else {
                InitSettingsFile();
            }
        }

        private static void InitSettingsFile() {
            XmlWriter writer = XmlWriter.Create(SETTINGS_PATH);
            writer.WriteStartDocument();
            writer.WriteStartElement("settings");

            // Resolution
            writer.WriteStartElement("resolution");
            writer.WriteAttributeString("width", "640");
            writer.WriteAttributeString("height", "360");
            writer.WriteAttributeString("index", "0");
            writer.WriteEndElement();
            
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            // Set default settings
            Resolution = new Point(640, 360);
        }
    }
}