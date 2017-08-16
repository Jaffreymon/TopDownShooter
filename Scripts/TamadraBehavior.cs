using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamadraBehavior : MonoBehaviour {
    [SerializeField]
    private const float healingAmount = 25f;
    [SerializeField]
    private const float expAmount = 9f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player currentPlayer = other.GetComponent<Player>();
            currentPlayer.addHealth(healingAmount);
            currentPlayer.AddExperience(expAmount);
            Destroy(this.gameObject);
        }
    }
}
