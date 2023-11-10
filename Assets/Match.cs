using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

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

    private GameObject p1;
    private GameObject p2;
    private bool hasPesteredRed;
    private bool hasPesteredBlue;


    // Start is called before the first frame update
    void Start()
    {
        match_start_time = Time.time;
        this.playMatchStart();

        p1 = gameObject.GetComponent<Opposite>().getP1();
        p2 = gameObject.GetComponent<Opposite>().getP2();
        hasPesteredRed = false;
        hasPesteredBlue = false;
    }

    // Update is called once per frame
    void Update()
    {
        float current = Time.time;
        int timeSinceStart = (int) (current - match_start_time);
        timer.GetComponent<TextMeshProUGUI>().text = "" + (matchLength - timeSinceStart);
        if ((matchLength - timeSinceStart) <= 60) {
            this.playOneMinute();
        }
        if ((matchLength - timeSinceStart) <= 10) {
            this.playTenSeconds();
        }

        if ((matchLength - timeSinceStart) <= 0)
        {
            endMatch();
        }

        if (!hasPesteredRed && p1.GetComponent<health>().deaths >= p2.GetComponent<health>().deaths + 5) {
            playBlueAhead();
            hasPesteredRed = true;
        }

        if (!hasPesteredBlue && p2.GetComponent<health>().deaths >= p1.GetComponent<health>().deaths + 5)
        {
            playRedAhead();
            hasPesteredBlue = true;
        }

    }


    public void endMatch()
    {
        if (p1.GetComponent<health>().deaths > p2.GetComponent<health>().deaths)
        {
            SceneManager.LoadScene(3);
        } else if (p1.GetComponent<health>().deaths > p2.GetComponent<health>().deaths)
        {
            SceneManager.LoadScene(4);
        } else
        {
            Application.Quit();
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
