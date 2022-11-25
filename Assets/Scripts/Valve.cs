using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField]
    private HingeJoint _joint;
    [SerializeField]
    private GameObject[] _activatableObjects;

    private float _precedentRotation = 0;
    private float _totalRotation=0;
    
    void Update()
    {
        _totalRotation += _joint.velocity * Time.deltaTime;
        if(!(_precedentRotation==_totalRotation))
        {
            _precedentRotation = _totalRotation;
            foreach (var item in _activatableObjects)
            {
                item.GetComponent<ValveActivatable>().Activate(_totalRotation);
            }
        }
    }
}

public interface IValveActivate
{
    public abstract void Activate(float rotation);
}

