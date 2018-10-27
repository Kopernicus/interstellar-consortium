using Kopernicus;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace InterstellarConsortium
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class EventHook : MonoBehaviour
    {
        void Start()
        {
            Events.OnBodyApply.Add((body, config) => ICPatcher.OnBodyApply(body, config));
            Events.OnLoaderLoadBody.Add((body, config) => ICPatcher.OnLoaderLoadBody(body, config));
            Events.OnLoaderLoadedAllBodies.Add((loader, config) => ICPatcher.OnLoaderLoadedAllBodies(loader, config));
        }
    }
}