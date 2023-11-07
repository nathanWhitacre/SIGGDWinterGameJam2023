using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Match : MonoBehaviour
{

    [SerializeField] private float matchLength;
    [SerializeField] private GameObject timer;

    private float match_start_time;

    [SerializeField] private AudioSource tenSec;
    [SerializeField] private AudioSource blueAhead;
    [SerializeField] private AudioSource[] opening;
    [SerializeField] private AudioSource oneMinute;
    [SerializeField] private AudioSource redAhead;
    [SerializeField] private AudioSource[] opposite;


    // Start is called before the first frame update
    void Start()
    {
        match_start_time = Time.time;
        this.playMatchStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        float current = Time.time;
        int timeSinceStart = (int) (current - match_start_time);
        timer.GetComponent<TextMeshProUGUI>().text = "" + (matchLength - timeSinceStart);
        if ((matchLength - timeSinceStart) == 60) {
            this.playOneMinute();
        }
        if ((matchLength - timeSinceStart) == 10) {
            this.playTenSeconds();
        }

    }

    public void playMatchStart() {
        int rand = Random.Range(0, 2);
        opening[rand].Play();
    }

    public void playOneMinute() {
        oneMinute.Play();
    }

    public void playTenSeconds() {
        tenSec.Play();
    }

    public void playBlueAhead() {
        blueAhead.Play();
    }

    public void playRedAhead() {
        redAhead.Play();
    }

    public void playOppositeDay() {
        int rand = Random.Range(0, 6);
        opposite[rand].Play();
    }
}
