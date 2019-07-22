using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Kopernicus;
using UnityEngine;

namespace InterstellarConsortium
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class EventHook : MonoBehaviour
    {
        private Boolean _settingsApplied;
        
        void Start()
        {
            Events.OnBodyApply.Add((body, config) => SystemPatcher.OnBodyApply(body));
            Events.OnLoaderLoadBody.Add((body, config) => SystemPatcher.OnLoaderLoadBody(body));
            Events.OnLoaderLoadedAllBodies.Add((loader, config) => SystemPatcher.OnLoaderLoadedAllBodies());
            GameEvents.OnGameDatabaseLoaded.Add(OnGameDatabaseLoaded);
        }

        // Convert the settings defined by the user into MM :FOR[] nodes
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public IEnumerable<String> ModuleManagerAddToModList()
        {
            if (_settingsApplied)
            {
                return null;
            }

            _settingsApplied = true;
            return ConvertToTags(InterstellarSettings.Instance.Settings, "IC");
        }

        // Convert the settings defined by the user into MM :FOR[] nodes
        private void OnGameDatabaseLoaded()
        {
            if (_settingsApplied)
            {
                return;
            }

            _settingsApplied = true;
            UrlDir.UrlFile file = GameDatabase.Instance.GetConfigs("InterstellarConsortium")[0].parent;
            List<String> tags = ConvertToTags(InterstellarSettings.Instance.Settings, "IC");
            foreach (String mmTag in tags)
            {
                file.configs.Add(new UrlDir.UrlConfig(file,
                    new ConfigNode("@InterstellarConsortium:FOR[" + mmTag + "]")));
            }
        }

        private List<String> ConvertToTags(ConfigNode node, String prefix)
        {
            List<String> tags = new List<String>();
            for (Int32 i = 0; i < node.CountValues; i++)
            {
                tags.Add(prefix + "-" + node.values[i].name);
                tags.Add(prefix + "-" + node.values[i].name + "-" + node.values[i].value);
            }

            for (Int32 i = 0; i < node.CountNodes; i++)
            {
                tags.AddRange(ConvertToTags(node.nodes[i], prefix + "-" + node.nodes[i].name));
            }

            return tags;
        }
    }
}