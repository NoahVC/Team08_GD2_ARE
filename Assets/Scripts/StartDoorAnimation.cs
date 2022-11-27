using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] Vines Activate = null;
    private bool startActivate = false;
    private void Update()
    {
        if (startActivate)
        {
            Invoke("StartVineActivate", 3);
            startActivate = false;
        }
           
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paintball"))
        {
            animator.SetBool("OnTrigger", true);
            startActivate=true;
        }
    }
    private void StartVineActivate()
    {
        Activate.isActivated = true;
    }
}
