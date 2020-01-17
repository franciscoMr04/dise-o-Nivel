using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerA : MonoBehaviour 
{
    public GameObject linterna;
    // Start is called before the first frame update
    void Start()
    {
        linterna.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        
    }

    
}
