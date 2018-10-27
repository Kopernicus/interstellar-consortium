using System;
using Kopernicus;
using Kopernicus.Configuration;

namespace InterstellarConsortium
{
    [RequireConfigType(ConfigType.Node)]
    [ParserTargetExternal("Body", "InterstellarConsortium", "Kopernicus")]
    public class InterstellarStar : BaseLoader
    {
        [ParserTarget("position", Optional = false)]
        public Vector3DParser position
        {
            get { return generatedBody.Get("IC:Position", Vector3d.zero); }
            set { generatedBody.Set("IC:Position", value.Value); }
        }

        [ParserTarget("home")]
        public String homePlanet
        {
            get { return generatedBody.Get<String>("IC:HomePlanet", null); }
            set { generatedBody.Set("IC:HomePlanet", value); }
        }

        [ParserTarget("SOI")]
        public NumericParser<Double> SOI
        {
            get { return generatedBody.Get("IC:SOI", 0d); }
            set { generatedBody.Set("IC:SOI", value.Value); }
        }

        [ParserTarget("Orbit")]
        public ConfigNode orbitPatches
        {
            get { return generatedBody.Get<ConfigNode>("IC:OrbitPatches", null); }
            set { generatedBody.Set("IC:OrbitPatches", value); }
        }
    }
}