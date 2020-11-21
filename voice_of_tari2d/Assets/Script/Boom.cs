using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public Splatter splatter;
    public float gravity_speed = 5f;
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
        if (other.gameObject.tag == "item1")
        {
            
            Rigidbody2D other_rb =other.gameObject.GetComponent<Rigidbody2D>();
            Vector3 offset=transform.position-other.gameObject.transform.position;
            offset.z = 0;
            float magsqr = offset.sqrMagnitude;
            if (magsqr > 0.0001f)
            {
                Debug.Log("item1");
                //Create the gravity- make it realistic through division by the "magsqr" variable
                other_rb.AddForce((gravity_speed*100 * offset.normalized / magsqr) * other_rb.mass);
                //other_rb.AddForce(transform.up * 20);
            }
        }
    }

}
