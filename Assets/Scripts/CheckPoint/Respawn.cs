using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    private CheckPointManager manager;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("RespawnManager").GetComponent<CheckPointManager>();
      
    }
    private void OnTriggerEnter(Collider other)
    {
        //when the player enters the trigger
        if (other.tag == "Player")
        {
            //teleport the player to the respawnPoint;
            player.transform.position = manager.lastCheckPointPos.position;
            Debug.Log(manager.lastCheckPointPos.position);
            Debug.Log("Respawning");
        }


    }

}
