using System;
using Kopernicus;

namespace InterstellarConsortium
{
    public class InterstellarSettings
    {
        public static InterstellarSettings Instance { get; set; }

        // Load the settings from GameData
        static InterstellarSettings()
        {
            // Maybe change this to manual loading to prevent tampering with the config?
            ConfigNode node = GameDatabase.Instance.GetConfigs("InterstellarConsortium")[0].config;
            Instance = Parser.CreateObjectFromConfigNode<InterstellarSettings>(node);
        }

        [ParserTarget("home")]
        public String HomeStar;

        [ParserTarget("homePlanet")]
        public String HomePlanet;

        [ParserTarget("KI")]
        public NumericParser<Double> KI = 1E13;

        [ParserTarget("Settings")] 
        public ConfigNode Settings;
    }
}