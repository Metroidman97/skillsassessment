using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    // Declare variables
    public GameObject nameText;
    public GameObject objectivesText;

    // Start is called before the first frame update
    void Start()
    {
        // Set the text to inactive at the start
        nameText.SetActive(false);
        objectivesText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If name text is off when P is pressed, turn on. If it's on, turn it off.
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!nameText.activeSelf)
                nameText.SetActive(true);
            else if (nameText.activeSelf)
                nameText.SetActive(false);
        }

        // If the objectives text is off when Q is pressed, turn on. If it's on, turn it off.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!objectivesText.activeSelf)
                objectivesText.SetActive(true);
            else if (objectivesText.activeSelf)
                objectivesText.SetActive(false);
        }
    }
}
