using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField]
    private double cycleMins;
    private double cycleCalc;
    [SerializeField]
    private float sunDimRate;
    private int dayCount;
    private bool currDay;

    private const int circleRotation = 360;
    private const float earthRotationY = 23f;
    private float earthRotationZ = 90f;

    [SerializeField]
    private Light sun;

    // Use this for initialization
    void OnEnable () {
        // Default position as Earth sunrise
        transform.rotation = Quaternion.Euler(0, earthRotationY, earthRotationZ);

        cycleCalc = 0.1/cycleMins * 1;
        sun.intensity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        // Light rotations
        transform.Rotate(0, 0, ((earthRotationZ * (float) cycleCalc) % circleRotation)* Time.timeScale, Space.World);

        // Day Phase start 80 deg starts sunrise
        if (220f >=transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z >= 80f)
        {
            sun.intensity = Mathf.Clamp01(sun.intensity + sunDimRate);
        }
        // Night Phase start 240 deg starts sunset
        else
        {
            sun.intensity = Mathf.Clamp01(sun.intensity - sunDimRate);
        }
        
    }



}
