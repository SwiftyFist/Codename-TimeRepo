using UnityEngine;
using UnityEditor;
using System.Collections;

public class enemy_script : MonoBehaviour
{
    public float jumpforce = 100f;
    public float velocity = 2f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private bool lookleft = true;
    public bool canjump = false;
    public Transform enemy_ground;
    public LayerMask layer_ground;
    public bool isGrounded = false;
    


    void Awake()
    {
         
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        
    }

    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(new Vector2(enemy_ground.position.x, enemy_ground.position.y), 0.1f, layer_ground);

        if ((canjump) && (isGrounded))
        {

            Salta();

        }
    }
    void FixedUpdate()
    {
        if (lookleft)
        {
            _rigidbody.velocity = Vector2.left * velocity;
        }
        else
        {
            _rigidbody.velocity = Vector2.right * velocity;
        }





    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            destroyer_player.ds_player.velocityTimeProblem = destroyer_player.ds_player.velocityTimeProblem+1f;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
			
            if (lookleft && collision.transform.position.x > _transform.position.x || !lookleft && collision.transform.position.x < _transform.position.x)
                Gira();

        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            canjump = true;


    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        canjump = false;
    }


    public void Salta()
    {
        _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);


    }


    public void Gira()
    {
        _transform.localScale = new Vector3(_transform.localScale.x * -1, _transform.localScale.y, _transform.localScale.z);
        lookleft = !lookleft;
    }
}
