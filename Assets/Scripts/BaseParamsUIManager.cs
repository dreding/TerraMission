using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BaseParamsUIManager : MonoBehaviour {

    public static BaseParamsUIManager Instance;

    public Text missionDay;

    public GameObject evetHandleWindow;
    public Text eventText;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        missionDay.text = String.Format("Mission Year {0} Day {1}", TimeManager.Instance.passedYears, (int)(TimeManager.Instance.passedDays - (TimeManager.Instance.passedYears * 686.98f)));
	}

    public void ShowEventWindow(string text)
    {
        evetHandleWindow.SetActive(true);
        eventText.text = text;
    }

    public void HideEventWindow()
    {
        evetHandleWindow.SetActive(false);
        eventText.text = string.Empty;
    }
}
