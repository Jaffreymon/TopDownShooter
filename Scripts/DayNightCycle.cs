using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    [SerializeField]
    private double cycleMins;
    private double cycleCalc;
    [SerializeField]
    private float sunDimRate;
    private int dayCount = 0;
    private bool currDay = false;

    private const int circleRotation = 360;
    private const float earthRotationY = 23f;
    private float earthRotationZ = 90f;

    [SerializeField]
    private Light sun;
    [SerializeField]
    private GUI_HUD gui;

    // Use this for initialization
    void OnEnable () {
        // Default position before Earth sunrise
        transform.rotation = Quaternion.Euler(0, earthRotationY, earthRotationZ);

        cycleCalc = 0.1/cycleMins * 1;
        sun.intensity = 0f;
        updateDayCount();
    }
	
	// Update is called once per frame
	void Update () {
        // Light rotations; preserves degrees within [-90, 270] due to sun's tilt
        transform.Rotate(0, 0, ((earthRotationZ * (float) cycleCalc) % circleRotation) * Time.timeScale, Space.World);

        // Day Phase start 80 deg starts sunrise
        if (220f >=transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z >= 80f)
        {
            if(currDay)
            {
                currDay = false;
                updateDayCount();
            }

            sun.intensity = Mathf.Clamp01(sun.intensity + sunDimRate);
        }
        // Night Phase start 220 deg starts sunset
        else
        {
            currDay = true;
            sun.intensity = Mathf.Clamp01(sun.intensity - sunDimRate);
        }
    }

    private void updateDayCount()
    {
        gui.SetDayCount(++dayCount);
    }


}
