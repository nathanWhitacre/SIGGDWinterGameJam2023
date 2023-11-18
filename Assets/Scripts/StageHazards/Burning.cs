using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    private float delay;
    private float damage;
    private float destructDuration;

    private float last_burn;
    private float destructTime;
    //private GameObject SFXManager;
    private SoundEffectsManager sfxManager;
    private AudioSource fireSFX;

    // Start is called before the first frame update
    void Start()
    {
        last_burn = Time.time;
        destructTime = -1;
        sfxManager = FindObjectOfType<SoundEffectsManager>();
        fireSFX = sfxManager.fireLoop;
    }

    private void Update()
    {
        if (destructTime > 0)
        {
            if (Time.time > destructTime)
            {
                //if (fireSFX.isPlaying)
                //{
                    fireSFX.Stop();
                    Debug.Log("STOP BEING ON FIRE");
                //}
                Destroy(this);
            }
        }
        float current = Time.time;
        float difference = current - last_burn;

        if (difference >= delay)
        {
            this.GetComponent<health>().damage(damage, 3);
            last_burn = current;
            Debug.Log("BURN BABY BURN!");
            if (!fireSFX.isPlaying && !this.GetComponent<health>().isDead)
            {
                fireSFX.Play();
            }
        }
    }

    public void SelfDestruct()
    {
        destructTime = Time.time + 5;
    }

    public void BurnReset()
    {
        destructTime = -1;
    }


    // Getters & Setters

    public void SetDelay(float delay)
    {
        this.delay = delay;
    }

    public void SetDamage(float damange)
    {
        this.damage = damange;
    }

    public void SetDuration(float duration)
    {
        destructDuration = duration;
    }

    public void stopFireSFX()
    {
        //if (fireSFX.isPlaying)
        //{
            fireSFX.Stop();
            Debug.Log("STOP BEING ON FIRE");
        //}
    }
}
