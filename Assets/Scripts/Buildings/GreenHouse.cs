﻿using UnityEngine;
using System.Collections;

namespace TerraMission.Buildings
{
    public class GreenHouse : Extractor
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Extract()
        {
            ExtractResources(TimeManager.Instance.passedMinutes - lastExtractionTime);
        }
       
    }
}
