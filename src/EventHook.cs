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
            Events.OnLoaderLoadBody.Add((body, config) => InterstellarReferenceLoader.OnLoaderLoadBody(body, config));
            Events.OnLoaderLoadedAllBodies.Add((loader, config) => InterstellarReferenceLoader.OnLoaderLoadedAllBodies(loader, config));
        }
    }
}