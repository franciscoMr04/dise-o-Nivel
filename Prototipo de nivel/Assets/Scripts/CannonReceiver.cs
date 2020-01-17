using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CannonReceiver : MonoBehaviour
{
    //Mensaje para recoger el arma
    public GameObject mensaje;
    public FirstPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        mensaje.SetActive(false);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mensaje.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.SetDerecho();
                gameObject.SetActive(false);
                mensaje.SetActive(false);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        mensaje.SetActive(false);
    }
}
