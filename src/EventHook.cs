using System;
using System.Collections.Generic;
using Kopernicus;
using System.Diagnostics.CodeAnalysis;
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
            Events.OnBodyApply.Add((body, config) => ICPatcher.OnBodyApply(body, config));
            Events.OnLoaderLoadBody.Add((body, config) => ICPatcher.OnLoaderLoadBody(body, config));
            Events.OnLoaderLoadedAllBodies.Add((loader, config) => ICPatcher.OnLoaderLoadedAllBodies(loader, config));
            GameEvents.OnGameDatabaseLoaded.Add(OnGameDatabaseLoaded);
        }

        // Convert the settings defined by the user into MM :FOR[] nodes
        public IEnumerable<String> ModuleManagerAddToModList()
        {
            if (_settingsApplied)
            {
                return null;
            }

            _settingsApplied = true;
            UrlDir.UrlFile file = GameDatabase.Instance.GetConfigs("InterstellarConsortium")[0].parent;
            return ConvertToTags(InterstellarSettings.Instance.Settings, "IC");
        }

        // Convert the settings defined by the user into MM :FOR[] nodes
        void OnGameDatabaseLoaded()
        {
            if (_settingsApplied)
            {
                return;
            }

            _settingsApplied = true;
            UrlDir.UrlFile file = GameDatabase.Instance.GetConfigs("InterstellarConsortium")[0].parent;
            List<String> tags = ConvertToTags(InterstellarSettings.Instance.Settings, "IC");
            for (Int32 i = 0; i < tags.Count; i++)
            {
                file.configs.Add(new UrlDir.UrlConfig(file, new ConfigNode("@InterstellarConsortium:FOR[" + tags[i] + "]")));
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