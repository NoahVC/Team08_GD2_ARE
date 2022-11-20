using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallShoot : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private GameObject _paintBall;
    [SerializeField]
    private Transform _pbStartLocation;
    
    private Vector3 _destination;
    private float _projectileSpeed = 10f;
    private float _timeToFire;
    private float _fireRate = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _timeToFire) //Paint ball on customizable timer for feel/gamesense
        {
            _timeToFire = Time.time + 1 / _fireRate;
            ShootPaint();
        }
    }

    private void ShootPaint()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) //IF hit collider, set destination for paint ball to that point
            _destination = hit.point; 
        else
            _destination = ray.GetPoint(1000); //Choose destination on ray

        InstantiateProjectile(_pbStartLocation); 
    }

    private void InstantiateProjectile(Transform firepoint) //creating paint ball with velocity '(_destination - _pbStartLocation.position).normalized' for correct rotation
    {
        var projectileObj = Instantiate(_paintBall, _pbStartLocation.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (_destination - _pbStartLocation.position).normalized * _projectileSpeed;
    }
}
