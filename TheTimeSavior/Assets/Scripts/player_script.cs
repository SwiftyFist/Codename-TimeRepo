using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{
	
	//Script richiamabile ovunque
    public static player_script pl_script;
	public float UpgradePoints = 0; 
	public   Transform _transform;
	//variabile che contiene la pos del player nel level_hub
	public Vector3 position;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AudioSource _jumpsound;
	//Variabile che contiene il valore dell'asse orizzontale
    private float direction;
	//Lookright è a true perchè il player guarda a destra 
    public bool lookright = true;
    [SerializeField]
	//controlla se è a terra
    private bool isGrounded = false;

    public float JumpForce = 6f;
	float moveVelocity;
    public float velocity = 5f;
    public Transform player_ground;
    public LayerMask  Layer_Ground;
    
   //Dentro l'awake fa in modo che il player non venga mai ricaricato con il cambio di scena
	void Awake()
    {
		if (pl_script != null) 
		{
			GameObject.Destroy (gameObject);
		} 
		else 
		{
			GameObject.DontDestroyOnLoad (gameObject);
			pl_script = this;
		}

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _jumpsound = GetComponent<AudioSource>();
		position = new Vector3 (_transform.position.x, _transform.position.y, _transform.position.z);
       
   }


   


    void Update()
    {
		
		//Prende il valore dell'asse x
        direction = Input.GetAxis("Horizontal");
		//Per vedere se è a terra controlla con un cerchio molto piccolo ai piedi del player(player_ground) se si incontra con il terreno (layer_ground)
        isGrounded = Physics2D.OverlapCircle(new Vector2(player_ground.position.x, player_ground.position.y), 0.1f, Layer_Ground);
        _animator.SetBool("Ground", isGrounded);

		//Se premendo il tasto salta (Spazio da tastiera o rb da controller) il player è a terra allora va ad eseguire la funzione salta
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Salta();
            }
        }
    }

        



    void FixedUpdate()
    {
		moveVelocity = 0;
       //muove il giocatore se non sta fermo sull'asse horizontal e imposta la velocità in base se va a sinistra o a destra
		//Imposta le variabili del animator per eseguire le animazioni in maniera corretta
        if (direction != 0.1)
        {
			    moveVelocity = velocity;
			    _rigidbody.velocity = new Vector2(direction * moveVelocity, _rigidbody.velocity.y);
                _animator.SetFloat("Horizontal_Speed", Mathf.Abs (_rigidbody.velocity.x));
        }
        else
        {
            _animator.SetFloat("Horizontal_Speed", 0f);
        }
		//Per le due animazioni di salto
        //_animator.SetFloat("Vertical_Speed", _rigidbody.velocity.y);


       


    }


	//Fa girare il player flippando lo sprite e la variabile bool lookright
    public void Gira()
    {
		
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        lookright = !lookright;
    }
	//Applica una forza verso l'alto per saltare ed esegue il suond effect del salto
    void Salta()
    {
        _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        _jumpsound.Play();
    }
}
