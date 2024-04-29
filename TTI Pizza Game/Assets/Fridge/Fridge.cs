using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    private HingeJoint joint;
    [SerializeField] bool open = false;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }
    public void OpenFridge()
    {
        var motor = joint.motor;
        if (!open)
        {
            joint.useMotor = true;
            open = true;
        }
        if (open)
        {
            motor.targetVelocity = 200;
            open = false;
        }
    }
}
