using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    private int enemigosRonda;
    private int enemigosRest;
    public GameObject Enemy;
    private int contadorEnemigos;
    public int enemDerrotado;
    public float tiempoRespawn;
    //Tiempo de salida de enemigos por ronda
    public float coolR1=0;
    public float coolR2=0;
    public float coolR3=0;
    public float coolR4=0;
    public float coolR5=0;
    public int maxEnemies = 10;
    public GameObject roundAdvisor;
    public GameObject enemiedLeftAdvisor;
    public GameObject doorsAdvisor;
    [SerializeField] private List<Transform> listaRespawns;
    public Transform respawn0;
    public Transform respawn1;
    public Transform respawn2;
    public Transform respawn3;
    public Transform respawn4;
    public Transform respawn5;
    public Transform respawn6;
    public Transform respawn7;
    public Transform respawn8;
    public List<Animator> listaPuertasRonda2;
    public List<Animator> listaPuertasRonda3;
    public List<Animator> listaPuertasRonda4;
    public Animator mecanPuertas5;
    public Animator mecanPuertas6;
    [SerializeField] private int ronda;

    // Start is called before the first frame update
    void Start()
    {
        ronda = 1;
        contadorEnemigos =0;
        enemDerrotado = 0;
        listaRespawns = new List<Transform>();
        listaRespawns.Add(respawn0);
        listaRespawns.Add(respawn1);
        listaRespawns.Add(respawn2);
        tiempoRespawn = 3f;
        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
        enemigosRonda = 10;
    }

    void Update()
    {
        enemigosRest = enemigosRonda - enemDerrotado;
        enemiedLeftAdvisor.GetComponent<TextMeshProUGUI>().text = "Enemigos restantes: " + enemigosRest;
        roundAdvisor.GetComponent<TextMeshProUGUI>().text = "RONDA " + ronda;
        System.Random rand = new System.Random();
        if (tiempoRespawn<= 0)
        {
            switch (ronda)
            {
                case 1:
                    if (enemDerrotado >= enemigosRonda)
                    {
                        ronda = 2;
                        foreach (Animator anim in listaPuertasRonda2) anim.SetTrigger("Abrir");
                        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        doorsAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        listaRespawns.Add(respawn3);
                        listaRespawns.Add(respawn4);
                        enemigosRonda = 10;
                        enemDerrotado = 0;
                    }
                    else
                    {
                        if (contadorEnemigos < maxEnemies)
                        {
                            int cual = rand.Next(listaRespawns.Count);
                            Instantiate(Enemy, listaRespawns[cual].position, Quaternion.identity);
                            contadorEnemigos += 1;
                            tiempoRespawn = coolR1;
                        }

                    }
                    break;


                case 2:
                    if (enemDerrotado >= enemigosRonda)
                    {
                        ronda = 3;
                        foreach (Animator anim in listaPuertasRonda3) anim.SetTrigger("Abrir");
                        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        doorsAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        listaRespawns.Add(respawn5);
                        listaRespawns.Add(respawn6);
                        enemigosRonda = 15;
                        enemDerrotado = 0;
                    }
                    else
                    {
                        if (contadorEnemigos < maxEnemies)
                        {
                            int cual = rand.Next(listaRespawns.Count);
                            Instantiate(Enemy, listaRespawns[cual].position, Quaternion.identity);
                            contadorEnemigos += 1;
                            tiempoRespawn = coolR2;
                        }

                    }

                    break;


                case 3:
                    if (enemDerrotado >= enemigosRonda)
                    {
                        ronda = 4;
                        foreach (Animator anim in listaPuertasRonda4) anim.SetTrigger("Abrir");
                        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        doorsAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        listaRespawns.Add(respawn7);
                        listaRespawns.Add(respawn8);
                        enemigosRonda = 15;
                        enemDerrotado = 0;
                    }
                    else
                    {
                        if (contadorEnemigos < maxEnemies)
                        {
                            int cual = rand.Next(listaRespawns.Count);
                            Instantiate(Enemy, listaRespawns[cual].position, Quaternion.identity);
                            contadorEnemigos += 1;
                            tiempoRespawn = coolR3;
                        }

                    }

                    break;


                case 4:
                    if (enemDerrotado >= enemigosRonda)
                    {
                        ronda = 5;
                        mecanPuertas5.SetTrigger("Abrir");
                        mecanPuertas6.SetTrigger("Abrir");
                        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        doorsAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        enemigosRonda = 15;
                        enemDerrotado = 0;
                    }
                    else
                    {
                        if (contadorEnemigos < maxEnemies)
                        {
                            int cual = rand.Next(listaRespawns.Count);
                            Instantiate(Enemy, listaRespawns[cual].position, Quaternion.identity);
                            contadorEnemigos += 1;
                            tiempoRespawn = coolR4;
                        }

                    }

                    break;

                case 5:
                    if (enemDerrotado >= enemigosRonda)
                    {
                        ronda = 6;
                        mecanPuertas6.SetTrigger("Abrir");
                        roundAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        doorsAdvisor.GetComponent<Animator>().SetTrigger("aviso");
                        enemigosRonda = 10;
                        enemDerrotado = 0;
                    }else
                    {
                        if (contadorEnemigos < maxEnemies)
                        {
                            int cual = rand.Next(listaRespawns.Count);
                            Instantiate(Enemy, listaRespawns[cual].position, Quaternion.identity);
                            contadorEnemigos += 1;
                            tiempoRespawn = coolR5;
                        }

                    }
                    break;

            }

        }
        
        tiempoRespawn -= Time.deltaTime;
    }

    public void ApuntarKill()
    {
        enemDerrotado++;
        contadorEnemigos--;
    }
}
