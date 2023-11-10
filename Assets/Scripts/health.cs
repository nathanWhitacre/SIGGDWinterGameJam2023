using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] public int maximumHealth = 100;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private List<Vector3> spawnPoints;
    [SerializeField] private float respawnDelay = 1.5f;
    [SerializeField] private bool killDevTool = false;
    [SerializeField] private bool damageDevTool = false;
    [SerializeField] public AudioSource[] punchKillVoiceLines;
    [SerializeField] private GameObject damageLineManager;
    private float currentHealth = 1;
    private float drunkLvl;
    [SerializeField] private float drunkDoT;
    [SerializeField] private float drunkTickTime;
    private float lastTick;

    bool isDead;
    bool isRespawning;
    float respawnTimerStart;
    public int deaths;
    private bool overrideKillLine;

    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isRespawning = false;
        currentHealth = maximumHealth;
        lastTick = Time.time;
        overrideKillLine = false;

        foreach (AudioSource voiceLine in punchKillVoiceLines)
        {
            voiceLine.volume = 0.5f;
            voiceLine.playOnAwake = false;
        }

        deaths = 0;
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

    public void damage(float damage, int type) {
        //types:
        //punch = 0
        //ball = 1
        //gun = 2
        //env = 3
        Opposite opp = GameObject.Find("GameManager").GetComponent<Opposite>();
        if (opp.isOppositeDay)
        {
            GameObject target = (this.GetComponent<move_1>().playerOne) ? opp.getP2() : opp.getP1();
            target.GetComponent<health>().direct(damage, type);
        } else
        {
            direct(damage, type);
        }
    }

    public void direct(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth = (currentHealth - damage > 0) ? currentHealth - damage : 0;
        if (currentHealth == 0)
        {
            kill();
        }
    }

    public void direct(float damage, int type)
    {
        //types:
        //0 = punch
        //1 = ball
        //2 = gun
        //3 = env
        if (isDead)
        {
            return;
        }

        if (type > 0) {
            overrideKillLine = true;
        }
        if (type == 1) {
            if (isPlayer1) {
                damageLineManager.GetComponent<DamageLineManager>().playBlueBall();
            }
            else {
                damageLineManager.GetComponent<DamageLineManager>().playRedBall();
            }
        }
        else if (type == 2) {
            if (isPlayer1) {
                damageLineManager.GetComponent<DamageLineManager>().playBlueGun();
            }
            else {
                damageLineManager.GetComponent<DamageLineManager>().playRedGun();
            }
        }

        currentHealth = (currentHealth - damage > 0) ? currentHealth - damage : 0;
        if (currentHealth == 0)
        {   
            if (isPlayer1 && (type == 3)) {
                damageLineManager.GetComponent<DamageLineManager>().playBlueEnv();
            }
            else if (type == 3) {
                damageLineManager.GetComponent<DamageLineManager>().playRedEnv();
            }
            kill();
        }
        overrideKillLine = false;
    }


    public void heal(float health)
    {
        currentHealth = (currentHealth + health < maximumHealth) ? currentHealth + health : maximumHealth;
    }


    public void kill()
    {
        body.interpolation = RigidbodyInterpolation.None;
        trans.position = new Vector3(0f, -50f, 0f);
        Opposite opp = GameObject.Find("GameManager").GetComponent<Opposite>();
        if (opp.isOppositeDay)
        {
            deaths += 2;
        }
        opp.NormalTime();

        respawnTimerStart = Time.time;
        isDead = true;

        GameObject enemy = (this.GetComponent<move_1>().playerOne) ? opp.getP2() : opp.getP1();
        AudioSource[] killLines = enemy.GetComponent<health>().punchKillVoiceLines;
        int randomVO = Random.Range(0, killLines.Length - 1);
        if (overrideKillLine == false) {
            killLines[randomVO].Play();
        }

        Burning burn = gameObject.GetComponent<Burning>();
        if (burn != null)
        {
            Destroy(burn);
        }

        gameObject.GetComponent<booze_binging>().cleanseBeer();

        deaths += 1;
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


        //Respawn
        if (isDead && Time.time >= respawnTimerStart + respawnDelay)
        {
            isDead = false;
            isRespawning = true;

            Opposite opp = GameObject.Find("GameManager").GetComponent<Opposite>();
            GameObject enemy = (this.GetComponent<move_1>().playerOne) ? opp.getP2() : opp.getP1();
            int nextSpawnIndex = 0;
            if (enemy.transform.position.x < 0)
            {
                nextSpawnIndex = Random.Range(0, 2);
                gameObject.GetComponent<move_1>().facingLeft = true;
            } else
            {
                nextSpawnIndex = Random.Range(2, 4);
                gameObject.GetComponent<move_1>().facingLeft = false;
            }
            trans.position = spawnPoints[nextSpawnIndex];
            currentHealth = maximumHealth;
        }
        if (isRespawning && Time.time >= respawnTimerStart + respawnDelay + 0.2f)
        {
            isRespawning = false;
            body.interpolation = RigidbodyInterpolation.Extrapolate;
        }

    }
}
