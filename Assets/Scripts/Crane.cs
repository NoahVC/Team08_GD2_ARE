using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : ValveActivatable
{
    private Transform _joint;
    private float _rotation;
    private float _initialRotation;

    private void Start()
    {
        _initialRotation = transform.rotation.eulerAngles.z;
        _joint = transform.GetChild(0);
    }

    void Update()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        float change =Mathf.Clamp((_initialRotation + _rotation), -180, 90);
        _joint.rotation = Quaternion.Euler(temp.x, change, temp.z);

    }

    public override void Activate(float rotation)
    {
        _rotation = rotation;
    }
}
