using UnityEngine;
using System.Collections;

public class gun_script : MonoBehaviour {

	public static gun_script gun;
    public float FireRate = 0;
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
		//Sparo quando si tiene premuto
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
    Mouse contiene la posizione del mouse all'interno della schermata di gioco
    firepointposition contiene la posizione del oggetto FirePoint
    hit fa un raycast per vedere con cosa collide 
    Dopo viene calcolata la direzione in cui si sta sparando (sinistra o destra) e poi spara il proiettile
	*/
    void Shoot()
    {
		
         Mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
         firePointPosition = new Vector2(FirePoint.position.x,FirePoint.position.y);
         //hit = Physics2D.Raycast(firePointPosition, Mouse - firePointPosition, 100, whatToHit);
		CalculateWhereIamLooking();
		Effect();

    }


	/*Funzione che controlla se il player guarda a sinistra o a destra settando così il vettore direction in maniera corretta prima dello sparo effettivo
	la funzione viene richiamata prima di effect, così per ogni sparo setta il vettore */
	void CalculateWhereIamLooking()
	{
		if (player_script.pl_script.lookright == true) {
			
			direction = Vector3.right;
		}
		else
		{
			
			direction = Vector3.left;

		}

	}


	//Funzione che fa partire il proiettile (prefab) dalla posizione e rotazione del firepoint (oggetto attaccato a l'uscita dell'arma)
    void Effect()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
		_shootSound.Play ();
    }



}
