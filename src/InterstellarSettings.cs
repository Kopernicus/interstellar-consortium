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
            ConfigNode node = GameDatabase.Instance.GetConfigs("IC")[0].config;
            Instance = Parser.CreateObjectFromConfigNode<InterstellarSettings>(node);
        }

        [ParserTarget("home")]
        public String HomeStar;

        [ParserTarget("homePlanet")]
        public String HomePlanet;

        [ParserTarget("KI")]
        public NumericParser<Double> KI = 1E13;
    }
}