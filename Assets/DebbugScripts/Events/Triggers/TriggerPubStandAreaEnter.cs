using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPubStandAreaEnter : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameEvents.current.PubStandTriggerEnter();
        }
    }


    // Inserte a nested event for the case your dialogue with the barman is concluded
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.current.PubStandTriggerExit();
        }
    }
}
