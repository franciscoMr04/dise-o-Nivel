using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
        Destroy(this.gameObject, 4f);
    }

}
