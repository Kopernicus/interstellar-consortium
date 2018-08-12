using System;
using System.Collections.Generic;
using System.Linq;
using Kopernicus;
using Kopernicus.Configuration;

namespace InterstellarConsortium
{
    [ParserTargetExternal("Body", "Orbit", "Kopernicus")]
    public class InterstellarReferenceLoader : BaseLoader
    {
        private static readonly Dictionary<String, String> References = new Dictionary<String, String>();
        
        private static readonly List<Body> Bodies = new List<Body>();

        /// <summary>
        /// The star the body is orbiting. The difference between this and referenceBody is,
        /// that with this, the body automatically reparents to it's parent star if it is moved
        /// to the center of the universe and has to be named "Sun"
        /// </summary>
        [ParserTarget("interstellarReference")]
        public String InterstellarReference
        {
            get
            {
                if (References.ContainsKey(generatedBody.transform.name))
                {
                    return References[generatedBody.transform.name];
                }

                return null;
            }
            set
            {
                if (References.ContainsKey(generatedBody.transform.name))
                {
                    References[generatedBody.transform.name] = value;
                }

                References.Add(generatedBody.transform.name, value);
            }
        }

        /// <summary>
        /// Gets invoked after a body has been loaded successfully. We use it to collect references to all bodies
        /// because Kopernicus doesn't have an API that allows us to access all bodies between loading and finalizing
        /// </summary>
        public static void OnLoaderLoadBody(Body body, ConfigNode node)
        {
            Bodies.Add(body);
        }

        /// <summary>
        /// Gets invoked when all bodies were successfully loaded
        /// </summary>
        public static void OnLoaderLoadedAllBodies(Loader loader, ConfigNode node)
        {
            // Transform the reference bodies
            foreach (KeyValuePair<String, String> reference in References)
            {
                // Try to find the referenced bodies
                Body body = Bodies.FirstOrDefault(b => b.name == reference.Key);
                Body target = Bodies.FirstOrDefault(b => b.identifier == reference.Value);

                if (body == null || target == null)
                {
                    continue;
                }

                // The body needs an orbit to be re-referenceable
                if (body.orbit == null)
                {
                    continue;
                }
                
                // The new parent body has to be a star
                if (target.scaledVersion.type != BodyType.Star)
                {
                    continue;
                }
                
                // Move the body
                body.orbit.referenceBody = target.name;
            }
        }
    }
}