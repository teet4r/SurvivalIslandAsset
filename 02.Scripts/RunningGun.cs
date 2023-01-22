using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGun : MonoBehaviour
{
    public Animation combatAni;
    public bool isRunning = false;

    FireCtrl fireCtrl;

    // Start is called before the first frame update
    void Start()
    {
        fireCtrl = GetComponent<FireCtrl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fireCtrl.isReloading) return;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            combatAni.Play("running");
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            combatAni.Play("runStop");
            isRunning = false;
        }
    }
}
