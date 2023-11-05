using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booze_binging : MonoBehaviour
{   
    [SerializeField] private int beerInv;
    [SerializeField] private int beerLevel;
    [SerializeField] private bool player1;
    private KeyCode downKey;
    private int itemLayer;

    // Start is called before the first frame update
    void Start()
    {
        beerInv = 0;
        beerLevel = 0;
        if (player1) {
            downKey = KeyCode.S;
        }
        else {
            downKey = KeyCode.K;
        }
        itemLayer = LayerMask.NameToLayer("item");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(downKey)) {
            this.drinkBeer();
        }
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
            if (id == 0) {
                this.addBeer();
            }
            else if (id == 1) {
                this.eatChicken();
            }
            Destroy(item);
        }
    }
}
