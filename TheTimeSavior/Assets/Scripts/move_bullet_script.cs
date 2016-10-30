using UnityEngine;
using System.Collections;

public class move_bullet_script : MonoBehaviour {

	//Velocità del proiettile 
	public int moveSpeed = 25;
	public int pointsToAdd = 10;




	void Update () 
	{
		
		//Fa un translate del proiettile e dopo due secondi viene distrutto il proiettile
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 2);

		
	}



	// Se il proiettile collide con il nemico questo viene distrutto ( da modificare) 
	// Ogni volta che un nemico viene distrutto anche il proiettile si distrugge e il Destroyer_Player si rallenta di 0.3 fino alla velocità minima di Destroyer_player
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy") 
		{
			Destroy (collision.gameObject);
			Destroy (gameObject);
			score_manager_script.AddPoints(pointsToAdd);
			  if(destroyer_player.velocityTimeProblem>=1.9)
			      {
			         destroyer_player.velocityTimeProblem = destroyer_player.velocityTimeProblem -0.3f;
			      }
		}
		//Se il proiettile collide con le piattaforme di gioco , il proiettile si distrugge
		//NON FUNZIONANTE
		else if (collision.gameObject.tag == "LevelObject") 
		{
			Destroy (gameObject);

	    }


}



}