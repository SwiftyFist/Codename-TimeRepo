using UnityEngine;
using System.Collections;

public class enemy2_script : MonoBehaviour {

	private player_script thePlayer;

	public float moveSpeed;

	public float playerRange;

	public LayerMask playerLayer;

	public bool playerInRange;


	void Start () {
	
		thePlayer = FindObjectOfType<player_script> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);

		if (playerInRange) 
		{
			transform.position = Vector3.MoveTowards (transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
		}
		}

	void OnDrawGizmosSelected(){
	
		Gizmos.DrawSphere (transform.position , playerRange);
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			destroyer_player.velocityTimeProblem = destroyer_player.velocityTimeProblem+1f;
		}
	}
}
