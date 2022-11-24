using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSecondaryLadder : MonoBehaviour
{
    private bool isPushed = false;
    public Transform ladderToDrop = null;

    private Vector3 ladderToDropPosition;
    private float maxYDrop = 8f;
    public void Start()
    {
        ladderToDropPosition = ladderToDrop.transform.position;
        ladderToDropPosition.y -= maxYDrop;
    }

    public void Update()
    {
        if (isPushed)
        {
            ladderToDrop.position = ladderToDropPosition;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paintball")
        {
            isPushed = true;
        }
    }
}
