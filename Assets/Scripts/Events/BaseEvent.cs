using UnityEngine;
using System.Collections;

public abstract class BaseEvent : MonoBehaviour {
    public string eventText;
    public int probability = 1;
    public int multiTimes = 1;
    public int maxProb = 100;

    private int _isUsed = 0;

    int lastTry = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void OnUpdate () {
        if (Mathf.FloorToInt(TimeManager.Instance.passedMinutes) > lastTry)
        {
            lastTry = Mathf.FloorToInt(TimeManager.Instance.passedMinutes);
            int chanse = Random.Range(0, maxProb);
            if (chanse <= probability)
                OnSuccess();
        }
	}

    protected void OnSuccess()
    {
        BaseParamsUIManager.Instance.ShowEventWindow(eventText);
    }
}
