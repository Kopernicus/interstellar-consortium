using System;
using System.Diagnostics.CodeAnalysis;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.Configuration.Parsing;

namespace InterstellarConsortium
{
    [RequireConfigType(ConfigType.Node)]
    [ParserTargetExternal("Body", "InterstellarConsortium", "Kopernicus")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class InterstellarStar : BaseLoader
    {
        [ParserTarget("position", Optional = false)]
        public Vector3DParser Position
        {
            get { return generatedBody.Get("IC:Position", Vector3d.zero); }
            set { generatedBody.Set("IC:Position", value.Value); }
        }

        [ParserTarget("home")]
        public String HomePlanet
        {
            get { return generatedBody.Get<String>("IC:HomePlanet", null); }
            set { generatedBody.Set("IC:HomePlanet", value); }
        }

        [ParserTarget("SOI")]
        public NumericParser<Double> Soi
        {
            get { return generatedBody.Get("IC:SOI", 0d); }
            set { generatedBody.Set("IC:SOI", value.Value); }
        }

        [ParserTarget("Orbit")]
        public ConfigNode OrbitPatches
        {
            get { return generatedBody.Get<ConfigNode>("IC:OrbitPatches", null); }
            set { generatedBody.Set("IC:OrbitPatches", value); }
        }
    }
}