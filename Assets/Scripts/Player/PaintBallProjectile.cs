using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) //TO DO: Add painting application logic (most likely here) and interaction with world depending on the tags
    {
        if(collision.gameObject.tag != "Paintball" && collision.gameObject.tag != "Player") //Paint ball disappears when hitting anything but another paint ball or the player.
        {
            Destroy(gameObject);
        }
    }
}
