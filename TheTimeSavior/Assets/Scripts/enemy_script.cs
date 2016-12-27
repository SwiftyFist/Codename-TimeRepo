﻿using UnityEngine;
//using UnityEditor;
using System.Collections;

public class enemy_script : MonoBehaviour
{
	
	//Velocità del nemico
    float velocity ;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
	private Animator _animator;
	public Transform enemy_ground;
    public LayerMask layer_ground;
	private bool isGrounded = false;
	public bool playerInRangeWalk;
	public bool playerInRangeRotate;
	public LayerMask playerLayer;
	public float playerRangeWalk;
	public float playerRangeRotate;
	private bool Triggered;
	private bool Rotate;
	bool facingLeft = true;



    void Awake()
    {
		_animator = GetComponent<Animator> (); 
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
		//la x del nemico è a -1 perchè quando è creato guarda a sinistra
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        
    }

    void Update()
	{
		//Per vedere se è a terra controlla con un cerchio molto piccolo ai piedi del nemico (enemy_ground) se si incontra con il terreno (layer_ground)
		isGrounded = Physics2D.OverlapCircle (new Vector2 (enemy_ground.position.x, enemy_ground.position.y), 0.1f, layer_ground);

		if (playerInRangeRotate == false) {
			playerInRangeWalk = Physics2D.OverlapCircle (transform.position, playerRangeWalk, playerLayer);
		}
		playerInRangeRotate = Physics2D.OverlapCircle (transform.position, playerRangeRotate, playerLayer);

		if (playerInRangeWalk) {
			Rotate = false;
			Triggered = true;
			_animator.SetBool ("Rotate", Rotate);
			_animator.SetBool ("Triggered", Triggered);
			velocity = 6f;
			if (facingLeft && player_script.pl_script.transform.position.x > _transform.position.x || !facingLeft && player_script.pl_script.transform.position.x < _transform.position.x) {
				Gira ();
			}
			if (facingLeft) {
				_rigidbody.velocity = Vector2.left * velocity;
			} else {
				_rigidbody.velocity = Vector2.right * velocity;
			}
		}
		if (playerInRangeRotate) {
			playerInRangeWalk = false;
			Triggered = false;
			Rotate = true;
			_animator.SetBool ("Rotate", Rotate);
			_animator.SetBool ("Triggered", Triggered);
			velocity = 10f;
		
			if (facingLeft) {
				_rigidbody.velocity = Vector2.left * velocity;
			} else {
				_rigidbody.velocity = Vector2.right * velocity;
			}
		}
    }


	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (transform.position , playerRangeWalk);
		Gizmos.color = Color.black;
		Gizmos.DrawSphere (transform.position , playerRangeRotate);
	}


    	

	//Quando il nemico collide con il player il destroyer player si velocizza di 1 ( Valore poi da testare a fondo per bilanciamento )
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyer_player.velocityTimeProblem = destroyer_player.velocityTimeProblem+1f;
        }
    }


	//Fa girare il nemico flippando lo sprite e la variabile bool canflip
    public void Gira()
    {
		
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
		facingLeft = !facingLeft;
    }
}
