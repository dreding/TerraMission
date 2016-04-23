using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragRotate : MonoBehaviour 
{
	public Transform target;
    public float ScrollSpeed = 1f;
	public float TScrollSpeed = 1f;
	public float PanSensive = 0.1f;
	public float TPanSensive = 0.1f;
	public float distance = 10.0f;
	
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float xTSpeed = 250.0f;
	public float yTSpeed = 120.0f;
	
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	
	private float x = 0.0f;
	private float y = 0.0f;

	private bool isMoving = false;
	private bool isTranslating = false;
	private bool isTTranslating = false;
	private Vector3 previousPosition;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    public float Elastic = 1f;

	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		Init();
	}

	void LateUpdate () 
	{
		if (!EventSystem.current.IsPointerOverGameObject() && !EventSystem.current.IsPointerOverGameObject(0) && !EventSystem.current.IsPointerOverGameObject(1)) 
		{
			Scroll ();
			Rotate ();
			Translate ();
			TScroll ();
			TTranslate ();
		}

        Quaternion rotation = Quaternion.Euler(y, x, 0f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        targetPosition = position;
        targetRotation = rotation;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*Elastic);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*Elastic);
    }

	void Init()
	{
		if (target) 
		{
			x += 0f;
			y -= 0f;
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);


            Quaternion rotation = Quaternion.Euler(y, x, 0f);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            //transform.rotation = rotation;
            //transform.position = position;
            targetPosition = position;
            targetRotation = rotation;
		}
	}

	static float ClampAngle (float angle, float min, float max) 
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}

	void Scroll()
	{
        if(Input.touchCount == 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                distance -= ScrollSpeed;
                Quaternion rotation = Quaternion.Euler(y, x, 0f);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                //transform.rotation = rotation;
                //transform.position = position;
                targetPosition = position;
                targetRotation = rotation;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                distance += ScrollSpeed;
                Quaternion rotation = Quaternion.Euler(y, x, 0f);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                //transform.rotation = rotation;
                //transform.position = position;
                targetPosition = position;
                targetRotation = rotation;
            }
        }
	}

	void Rotate()
	{
        if(Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isMoving = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                isMoving = false;
            }

            if (isMoving && target)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0f);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                //transform.rotation = rotation;
                //transform.position = position;
                targetPosition = position;
                targetRotation = rotation;

                target.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
            }
        }
	}

	void Translate()
	{
        if(Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTranslating = true;
                previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isTranslating = false;
            }

            if (isTranslating && target)
            {
                Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 delta = previousPosition - mousePos;

                target.Translate(new Vector3(delta.x, 0f, delta.y) * PanSensive, Space.Self);
                previousPosition = mousePos;

                Quaternion rotation = Quaternion.Euler(y, x, 0f);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                //transform.rotation = rotation;
                //transform.position = position;
                targetPosition = position;
                targetRotation = rotation;
            }
        }
		
	}

	void TScroll()
	{
		if (Input.touchCount == 2) 
		{
			float thX = 2f;
			float thY = 2f;

			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);
			int rightIndex = 0;
			float rotationDir = 0f;

			rightIndex = touchZero.position.x > touchOne.position.x ? 0 : 1;

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = Mathf.Abs (touchZeroPrevPos.x - touchOnePrevPos.x);
			float touchDeltaMag = Mathf.Abs (touchZero.position.x - touchOne.position.x);
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			float prevTouchDeltaMagX = Mathf.Abs (touchZeroPrevPos.y - touchOnePrevPos.y);
			float touchDeltaMagX = Mathf.Abs (touchZero.position.y - touchOne.position.y);
			float deltaMagnitudeDiffX = prevTouchDeltaMagX - touchDeltaMagX;

            float prevTouchDeltaMagFull = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMagFull = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiffFull = prevTouchDeltaMagFull - touchDeltaMagFull;

            if (Mathf.Abs(deltaMagnitudeDiffFull) > 3f)
            {
                distance += deltaMagnitudeDiffFull * TScrollSpeed;
            }
            else if (Mathf.Abs(touchZero.deltaPosition.x) < thX && Mathf.Abs(touchOne.deltaPosition.x) < thX && (Mathf.Sign(touchZero.deltaPosition.y) == Mathf.Sign(touchOne.deltaPosition.y)))
            {
                y += touchZero.deltaPosition.y * xTSpeed * 0.02f;

                y = ClampAngle(y, 20f, 80f);
            }
            else
            {
                if (rightIndex == 0)
                {
                    if (touchZero.deltaPosition.y < 0f)
                    {
                        rotationDir = -1f;
                    }
                    else
                    {
                        rotationDir = 1f;
                    }
                }
                if (rightIndex == 1)
                {
                    if (touchOne.deltaPosition.y < 0f)
                    {
                        rotationDir = -1f;
                    }
                    else
                    {
                        rotationDir = 1f;
                    }
                }
                x += (Mathf.Abs(Input.GetTouch(0).deltaPosition.magnitude) + Mathf.Abs(Input.GetTouch(1).deltaPosition.magnitude)) * yTSpeed * 0.02f * rotationDir;
            }
            distance = Mathf.Clamp(distance, 1f, 50f);
			Quaternion rotation = Quaternion.Euler(y, x, 0f);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            targetPosition = position;
            targetRotation = rotation;

			target.rotation = Quaternion.Euler (new Vector3 (0f, transform.rotation.eulerAngles.y, 0f));
		} 
	}

	void TTranslate()
	{
		if (Input.touchCount == 1) 
		{
			if (!isTTranslating) 
			{
				isTTranslating = true;
				previousPosition = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			}

			if (isTTranslating && target)
            {
				Vector3 mousePos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
				Vector3 delta = previousPosition - mousePos;

				target.Translate (new Vector3 (delta.x, 0f, delta.y) * TPanSensive, Space.Self);
				previousPosition = mousePos;

				Quaternion rotation = Quaternion.Euler (y, x, 0f);
				Vector3 position = rotation * new Vector3 (0.0f, 0.0f, -distance) + target.position;

                targetPosition = position;
                targetRotation = rotation;

                target.rotation = Quaternion.Euler (new Vector3 (0f, transform.rotation.eulerAngles.y, 0f));
			}
		} 
		else 
		{
			isTTranslating = false;
		}
	}
	
}
