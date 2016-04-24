using System;
using UnityEngine;

namespace TerraMission.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        public BuildingMetadata Metadata { get; set; }

        private float _passedTime = 0;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            _passedTime = TimeManager.Instance.passedMinutes;
        }

        protected virtual void Update()
        {
            var passedTime = TimeManager.Instance.passedMinutes;

            var deltaTime = passedTime - _passedTime;
            OnUpdate(deltaTime);

            _passedTime = passedTime;
        }

        protected virtual void OnUpdate(float deltaTime)
        {
            var metadata = Metadata;

            var requiredResources = metadata.ResourcesUsage;
            if (requiredResources != null)
            {
                foreach (var requiredResource in requiredResources)
                {
                    var resource = requiredResource.Key;
                    var count = requiredResource.Value * deltaTime;

                    if (!RequireResources(resource, count))
                    {
                        // TODO: Do something
                    }
                }
            }
        }

        protected bool RequireResources(ResourceType resource, float count)
        {
            throw new NotImplementedException();
        }
    }
}
