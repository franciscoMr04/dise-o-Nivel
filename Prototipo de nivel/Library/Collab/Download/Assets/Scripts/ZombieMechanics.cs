using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMechanics : MonoBehaviour
{
    private int vidas;
    public Transform target;
    private NavMeshAgent agent;
    public bool cannonDmg;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vidas = 3;
        cannonDmg = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) agent.destination = target.position;
        if (vidas <= 0) Destroy(this.gameObject);
        if (cannonDmg) time += Time.deltaTime;
        if (time >= 1) Destroy(this.gameObject);
    }

    public void DamagedByCannon()
    {
        cannonDmg = true;
    }
    public void AddDamage(int dmg)
    {
        vidas -= dmg;
    }
}
