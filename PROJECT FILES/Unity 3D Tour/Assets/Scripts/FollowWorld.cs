using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowWorld : MonoBehaviour
{
//    [Header("Tweaks")]
//    [SerializeField] public Transform lookAt;
//    [SerializeField] public Vector3 offset;
    public GameObject gameObject;

    [Header("Logic")]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(this.transform.position);

        // https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html?_ga=2.63408550.820885107.1608608830-1774706090.1608608830
        if (pos.z > 0){
            gameObject.transform.position = pos;
        }

        // Debug.Log(pos.x + " " + pos.y + " " + pos.z);
    }
}
