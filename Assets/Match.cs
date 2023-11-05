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

    // Start is called before the first frame update
    void Start()
    {
        match_start_time = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        float current = Time.time;
        int timeSinceStart = (int) (current - match_start_time);
        timer.GetComponent<TextMeshProUGUI>().text = "" + (matchLength - timeSinceStart);
    }
}
