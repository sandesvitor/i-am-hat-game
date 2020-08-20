using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_Player : MonoBehaviour
{
    private Light _probeLight;
    private bool _switchLight = true;

    void Start()
    {
        _probeLight = GetComponentInChildren<Light>();

        SetPosition();
        StartCoroutine(LigthFlicker());
    }

    void Update()
    {
        
    }

    void SetPosition() {
        this.transform.position = this.transform.parent.position
            + new Vector3(0, this.transform.parent.localScale.y - 0.10f, 0);

        this.transform.rotation = this.transform.parent.rotation;
    }

    public void HatTransfer(Transform newParent) {
        this.transform.SetParent(newParent);
        SetPosition();
    }

    private IEnumerator LigthFlicker() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            _probeLight.enabled = _switchLight;
            _switchLight = !_switchLight;
        }
    }
}
