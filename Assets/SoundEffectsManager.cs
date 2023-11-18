using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{

    [SerializeField] private AudioSource[] smacks;

    [SerializeField] private AudioSource[] steps;

    [SerializeField] public AudioSource beerDrink;
    [SerializeField] public AudioSource pool;
    [SerializeField] public AudioSource fireLoop;
    [SerializeField] public AudioSource fireCleansed;
    [SerializeField] public AudioSource getFood;
    [SerializeField] public AudioSource getItem;
    [SerializeField] public AudioSource oppositeDay;
    [SerializeField] public AudioSource poisonCleansed;
    [SerializeField] public AudioSource trampolineBounce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playSmack()
    {
        //TODO: make this mofo work
        int rand = Random.Range(0, 5);
        smacks[rand].Play();
    }

    public void playStep()
    {
        //TODO make this mofo work
        int rand = Random.Range(0, 3);
        steps[rand].Play();
    }
}
