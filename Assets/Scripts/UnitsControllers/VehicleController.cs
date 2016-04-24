using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class VehicleController : MonoBehaviour {

    public float speed; // м/с
    public bool isChoosed = false;

    protected bool _isMoving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isChoosed || _isMoving)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.5f;

            Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero, float.PositiveInfinity, 1 << 8);
            if (hit)
            {
                MoveToTarget(hit.point);
            }
        }
	}

    protected void MoveToTarget(Vector2 pos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y, transform.position.z);
        float time = Vector3.Distance(this.transform.position, newPos) / speed;
        this.transform.DOMove(newPos, time);
    }
}
