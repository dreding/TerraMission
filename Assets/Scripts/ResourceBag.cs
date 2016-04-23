using System.Collections.Generic;

namespace TerraMission
{
    public class ResourceBag : Dictionary<ResourceType, float>
    {
        public new float this[ResourceType resource]
        {
            get
            {
                float count = 0;
                if (TryGetValue(resource, out count))
                    return count;

                return 0;
            }

            set
            {
                base[resource] = value;
            }
        }

    }
}
