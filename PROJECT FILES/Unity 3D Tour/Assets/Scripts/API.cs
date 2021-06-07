using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    public Text ProgressIndicator;
    public Image LoadingBar;
    
    public void GetBundleObject(string assetName, UnityAction<GameObject> callback, Transform bundleParent)
    {
        StartCoroutine(GetDisplayBundleRoutine(assetName, callback, bundleParent));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];
        return Path.Combine(Application.persistentDataPath, filename);
    }

    IEnumerator GetDisplayBundleRoutine(string bundleURL, UnityAction<GameObject> callback, Transform bundleParent)
    {
        Debug.Log("Requesting bundle at " + bundleURL);

        // request asset bundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        // www.downloadHandler = new DownloadHandlerFile(GetFilePath(bundleURL));
        yield return www.SendWebRequest();

        StartCoroutine(ShowDownloadProgress(www));

        if (www.isNetworkError)
        {
            Debug.Log("Network error");
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
                bundle.Unload(false);
                callback(arObject);
            }
            else
            {
                Debug.Log("Not a valid asset bundle");
            }
        }
    }

    IEnumerator ShowDownloadProgress(UnityWebRequest req)
    {
        float downloadDataProgress;
        while (!req.isDone)
        {
            /*
             * as BugFinder metnioned in the comments
             * what you want to track is uwr.downloadProgress
             */
            downloadDataProgress = req.downloadProgress * 100;
            Debug.Log("Download: " + downloadDataProgress);

            ProgressIndicator.text = ((int)downloadDataProgress).ToString() + "%";
            LoadingBar.fillAmount = downloadDataProgress / 100;

            yield return null;
        }
    }
}