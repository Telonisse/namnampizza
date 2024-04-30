using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    private HingeJoint joint;
    [SerializeField] bool open;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }
    public void OpenFridge()
    {
        var motor = joint.motor;
        if (!open)
        {
            motor.targetVelocity = -200;
            joint.motor = motor;
        }
        if (open)
        {
            motor.targetVelocity = 200;
            joint.motor = motor;
        }
    }

    public void CloseFridge()
    {
        if (open)
        {
            open = false;
        }
        else
        {
            open = true;
        }
    }
}
