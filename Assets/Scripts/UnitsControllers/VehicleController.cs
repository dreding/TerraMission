using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class VehicleController : MonoBehaviour {

    public float speed; // м/с
    public bool isChoosed = false;

    protected bool _isMoving = false;

    private Vector3 direction;
    private float arriveTime = 0;
    private Vector3 targetPos;
    private float startTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    protected void OnUpdate()
    {
        Move();
        if (!isChoosed || _isMoving)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.5f;

            Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero, float.PositiveInfinity);
            if (hit && hit.transform.tag == "Ground")
            {
                MoveToTarget(hit.point);
            }
        }
    }

    private void Move()
    {
        if (_isMoving && direction != Vector3.zero)
        {
            if (arriveTime <= TimeManager.Instance.passedMinutes)
            {
                this.transform.position = targetPos;
                _isMoving = false;
                direction = Vector3.zero;
                startTime = 0;
            }
            else
            {              
                Vector3 dif = direction * (TimeManager.Instance.passedMinutes - startTime);
                Vector3 newPos = transform.position + dif;
                newPos.z = transform.position.z;
                this.transform.position = newPos;
                startTime = TimeManager.Instance.passedMinutes;
            }
        }
    }

    protected void MoveToTarget(Vector2 pos)
    { 
        targetPos = new Vector3(pos.x, pos.y, transform.position.z);
        float dist = Vector3.Distance(this.transform.position, targetPos) / speed;
        float needTime = (dist * 222.2f) / (speed * 60);
        startTime = TimeManager.Instance.passedMinutes;
        arriveTime = TimeManager.Instance.passedMinutes + needTime;

        direction = (targetPos - transform.position).normalized;
        _isMoving = true;
    }
}
