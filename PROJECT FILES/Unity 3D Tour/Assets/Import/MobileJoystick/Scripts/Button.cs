using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts
{
    /// <summary>
    /// Put it on any Image UI Element
    /// </summary>
    public class Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [HideInInspector]
        public bool Pressed;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }

}