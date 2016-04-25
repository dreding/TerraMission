using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Robot : VehicleController {


	// Use this for initialization
	void Start () {
        isChoosed = true;
        _isMoving = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    void OnMouseDown()
    {
        Debug.Log("Robot");
        isChoosed = !isChoosed;
    }
    
}
