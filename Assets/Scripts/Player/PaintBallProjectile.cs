using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallProjectile : MonoBehaviour
{
    [SerializeField]
    private SphereCollider _sphere;
    
    [SerializeField]
    private Texture2D _paintSplash;
    [SerializeField]
    private Color _paintColor;

    private int _textureWidth;
    private int _textureHeight;
    private float[,] _textureAlphas;
    private float _paintDiameter = 1.5f;

    private void Start()
    {
        _textureWidth = _paintSplash.width;
        _textureHeight = _paintSplash.height;
        _textureAlphas = new float[_textureWidth, _textureHeight];
    }

    private void OnCollisionEnter(Collision collision) //TO DO: Add painting application logic (most likely here) and interaction with world depending on the tags
    {
        if(collision.gameObject.tag != "Paintball" && collision.gameObject.tag != "Player") //Paint ball disappears when hitting anything but another paint ball or the player.
        {
            Collider[] hitColliders = Physics.OverlapSphere(collision.transform.position, _sphere.radius);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log($"Collidor hit {hitCollider.tag}");
            }

            Destroy(gameObject);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down/*GetRandomProjectileSplash()*/, out hit, _paintDiameter))
            {
                MyShaderBehavior Script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                if (Script != null)
                {
                    Script.PaintOn(hit.textureCoord, _textureAlphas, _paintColor);
                }
                Debug.Log(hit.textureCoord);
            }
        }
    }

    public Vector3 GetRandomProjectileSplash()
    {
        return Random.onUnitSphere;
    }

}
