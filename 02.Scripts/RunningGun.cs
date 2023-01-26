using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGun : CustomUpdateBehaviour
{
    public bool isRunning { get; private set; } = false;

    [SerializeField] Animation _combatAnimation;
    [SerializeField] FireCtrl _fireCtrl;

    public override void CustomUpdate()
    {
        if (_fireCtrl.isReloading) return;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            _combatAnimation.Play("running");
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _combatAnimation.Play("runStop");
            isRunning = false;
        }
    }
}
