using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaController : MonoBehaviour
{
    public float speed;
    
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
        Destroy(this.gameObject, 4f);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemigos")
        {
            collision.gameObject.GetComponent<ZombieMechanics>().AddDamage(1);
        }
        Destroy(this.gameObject);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}
