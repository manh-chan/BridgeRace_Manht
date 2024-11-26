using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Player : Character
{
    [SerializeField] private float speed = 5;

    private void Start()
    {
        ChangeColor(colorType);
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    void Update()
    {
      
            if (Input.GetMouseButton(0))
            {

            Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + TF.position;
            if (CanMove(nextPoint) && CheckGate(nextPoint))
            {
                TF.position = CheckGround(nextPoint);
            }
            if (JoystickControl.direct != Vector3.zero)
                {
                    charRotate.forward = JoystickControl.direct;
                }

                ChangeAnim(Constant.ANIM_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }

        
    }
}
