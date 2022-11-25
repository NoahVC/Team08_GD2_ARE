using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : ValveActivatable
{
    private float _rotation;
    private float _initialRotation;


    public override void Activate(float rotation)
    {
        _rotation = rotation;
    }

    

    private void Start()
    {
        _initialRotation = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        float change = Mathf.Clamp((_initialRotation + _rotation),275,360);

        transform.rotation = Quaternion.Euler(temp.x, temp.y, change);

        //Debug.Log(transform.rotation.eulerAngles.z);
    }
}
