using System;
using UnityEngine;

namespace TerraMission.Buildings
{
    /// <summary>
    /// Здание добычи ресурсов
    /// </summary>
    public class Extractor : Building
    {
        public ResourceType resourceType;
        public int count;
        public int extractionTime;

        public new ExtractorMetadata Metadata
        {
            get { return (ExtractorMetadata)base.Metadata; }
            set { base.Metadata = value; }
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }

        protected virtual void ExtractResources(float deltaTime)
        {
            var metadata = Metadata;

            var extractCount = count * (deltaTime / extractionTime);

            OnResourcesExtracted(new ResourceBunch(resourceType, extractCount));
        }

        protected virtual void OnResourcesExtracted(ResourceBunch resourceBunch)
        {
            Debug.Log(string.Format("[OnResourcesExtracted] {0}: {1}",
                resourceBunch.Resource, resourceBunch.Count));
        }
    }
}
