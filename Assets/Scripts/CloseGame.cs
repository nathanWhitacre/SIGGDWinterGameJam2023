using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Application.Quit();
        }
    }
}
