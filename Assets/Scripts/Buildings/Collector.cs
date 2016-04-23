using UnityEngine;
using System.Collections;

namespace TerraMission.Buildings
{
    /// <summary>
    /// Хранилище ресурсов
    /// </summary>
    public class Collector : Building
    {
        public new CollectorMetadata Metadata
        {
            get { return (CollectorMetadata)base.Metadata; }
            set { base.Metadata = value; }
        }


        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            CollectResources(deltaTime);
        }

        protected virtual void CollectResources(float deltaTime)
        {
            var metadata = Metadata;
            
        }
    }
}
