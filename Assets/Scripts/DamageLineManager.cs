using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLineManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] redGun;
    [SerializeField] private AudioSource[] blueGun;
    [SerializeField] private AudioSource[] redBall;
    [SerializeField] private AudioSource[] blueBall;
    [SerializeField] private AudioSource[] redEnv;
    [SerializeField] private AudioSource[] blueEnv;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playRedBall() {
        int rand = Random.Range(0, 5);
        redBall[rand].Play();
    }

    public void playBlueBall() {
        int rand = Random.Range(0, 4);
        blueBall[rand].Play();
    }

    public void playBlueGun() {
        int rand = Random.Range(0, 4);
        blueGun[rand].Play();
    }

    public void playRedGun() {
        int rand = Random.Range(0, 3);
        redGun[rand].Play();
    }

    public void playBlueEnv() {
        int rand = Random.Range(0, 4);
        blueEnv[rand].Play();
    }

    public void playRedEnv() {
        int rand = Random.Range(0, 4);
        redEnv[rand].Play();
    }
}
