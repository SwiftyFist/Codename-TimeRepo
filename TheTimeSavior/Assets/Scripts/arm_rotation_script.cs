using UnityEngine;
using System.Collections;

public class arm_rotation_script : MonoBehaviour {
    public int rotationOffset;
	public bool direction;
    private Transform _transform;
    private Vector3 difference;
	public Transform FirePoint;
    void Awake()
    {
		direction = true;
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
		
		//Controller Rotation (Right Stick ) NON FUNZIONANTE
        float x = Input.GetAxis("RightStickX");
        float y = Input.GetAxis("RightStickY");
        float aim_angle = 0.0f;
        // CANCEL ALL INPUT BELOW THIS FLOAT
        float R_analog_threshold = 0.1f;

        if (Mathf.Abs(x) < R_analog_threshold) { x = 0.0f; }

        if (Mathf.Abs(y) < R_analog_threshold) { y = 0.0f; }
        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {
			
            aim_angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			//Debug.Log ("rotation = "+aim_angle);
            _transform.rotation = Quaternion.Euler(0f,0f,aim_angle);

			if (aim_angle > 0f && aim_angle < 90f || aim_angle < 0 && aim_angle > -90f)
			{
				if (direction == false)
				{
					direction = true;
					Flip();
				}
			}
			if (aim_angle > 100f && aim_angle < 180f || aim_angle < -100f && aim_angle > -180f) 
			{
				if (direction == true)
				{
					direction = false;
					Flip ();
				}
			}
        }









		//Mouse Rotation FUNZIONANTE
		else{

        
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
		//Debug.Log ("rotation = "+rotZ);
		if (direction == true) 
		{
			rotationOffset = 0;
		}
		if (direction == false) 
		{
			rotationOffset = 0;
		}

		_transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

		if (rotZ > 0f && rotZ < 90f || rotZ < 0 && rotZ > -90f)
		{
			if (direction == false)
			{
				direction = true;
				Flip();
			}
		}
		if (rotZ > 100f && rotZ < 180f || rotZ < -100f && rotZ > -180f) 
		{
			if (direction == true)
			{
				direction = false;
				Flip ();
			}
		}
    }
	}
	void Flip()
	{
		if (direction == false && player_script.pl_script.lookright == true || direction == true && player_script.pl_script.lookright == false) 
		{
			player_script.pl_script.Gira ();
		}	
	

		_transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y * -1, _transform.localScale.z);
	}


}

