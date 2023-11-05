using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] public int maximumHealth = 100;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private List<Vector3> spawnPoints;
    [SerializeField] private bool killDevTool = false;
    [SerializeField] private bool damageDevTool = false;
    private float currentHealth = 1;
    private float drunkLvl;
    [SerializeField] private float drunkDoT;
    [SerializeField] private float drunkTickTime;
    private float lastTick;    

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
        lastTick = Time.time;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public void fullHealth() {
        this.setCurrentHealth(maximumHealth);
    }


    public void damage(float damage)
    {
        Opposite opp = GameObject.Find("GameManager").GetComponent<Opposite>();
        if (opp.isOppositeDay)
        {
            GameObject target = (this.GetComponent<move_1>().playerOne) ? opp.getP2() : opp.getP1();
            target.GetComponent<health>().direct(damage);
        } else
        {
            direct(damage);
        }
    }

    public void direct(float damage)
    {
        currentHealth = (currentHealth - damage > 0) ? currentHealth - damage : 0;
        if (currentHealth == 0)
        {
            kill();
        }
    }


    public void heal(float health)
    {
        currentHealth = (currentHealth + health < maximumHealth) ? currentHealth + health : maximumHealth;
    }


    public void kill()
    {
        int nextSpawnIndex = Random.Range(0, spawnPoints.Count);
        trans.position = spawnPoints[nextSpawnIndex];
        currentHealth = maximumHealth;
        Opposite opp = GameObject.Find("GameManager").GetComponent<Opposite>();
        opp.NormalTime();

    }

    public void setDrunkLevel(int lev) {
        if (drunkLvl == 0) {
            lastTick = Time.time;
        }
        drunkLvl = lev;
    }


    // Update is called once per frame
    void Update()
    {
        if (killDevTool)
        {
            killDevTool = false;
            kill();
        }

        if (damageDevTool)
        {
            damageDevTool = false;
            damage(35);
        }

        if (drunkLvl > 0) {
            if ((Time.time - lastTick) > drunkTickTime) {
                this.damage(drunkDoT * drunkLvl);
                lastTick = Time.time;
            }
        }
    }
}
