using System;
using UnityEngine;

namespace TerraMission.Buildings
{
    /// <summary>
    /// Здание добычи ресурсов
    /// </summary>
    public class Extractor : Building
    {
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

            var resourceType = metadata.Resource;
            var count = metadata.Count * deltaTime;

            OnResourcesExtracted(new ResourceBunch(resourceType, count));
        }

        protected virtual void OnResourcesExtracted(ResourceBunch resourceBunch)
        {
            Debug.Log(string.Format("[OnResourcesExtracted] {0}: {1}",
                resourceBunch.Resource, resourceBunch.Count));
        }
    }
}
