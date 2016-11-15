using UnityEngine;
using System.Collections;

public class move_bullet_script : MonoBehaviour {

	//Velocità del proiettile 
	public int moveSpeed ;
	public int pointsToAdd ;
	public int damageToGive;



	void Update () 
	{
		
		//Fa un translate del proiettile e dopo due secondi viene distrutto il proiettile
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 2);

		
	}



	// Se il proiettile collide con il nemico questo viene danneggiato di 1 attraverso la funzione giveDamage del enemy health manager
	// Ogni volta che il proiettile collide con nemici o elementi di gioco questo viene distrutto

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			Destroy (gameObject);
			other.GetComponent<enemy_health_manager_script>().giveDamage(damageToGive);

		}

		if (other.tag == "LevelObject"){
			Destroy (gameObject);
		}
	}


		
	   

 }



