using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;

    [SerializeField] GameObject p1HealthTracker;
    [SerializeField] GameObject p2HealthTracker;



    // Update is called once per frame
    void Update()
    {
        health p1Health = p1.GetComponent<health>();
        health p2Health = p2.GetComponent<health>();

        TextMeshProUGUI p1HealthField = p1HealthTracker.GetComponent<TextMeshProUGUI>();
        p1HealthField.SetText("" + p1Health.getCurrentHealth());

        TextMeshProUGUI p2HealthField = p2HealthTracker.GetComponent<TextMeshProUGUI>();
        p2HealthField.SetText("" + p2Health.getCurrentHealth());
    }
}
