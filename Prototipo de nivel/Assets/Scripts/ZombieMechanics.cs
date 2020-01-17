using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ZombieMechanics : MonoBehaviour
{
    [SerializeField]  private int vidas;
    public Transform target;
    private NavMeshAgent agent;
    private float tempDaño;
    public bool cannonDmg;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vidas = 6;
        cannonDmg = false;
        time = 0.0f;
        tempDaño = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if ((Vector3.Distance(transform.position, target.position) <= 1f) && (tempDaño <= 0))
        {
            target.GetComponent<FirstPersonController>().DañarJugador();
            tempDaño = 5f;
        }
        if ((target != null)&&(agent.enabled == true)) agent.destination = target.position;

        //Condiciones de muerte
        if ((vidas <= 0)||(time>=1)) BajaEnemiga();
        if (cannonDmg) time += Time.deltaTime;
        tempDaño -= Time.deltaTime;
    }

    public void DamagedByCannon()
    {

        cannonDmg = true;
        agent.enabled = false;
    }   

    public void BajaEnemiga()
    {
        GameObject GM = GameObject.Find("GameManager");
        GM.GetComponent<LevelManager>().ApuntarKill();
        Destroy(this.gameObject);
    }
    public void AddDamage(int dmg)
    {
        vidas -= dmg;
    }
}
