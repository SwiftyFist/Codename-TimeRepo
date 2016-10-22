using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{
    public static player_script pl_script;
    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AudioSource _jumpsound;
    private float direction;

    public bool lookright = true;
    [SerializeField]
    private bool isGrounded = false;

    public float JumpForce = 5f;
    public float velocity = 5f;
    public Transform player_ground;
    public LayerMask  Layer_Ground;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _jumpsound = GetComponent<AudioSource>();
        pl_script = this;
    }


   


    void Update()
    {

        direction = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(new Vector2(player_ground.position.x, player_ground.position.y), 0.1f, Layer_Ground);
        _animator.SetBool("Ground", isGrounded);

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
       
        if (direction != 0)
        {
            if (lookright)
            {
                _rigidbody.velocity = new Vector2(direction * velocity, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(direction * velocity, _rigidbody.velocity.y);
            }
                _animator.SetFloat("Horizontal_Speed", Mathf.Abs (_rigidbody.velocity.x));
        }
        else
        {
            _animator.SetFloat("Horizontal_Speed", 0f);
        }
        _animator.SetFloat("Vertical_Speed", _rigidbody.velocity.y);


       


    }



    public void Gira()
    {
		
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        lookright = !lookright;
    }
    void Salta()
    {
        _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        _jumpsound.Play();
    }
}
