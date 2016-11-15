using UnityEngine;
using UnityEditor;
using System.Collections;

public class enemy_script : MonoBehaviour
{
	
	//Velocità del nemico
    public float velocity = 5f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;

	public Transform enemy_ground;
    public LayerMask layer_ground;
	private bool isGrounded = false;
    


	//facing
	bool canflip = true;
	bool facingRight = false;



	//attacking
	public float AttackTime = 2f;
	float startAttackTime;
	bool attacking = false;


    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
		//la x del nemico è a -1 perchè quando è creato guarda a sinistra
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        
    }

    void Update()
    {
		//Per vedere se è a terra controlla con un cerchio molto piccolo ai piedi del nemico (enemy_ground) se si incontra con il terreno (layer_ground)
        isGrounded = Physics2D.OverlapCircle(new Vector2(enemy_ground.position.x, enemy_ground.position.y), 0.1f, layer_ground);



    }
    void FixedUpdate()
    {
		AttackTime -= 0.01f;
		//se guarda a sinistra si muove a sinistra , e viceversa
		/*if (facingRight)
        {
            _rigidbody.velocity = Vector2.right * velocity;
        }
        else
        {
            _rigidbody.velocity = Vector2.left * velocity;
        }*/





    }

	//Quando il nemico collide con il player il destroyer player si velocizza di 1 ( Valore poi da testare a fondo per bilanciamento )
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyer_player.velocityTimeProblem = destroyer_player.velocityTimeProblem+1f;
        }
    }

	//Il nemico ha come figlio un oggetto chiamato enemy_view (è un trigger) che corrisponde alla vista del nemico
	//Quando il nemico guarda a sinistra ma il player è alla sua destra il nemico si gira, e viceversa
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
			
			if (facingRight && collision.transform.position.x < _transform.position.x || !facingRight && collision.transform.position.x > _transform.position.x) {
				Gira ();
			}
			//canflip = false;
			attacking = true;
			startAttackTime = Time.time + AttackTime;
        }

    }

	public void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.gameObject.name == "Player") {
			
				if (AttackTime <= 0f) {
					attacking = false;
				}
			if (!attacking) {
				if (facingRight && collision.transform.position.x < _transform.position.x || !facingRight && collision.transform.position.x > _transform.position.x)
					Gira ();
			} else{
			    if(!facingRight) 
					_rigidbody.velocity = Vector2.left * velocity ;
				else 
					_rigidbody.velocity = Vector2.right * velocity;

				//da inserire modifica a l'animator
				}
			}
		}



	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player") {
			canflip = true;
			attacking = false;
			AttackTime = 2;
		}
	}



	//Fa girare il nemico flippando lo sprite e la variabile bool canflip
    public void Gira()
    {
		if (!canflip)
			return;
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
		facingRight = !facingRight;
    }
}
