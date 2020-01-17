using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndGame : MonoBehaviour
{
    public GameObject mensaje;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        mensaje.GetComponent<TextMeshProUGUI>().text = "VICTORIA";
        mensaje.GetComponent<Animator>().SetTrigger("mensaje");
    }
}
