using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{

    [SerializeField] private int maximumHealth = 100;
    private float currentHealth = 1;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }


    public void damage(float damage)
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
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
