using UnityEngine;
using System.Collections;

public class enemy_health_manager_script : MonoBehaviour {

	public static enemy_health_manager_script hl_script;
	public int enemyHealth;

	//public GameObject deathEffect;

	public int pointsOnDeath;

	void Awake () {
	
		hl_script = this;
	}
	


	void Update () {
	
		if (enemyHealth <= 0) {
			score_manager_script.AddPoints (pointsOnDeath);
			if(destroyer_player.velocityTimeProblem>=2.9)
			{
				destroyer_player.velocityTimeProblem = destroyer_player.velocityTimeProblem -0.3f;
			}
			Destroy (gameObject);
		}
	}
    

	public void giveDamage(int damageToGive)
		{
			enemyHealth -= damageToGive;
		}
	
}
