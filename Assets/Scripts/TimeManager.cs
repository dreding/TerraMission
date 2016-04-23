using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    public static TimeManager Instance;

    [SerializeField]
    public int timeSpeed { get; private set; }

    [SerializeField]
    private float _passedTime = 0f;

    [SerializeField]
    private float passedMinutes;

    [SerializeField]
    private int passedHours;

    [SerializeField]
    public int passedDays { get; private set; }

    [SerializeField]
    public int passedYars { get; private set; }

    public float PassedTime { get { return _passedTime; } }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        _passedTime += Time.deltaTime * timeSpeed;

        passedMinutes = _passedTime / 60;
      
        passedHours = Mathf.FloorToInt(passedMinutes / 60);

        passedDays = Mathf.FloorToInt(passedMinutes / Mars.Instance.dayLenght);

        passedYars = Mathf.FloorToInt(passedDays / Mars.Instance.yearLenght);
	}

    public void ChangeTimeSpeed(float newSpeed)
    {
        timeSpeed = (int)newSpeed;
    }

}
