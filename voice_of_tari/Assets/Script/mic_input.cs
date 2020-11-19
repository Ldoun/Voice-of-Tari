using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mic_input : MonoBehaviour
{
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public bool flapped;
    public float speed;
    private Rigidbody rb;

    private void Awake()
    {
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
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        /*if (level < sensitivity && flapped)
        {
            flapped = false;
        }*/
            

    }
    void Flap()
    {
        Debug.Log("flapped");
        transform.Translate(Vector3.up * speed );   
    }
}
