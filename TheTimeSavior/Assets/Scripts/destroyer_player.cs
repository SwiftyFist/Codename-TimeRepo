using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class destroyer_player : MonoBehaviour
{
	//ds_player è la variabile usata per accedere alle variabili pubbliche di questo script, velocityTimeProblem è la velocità del problema che insegue il player
    public static destroyer_player ds_player;
    public  float velocityTimeProblem = 2f;
    
    void Awake()
    {
        ds_player = this;
    }

	//Nel update viene mosso semplicemente verso destra il problema moltiplicando per velocityTimeProblem
    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocityTimeProblem;
    }

	//Quando entra in collisione con il player riparte la scena (game over)
	//Nella versione finale deve entrare nella scena level_hub dopo aver dato una schermata di game over con le statistiche sul punteggio
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") SceneManager.LoadScene("Level");

    }

  
}
