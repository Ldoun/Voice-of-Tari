using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public float speed = 0.54f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed);
        if (transform.position.x < -340)
        {
            Destroy(gameObject);
        }
        if (gameObject.tag == "item1")
        {

        }
    }
}
