using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Boom : MonoBehaviour
{
    public Splatter splatter;
    public float gravity_speed = 5f;
    public float spike_gravity_speed = 100000f;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public Text gameboard;
    public Image Background;

    // Start is called before the first frame update
    private void Awake()
    {
        gameboard.text = "";
        Background.enabled = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //m_GravityDirection = GravityDirection.Down;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.mute = false;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        gameboard.text = "";
        Background.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            Debug.Log("Play Sound");
            audioSource.Play();
            Debug.Log("Boom!!!!");
            Background.enabled = true;
            gameboard.text = "GAME OVER";
            Destroy(gameObject);
            Instantiate(splatter, transform.position, Quaternion.identity);
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
                other_rb.AddForce((gravity_speed * offset.normalized / magsqr) * other_rb.mass *5);
                //other_rb.AddForce(transform.up * 20);
            }
        }
        if (other.gameObject.tag == "obstacle_2")
        {
            Rigidbody2D other_rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector3 offset = transform.position - other.gameObject.transform.position;
            offset.z = 0;
            float magsqr = offset.sqrMagnitude;
            if (magsqr > 0.0001f)
            {
                Debug.Log("obstacle_2");
                //Create the gravity- make it realistic through division by the "magsqr" variable
                other_rb.AddForce((spike_gravity_speed * offset.normalized / magsqr) * other_rb.mass);
                //other_rb.AddForce(transform.up * 20);
            }
        }
        if (other.gameObject.tag == "item_big") {
            rb.velocity /= 2;
            Destroy(other.transform.parent.gameObject);
            Destroy(other.gameObject);
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log("go BIg!");
        }

        if (other.gameObject.tag == "item_small")
        {
            //rb.velocity /= 2;
            Destroy(other.transform.parent.gameObject);
            Destroy(other.gameObject);
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log("go small!");
        }
    }
}
