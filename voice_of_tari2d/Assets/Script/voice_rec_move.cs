using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
public class voice_rec_move : MonoBehaviour
{
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public bool flapped;
    public float speed;
    private Rigidbody2D rb;
    public Text gameboard;
    public Image Background;
    // Start is called before the first frame update
    /*enum GravityDirection { Down, Left, Up, Right };
    GravityDirection m_GravityDirection;*/

    private void Awake()
    {
        gameboard.text = "";
        Background.enabled = false;
        //init microphone input
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            microphoneInitialized = true;
            Debug.Log("mic active");
        }
        else
        {
            Debug.Log("mic not active");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //m_GravityDirection = GravityDirection.Down;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -240)
        {
            rb.velocity = new Vector2(1.0f * (speed / 5), 0);
            Debug.Log("Boom!!!!");
            Background.enabled = true;
            gameboard.text = "GAME OVER";
            Destroy(gameObject);
        }
        //get mic volume
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
        microphoneInput.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));
        Debug.Log(level);
        if (level > sensitivity)
        {
            Flap();
            flapped = true;
        }
        else
        {
            Down();
        }
        
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < 0f)
            pos.y = 0f;
        if (pos.y > 1f)
            pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        if (level < sensitivity && flapped)
        {
            flapped = false;
        }
    }
    void Flap()
    {
        Debug.Log("flapped");
        if (transform.position.x < -148.5)
        {
            rb.velocity = new Vector2(1.0f * (speed/10), 1.0f * speed);
        }
        rb.velocity = new Vector2(0.0f, 1.0f*speed);
        //transform.Translate(Vector3.up * speed);

        //using AddForce
        //rb.AddForce(transform.up * speed);

        //gravity controll
        //Physics2D.gravity = new Vector2(0, 5000f);

    }
    private void Down() {
        if (transform.position.x < -148.5)
        {
            rb.velocity = new Vector2(1.0f * (speed / 10), 1.0f * speed);
        }
        rb.velocity = new Vector2(0.0f, -1.0f*speed);
        //transform.Translate(Vector3.down * speed);
        //gravity controll
        //Physics2D.gravity = new Vector2(0, -3000f);
    }
    void in_cam_check(int mode)
    {
        int in_cam = 0;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < 0f)
            in_cam = 1;
            pos.y = 0f;
        if (pos.y > 1f)
            in_cam = 1;
        if (in_cam == 0){
            if (mode == 0)
            {
                transform.Translate(Vector3.up * speed);
            }
            if (mode == 1)
            {
                transform.Translate(Vector3.down * speed);
            }
        }

    }
    /*void FixedUpdate()
    {
        switch (m_GravityDirection)
        {
            case GravityDirection.Down:
                //Change the gravity to be in a downward direction (default)
                Physics2D.gravity = new Vector2(0, -9.8f);
                //Press the space key to switch to the left direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Left;
                    Debug.Log("Left");
                }
                break;

            case GravityDirection.Left:
                //Change the gravity to go to the left
                Physics2D.gravity = new Vector2(-9.8f, 0);
                //Press the space key to change the direction of gravity
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Up;
                    Debug.Log("Up");
                }
                break;

            case GravityDirection.Up:
                //Change the gravity to be in a upward direction
                Physics2D.gravity = new Vector2(0, 9.8f);
                //Press the space key to change the direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Right;
                    Debug.Log("Right");
                }
                break;

            case GravityDirection.Right:
                //Change the gravity to go in the right direction
                Physics2D.gravity = new Vector2(9.8f, 0);
                //Press the space key to change the direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Down;
                    Debug.Log("Down");
                }

                break;
        }
    }*/
}
