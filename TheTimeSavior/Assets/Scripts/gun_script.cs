using UnityEngine;
using System.Collections;

public class gun_script : MonoBehaviour {
	//Script richiamabile ovunque
	public static gun_script gun;
    public float FireRate = 10;
    public float damage = 10;
    public LayerMask whatToHit;
    public Transform BulletPrefab;
	public Vector3 direction;
	float timeToFire = 0;
    Transform FirePoint;
    private Vector2 Mouse;
    private Vector2 firePointPosition;
    private RaycastHit2D hit;
	private AudioSource _shootSound;


	//Nell'awake nella variabile transform FirePoint va a prendere il figlio dell'arma FirePoint se non è presente da un errore di debug.
	//Inoltre setta la variabile gun =this ovvero contiene lo script in se cosi da accedere alle variabili pubbliche da tutti gli script
	//_shootSound va a riprendere il suono 
    void Awake () {
		gun = this;
        FirePoint = transform.FindChild("FirePoint");
        if (FirePoint == null)
        {
            Debug.LogError("No Firepoint");
        }

		_shootSound = GetComponent<AudioSource>();
	}

	void Update () {
        

		//Sparo premendo il tasto Fire1 una volta
        if (FireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
		//Sparo quando si tiene premuto       Da rivedere
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time > timeToFire)
            {
				
                timeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }


	}


	/*Funzione che si occupa di gestire dove sparare e in che modo.
    Test:Mouse contiene la posizione del mouse all'interno della schermata di gioco
    Test:firepointposition contiene la posizione del oggetto FirePoint
    Test:hit fa un raycast per vedere con cosa collide 
    Al momento richiama solamente la funzione per sparare il proiettile, dovranno essere integrate altre funzioni
	*/
    void Shoot()
    {
		
         //Mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
         //firePointPosition = new Vector2(FirePoint.position.x,FirePoint.position.y);
         //hit = Physics2D.Raycast(firePointPosition, Mouse - firePointPosition, 100, whatToHit);
		 Effect();

    }


	//Funzione che fa partire il proiettile (prefab) dalla posizione e rotazione del firepoint (oggetto attaccato a l'uscita dell'arma)
	//Esegue il sound effect dello sparo
    void Effect()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
		_shootSound.Play ();
    }



}
