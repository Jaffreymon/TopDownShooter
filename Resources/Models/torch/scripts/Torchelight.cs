using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;

	public GameObject MainFlame;
    ParticleSystem.EmissionModule mainFlameEmission;

    public GameObject BaseFlame;
    ParticleSystem.EmissionModule baseFlameEmission;

    public GameObject Etincelles;
    ParticleSystem.EmissionModule entincellesEmission;

    public GameObject Fumee;
    ParticleSystem.EmissionModule fumeeEmission;
    public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		TorchLight.GetComponent<Light>().intensity=IntensityLight;

        mainFlameEmission = MainFlame.GetComponent<ParticleSystem>().emission;
        mainFlameEmission.rateOverTime = IntensityLight * 20f;

        baseFlameEmission = MainFlame.GetComponent<ParticleSystem>().emission;
        baseFlameEmission.rateOverTime = IntensityLight * 15f;

        entincellesEmission = MainFlame.GetComponent<ParticleSystem>().emission;
        entincellesEmission.rateOverTime = IntensityLight * 7f;

        fumeeEmission = MainFlame.GetComponent<ParticleSystem>().emission;
        fumeeEmission.rateOverTime = IntensityLight * 12f;
	}
	

	void Update () {
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		TorchLight.GetComponent<Light>().intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));

		TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/1.5f,1f),Mathf.Min(IntensityLight/2f,1f),0f);

        mainFlameEmission.rateOverTime = IntensityLight * 20f;
        baseFlameEmission.rateOverTime = IntensityLight * 15f;
        entincellesEmission.rateOverTime = IntensityLight * 7f;
        fumeeEmission.rateOverTime = IntensityLight * 12f;

    }
}
