using UnityEngine;
using System.Collections;

public class move_bullet_script : MonoBehaviour {

	//Velocità del proiettile 
	public int moveSpeed=25;





	void Update () 
	{
		
		//Fa un translate del proiettile , in gun.direction cambia il vettore se il player guarda a sinistra o destra
		transform.Translate (gun_script.gun.direction * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 1);

		
	}



}
