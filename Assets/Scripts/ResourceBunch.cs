using UnityEngine;
using System.Collections;

namespace TerraMission
{
    public class ResourceBunch
    {
        public ResourceType Resource { get; set; }
        public float Count { get; set; }

        public ResourceBunch(ResourceType resource, float count = 0)
        {
            Resource = resource;
            Count = count;
        }
    }
}
