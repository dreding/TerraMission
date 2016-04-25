using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    [SerializeField]
    public int timeSpeed { get; private set; }

    [SerializeField]
    public float _passedTime { get; private set; }

    [SerializeField]
    public float passedMinutes { get; private set; }

    [SerializeField]
    float mins;

    [SerializeField]
    private int passedHours;

    [SerializeField]
    public int passedDays { get; private set; }

    [SerializeField]
    public int passedYears { get; private set; }

    void Awake()
    {
        timeSpeed = 1;
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _passedTime += Time.deltaTime * timeSpeed;

        passedMinutes = _passedTime / 60;
        mins = passedMinutes;

        passedHours = Mathf.FloorToInt(passedMinutes / 60);

        passedDays = Mathf.FloorToInt(passedMinutes / Mars.Instance.dayLenght);

        passedYears = Mathf.FloorToInt(passedMinutes / Mars.Instance.yearLenght);
    }

    public void ChangeTimeSpeed(float newSpeed)
    {
        timeSpeed = (int)newSpeed;
    }

}
