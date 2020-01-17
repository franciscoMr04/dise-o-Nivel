using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public LayerMask queExplota;
    public float speed;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
        Destroy(this.gameObject, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        esfera.transform.position = transform.position;
        Destroy(esfera, 0.2f);
        
        Collider[] cosas = Physics.OverlapSphere(esfera.transform.position, 10, queExplota);
        foreach (Collider cosa in cosas)
        {
            Rigidbody rb = cosa.GetComponent<Rigidbody>();
            ZombieMechanics zm = cosa.GetComponent<ZombieMechanics>();
            if (rb!= null)
            {
                rb.AddExplosionForce(10, esfera.transform.position, 30, 0.3f, ForceMode.Impulse);
                zm.DamagedByCannon();
            }
            
        }
        
        

        Destroy(this.gameObject);
    }
}
