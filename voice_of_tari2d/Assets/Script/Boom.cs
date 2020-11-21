using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public Splatter splatter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            Debug.Log("Boom");
            Instantiate(splatter, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
