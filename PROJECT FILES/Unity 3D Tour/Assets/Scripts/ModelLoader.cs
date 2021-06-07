using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ModelLoader : MonoBehaviour
{
    public ContentController contentController;
    GameObject mainScriptHolder;
    Main mainScript;

    private void Start()
    {
        mainScriptHolder = GameObject.Find("MainScriptHolder");
        mainScript = mainScriptHolder.GetComponent<Main>();
    }

    public void DownloadFile(string defaultUrl)
    {
        string modelUrl = "";

        if (mainScript.modelUrl != "")
        {
            modelUrl = mainScript.modelUrl;
        }
        else
        {
            modelUrl = "defaultUrl";
        }

        //modelUrl = "https://gitlab.com/reinhardjonathansilalahi/3dmodel/-/raw/master/3dmodel";
        contentController.LoadContent(modelUrl);
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.3dtour.player.UnityPlayerActivity");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}