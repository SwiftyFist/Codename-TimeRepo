using UnityEngine;
using System.Collections;

public class destroyer_object : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Enemy") Destroy(collision.gameObject); 

    }
}
