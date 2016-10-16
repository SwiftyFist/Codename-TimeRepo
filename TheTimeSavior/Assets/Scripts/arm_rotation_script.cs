using UnityEngine;
using System.Collections;

public class arm_rotation_script : MonoBehaviour {
    public int rotationOffset = 90;
    private Transform _transform;
    private Vector3 difference;
    void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
		//Controller Rotation (Right Stick ) NON FUNZIONANTE
        float x = Input.GetAxis("RightStickX");
        float y = Input.GetAxis("RightStickY");
        float aim_angle = 0.0f;
        // CANCEL ALL INPUT BELOW THIS FLOAT
        float R_analog_threshold = 0.20f;

        if (Mathf.Abs(x) < R_analog_threshold) { x = 0.0f; }

        if (Mathf.Abs(y) < R_analog_threshold) { y = 0.0f; }
        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {
            aim_angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f,0f,aim_angle);
        }


        //Mouse Rotation FUNZIONANTE
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        if (player_script.pl_script.lookright == false)
        {
            _transform.rotation = Quaternion.Euler(0f, 0f, rotZ + -180);
        }
    }


}

//Ciao scemo!