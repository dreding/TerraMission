using System;

namespace TerraMission.Buildings
{
    public class ExtractorMetadata : BuildingMetadata
    {
        public ResourceType Resource
        {
            get { return GetValue<ResourceType>(() => Resource); }
            set { SetValue(() => Resource, value); }
        }

        public int Speed
        {
            get { return GetValue<int>(() => Speed); }
            set { SetValue(() => Speed, value); }
        }
    }
}
