using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SceneManager : MonoBehaviour
{
    public void Nivel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void VolverMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PantallaVictoria()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void PantallaDerrota()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
