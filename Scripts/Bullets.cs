using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {

    private float lifeTime = 4f;
    private float deathTime;
    private bool fading;

    private Material mat;
    private Color originalCol;
    private float fadePercent;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        originalCol = mat.color;
        deathTime = Time.time + lifeTime;
        StartCoroutine("Fade");
	}

    // Bullet fades after time
    IEnumerator Fade()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            if (fading)
            {
                // Bullet fades over time
                fadePercent += Time.deltaTime;
                mat.color = Color.Lerp(originalCol, Color.clear, fadePercent);
                if (fadePercent >= 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (Time.time > deathTime)
                {
                    fading = true;
                }
            }
        }
    }

    // Prevents any bullet movement while in contact with ground
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            GetComponent<Rigidbody>().Sleep();
        }
    }
}
