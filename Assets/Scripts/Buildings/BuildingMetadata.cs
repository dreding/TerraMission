using System;

namespace TerraMission.Buildings
{
    public class BuildingMetadata : Metadata
    {
        public string Name
        {
            get { return GetValue<string>(() => Name); }
            set { SetValue(() => Name, value); }
        }

        public string Description
        {
            get { return GetValue<string>(() => Description); }
            set { SetValue(() => Description, value); }
        }
        
        // Используемые ресурсы
        public ResourceBag ResourcesUsage
        {
            get { return GetValue<ResourceBag>(() => ResourcesUsage); }
            set { SetValue(() => ResourcesUsage, value); }
        }
    }
}
