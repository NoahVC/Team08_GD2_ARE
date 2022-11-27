using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    private Rigidbody rb;
    private bool _isActivated;
    private float totalTime = 3f;
    private float currentTime = 0;
    public float floatForce = 5f;
    public GameObject canvas;
    public HUD hud;
    public bool isActivated
    {
        get { return _isActivated; }
        set { _isActivated = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            canvas.SetActive(true);
            rb.velocity = Vector3.up * floatForce;
            if (currentTime <= totalTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                canvas.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paintball"))
        {
            isActivated = true;
            hud.currentBalloons += 1;
        }
    }
}
