using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_Player : MonoBehaviour
{

    void Start()
    {
        SetPosition();   
    }

    void Update()
    {
        
    }

    void SetPosition() {
        this.transform.position = this.transform.parent.position
            + new Vector3(0, this.transform.parent.localScale.y - 0.10f, 0);
    }

    public void HatTransfer(Transform newParent) {
        this.transform.parent = null;
        this.transform.parent = newParent;
        SetPosition();
    }
}
