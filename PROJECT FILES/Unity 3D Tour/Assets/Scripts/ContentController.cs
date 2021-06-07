using UnityEngine;
using System.Collections;

public class ContentController : MonoBehaviour
{
    public API api;
    public GameObject CircularProgressBar;

    void Start()
    {
        CircularProgressBar.SetActive(false);
    }

    public void LoadContent(string url)
    {
        CircularProgressBar.SetActive(true);
        DestroyAllChildren();
        api.GetBundleObject(url, OnContentLoaded, transform);
    }

    void OnContentLoaded(GameObject content)
    {
        //do something cool here
        Debug.Log("Loaded: " + content.name);
        CircularProgressBar.SetActive(false);
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}