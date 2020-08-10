using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    void Update()
    {
    }

    public void DestroyOwnHat() {
        Transform hat = GameObject.Find("Hat_NPC").GetComponent<Transform>();
        if (hat != null) {
            Destroy(hat.gameObject);
        } else {
            Debug.LogError("Hat GameObject is NULL.");
        }
    }

    
}
