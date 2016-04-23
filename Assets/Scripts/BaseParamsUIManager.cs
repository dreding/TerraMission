using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BaseParamsUIManager : MonoBehaviour {

    public Text missionDay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        missionDay.text = String.Format("Mission Year {0} Day {1}", TimeManager.Instance.passedDays / 365, TimeManager.Instance.passedDays);
	}
}
