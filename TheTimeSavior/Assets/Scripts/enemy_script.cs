using UnityEngine;
using UnityEditor;
using System.Collections;

public class enemy_script : MonoBehaviour
{
	//Potenza del salto NON FUNZIONANTE
    public float jumpforce = 100f;
	//Velocità del nemico
    public float velocity = 2f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
	//lookleft è a true perchè il nemico guarda a sinistra quando viene creato
    private bool lookleft = true;
	//bool per la possibiltà di saltare NON FUNZIONANTE
    public bool canjump = false;
    public Transform enemy_ground;
    public LayerMask layer_ground;
	//bool per vedere se il nemico è a terra
    public bool isGrounded = false;
    


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

		//NON FUNZIONANTE
        if ((canjump) && (isGrounded))
        {

            Salta();

        }
    }
    void FixedUpdate()
    {
		//se guarda a sinistra si muove a sinistra , e viceversa
        if (lookleft)
        {
            _rigidbody.velocity = Vector2.left * velocity;
        }
        else
        {
            _rigidbody.velocity = Vector2.right * velocity;
        }





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
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
			
            if (lookleft && collision.transform.position.x > _transform.position.x || !lookleft && collision.transform.position.x < _transform.position.x)
                Gira();

        }

    }

	//quando il nemico vede il player setta canjump a true per permettergli di saltare
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            canjump = true;


    }
	//quando il player non viene più visto dal nemico il nemico non salta mai settando canjump a false
    public void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.name == "Player")
        canjump = false;
    }

	//Funzione che aggiunge una forza verso l'alto per far saltare il nemico
    public void Salta()
    {
        _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);


    }

	//Fa girare il nemico flippando lo sprite e la variabile bool lookleft
    public void Gira()
    {
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        lookleft = !lookleft;
    }
}
