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

	/*public void OnTriggerEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy") 
		{
			Destroy (collision.gameObject);
			Destroy (gameObject);
		}
	}*/
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy") 
		{
			Destroy (collision.gameObject);
			Destroy (gameObject);
			if(destroyer_player.ds_player.velocityTimeProblem>=1.9){
			destroyer_player.ds_player.velocityTimeProblem = destroyer_player.ds_player.velocityTimeProblem-0.3f;
			}
		}
	}


}
