using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booze_binging : MonoBehaviour
{   
    [SerializeField] private int beerInv;
    [SerializeField] private int beerLevel;
    [SerializeField] private bool player1;
    private KeyCode downKey;
    private KeyCode itemKey;
    private int itemLayer;
    [SerializeField] private bool shotgunEquipped;
    [SerializeField] private bool ballEquipped;
    [SerializeField] private float gunForce;
    [SerializeField] private float ballForce;
    [SerializeField] private float frontOffset;

    [SerializeField] private GameObject projectile;
    private bool left;

    // Start is called before the first frame update
    void Start()
    {
        beerInv = 0;
        beerLevel = 0;
        if (player1) {
            downKey = KeyCode.S;
            itemKey = KeyCode.V;
        }
        else {
            downKey = KeyCode.K;
            itemKey = KeyCode.Slash;
        }
        itemLayer = LayerMask.NameToLayer("item");
        shotgunEquipped = false;
        ballEquipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        left = this.gameObject.GetComponent<move_1>().facingLeft;
        if (Input.GetKeyDown(downKey)) {
            this.drinkBeer();
        }
        if (Input.GetKeyDown(itemKey)) {
            if (shotgunEquipped) {
                shotgunEquipped = false;
                this.fireFun();
            }
            else if (ballEquipped) {
                ballEquipped = false;
                this.throwBowlingBall();
            }
        }
    }

    void launchProjectile(Vector3 dir, float speed, float size, float damage, float duration) {
        //rock radius, force, offset are 0
        Vector3 launchPos = this.transform.position;
        if (left) {
            launchPos.x -= frontOffset;
        }
        else {
            launchPos.x += frontOffset;
        }
        dir.y += 0.05f;
        dir = dir.normalized;
        dir = dir * speed;
        Vector3 sizeScale = new Vector3(size, size, size);
        GameObject proj = Instantiate(projectile, launchPos, Quaternion.identity);
        proj.transform.localScale = sizeScale;
        proj.GetComponent<Rocked>().duration = duration;
        proj.GetComponent<Rocked>().damage = damage;
        proj.GetComponent<Rocked>().force = 0;
        proj.GetComponent<Rocked>().radius = 0;
        proj.GetComponent<Rocked>().offset = 0;
        proj.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
    }

    void fireFun() {
        Vector3 launchDir;
        if (left) {
            launchDir = (this.gameObject.transform.right * -1f);
        }
        else {
            launchDir = this.gameObject.transform.right;
        }
        Vector3 pointDir = launchDir;
        pointDir.y += 0.63f;
        for (int i = 0; i < 7; i += 1) {
            Debug.Log("hi");
            launchProjectile(pointDir, gunForce, 0.3f, 15f, 0.4f);
            pointDir.y -= 0.21f;
        }
    }

    void throwBowlingBall() {
        Vector3 launchDir;
        if (left) {
            launchDir = (this.gameObject.transform.right * -1f);
        }
        else {
            launchDir = this.gameObject.transform.right;
        }
        launchProjectile(launchDir, ballForce, 1f, 50f, 5f);
    }

    public void addBeer() {
        beerInv += 1;
    }

    void drinkBeer() {
        if (beerInv > 0) {
            beerInv -= 1;
            beerLevel += 1;
            Debug.Log("glug glug");
        }
        else {
            Debug.Log("no more drinksies");
        }
        this.gameObject.GetComponent<PunchAttack>().setDrunkDmg(beerLevel);
        this.gameObject.GetComponent<PunchAttack>().setDrunkDmg(beerLevel);
    }

    public void cleanseBeer() {
        beerLevel = 0;
        Debug.Log("sober");
        this.gameObject.GetComponent<PunchAttack>().setDrunkDmg(beerLevel);
        this.gameObject.GetComponent<health>().setDrunkLevel(beerLevel);
    }

    public int getBeerInv() {
        return beerInv;
    }

    public int getDrunkLevel() {
        return beerLevel;
    }

    void eatChicken() {
        Debug.Log("nom nom");
        this.gameObject.GetComponent<health>().fullHealth();
    }

    void OnCollisionEnter(Collision col) {
        GameObject item = col.gameObject;
        if (item.layer == itemLayer) {
            int id = item.GetComponent<item_info>().getID();
            Debug.Log("id " + id);
            if (id == 0) {
                this.addBeer();
            }
            else if (id == 1) {
                this.eatChicken();
            }
            else if ((id == 2) && (shotgunEquipped == false)) {
                //show shotgun on model
                //replace ball if equipped
                shotgunEquipped = true;
                ballEquipped = false;

            }
            else if ((id == 3) && (ballEquipped == false)) {
                //show ball on model
                //replace shotgun if equipped
                ballEquipped = true;
                shotgunEquipped = false;
            }
            Destroy(item);
        }
    }
}
