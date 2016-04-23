using System;

namespace TerraMission.Buildings
{
    public class CollectorMetadata : BuildingMetadata
    {
        public ResourceType Resource
        {
            get { return GetValue<ResourceType>(() => Resource); }
            set { SetValue(() => Resource, value); }
        }

        public int Capacity
        {
            get { return GetValue<int>(() => Capacity); }
            set { SetValue(() => Capacity, value); }
        }
    }
}
