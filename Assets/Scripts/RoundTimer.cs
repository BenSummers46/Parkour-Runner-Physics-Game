using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    // Creates a timer that will start at the beginning of each level.

    [SerializeField] int time;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI finishTime;
    
    // Starts the timer on level load
    void Start()
    {
        StartCoroutine(Time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Encrements the timer every second and sets the relevant UI text variables.
    IEnumerator Time()
    {
        yield return new WaitForSeconds(1f);

        time++;

        text.text = "" + time + "s";
        finishTime.text = "" + time + "s";

        StartCoroutine(Time());

    }

    // Stops the time co-routine
    public void StopTime()
    {
        StopCoroutine(Time());
    }
}
