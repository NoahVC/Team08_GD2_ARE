using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paintball"))
        {
            animator.SetBool("OnTrigger",true);
        }
    }
}
