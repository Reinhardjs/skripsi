using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    private bool hasExtra = false;
    public string modelUrl = "";
    public AndroidJavaObject extras;
    public AndroidJavaObject intent;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        hasExtra = intent.Call<bool>("hasExtra", "modelUrl");
        Debug.Log("start");

        if (hasExtra)
        {
            Debug.Log("has extra");
            extras = intent.Call<AndroidJavaObject>("getExtras");
            modelUrl = extras.Call<string>("getString", "modelUrl");
            Debug.Log("Model URL : " + modelUrl);
        }
        else
        {
            Debug.Log("no extra");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
