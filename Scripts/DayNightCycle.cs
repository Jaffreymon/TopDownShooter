using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    private double cycleMins;
    private double cycleCalc;
    private float sunDimRate;
    private int dayCount;
    private bool currDay;

    [SerializeField]
    private Light sun;

    // Use this for initialization
    void Start () {
        cycleMins = 7f;
        sunDimRate = 0.1f;
        cycleCalc = 0.1/cycleMins * -1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, (float) -cycleCalc, Space.World);

        // Day
        if (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= 40) 
        {
            sun.intensity = Mathf.Clamp(sun.intensity + (sunDimRate * Time.deltaTime), 0, 1);
        }
        // Night
        else if (160 <= transform.eulerAngles.z && transform.eulerAngles.z <= 220)
        {
            sun.intensity -= sunDimRate * Time.deltaTime;   
        }
    }
}
