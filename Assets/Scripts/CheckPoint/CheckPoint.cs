using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   private CheckPointManager m_Manager;
    private void Start()
    {
        m_Manager = GameObject.FindGameObjectWithTag("RespawnManager").GetComponent<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //teleport the player to the respawnPoint;
            m_Manager.lastCheckPointPos.position = transform.position;
            Debug.Log("Checkpoint reached.");
        }
    }
}
