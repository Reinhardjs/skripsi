using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Scripts
{
    public class SC_InventorySystem : MonoBehaviour
    {
        public Texture crosshairTexture;
        public FirstPersonController playerController;
        public SC_PickItem[] availableItems; //List with Prefabs of all the available items

        //Available items slots
        int[] itemSlots = new int[6];
        bool showInventory = false;

        //Item Pick up
        SC_PickItem detectedItem;
        int detectedItemIndex;

        Vector2 firstTouchPosition;
        List<TouchField> touchedFields = new List<TouchField>();
        GameObject inventory;

        // Start is called before the first frame update
        void Start()
        {
            //Initialize Item Slots
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i] = -1;
            }

            for (int i = 0; i < availableItems.Length; i++)
            {
                itemSlots[i] = i;
            }

            inventory = GameObject.Find("Inventory");
            inventory.SetActive(false);
            
        }

        // Update is called once per frame
        void Update()
        {

            if (showInventory && touchedFields.Count > 0)
            {
                TouchField touchField = touchedFields[0];

                for (int j = 0; j < availableItems.Length; j++)
                {
                    if (touchField.itemName == availableItems[j].itemName)
                    {
                        RaycastHit hit;
                        Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            Transform objectHit = hit.transform;
                            Vector3 hitPoint = hit.point;
                            hitPoint += objectHit.up;
                            Instantiate(availableItems[j], hitPoint, transform.rotation);
                            // Instantiate(availableItems[j], playerController.playerCamera.transform.position + (playerController.playerCamera.transform.forward), Quaternion.identity);
                        }

                        break;
                    }
                }
                touchedFields.RemoveAt(0);
            }
        }

        public void addTouchedField(TouchField touchField)
        {
            touchedFields.Add(touchField);
        }

        public void removeItemClicked()
        {
            //Item delete
            if (detectedItem && detectedItemIndex > -1)
            {
                detectedItem.PickItem();
            }
        }

        public void toggleShowInventory()
        {
            showInventory = !showInventory;
            inventory.SetActive(showInventory);
        }

        void FixedUpdate()
        {

            //Detect if the Player is looking at any item
            RaycastHit hit;
            Ray ray = playerController.playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

            if (Physics.Raycast(ray, out hit, 5f))
            {
                Transform objectHit = hit.transform;

                if (objectHit.CompareTag("Respawn"))
                {
                    if ((detectedItem == null || detectedItem.transform != objectHit) && objectHit.GetComponent<SC_PickItem>() != null)
                    {
                        SC_PickItem itemTmp = objectHit.GetComponent<SC_PickItem>();

                        //Check if item is in availableItemsList
                        for (int i = 0; i < availableItems.Length; i++)
                        {
                            if (availableItems[i].itemName == itemTmp.itemName)
                            {
                                detectedItem = itemTmp;
                                detectedItemIndex = i;
                            }
                        }
                    }
                }
                else
                {
                    detectedItem = null;
                }
            }
            else
            {
                detectedItem = null;
            }
        }

        void OnGUI()
        {
            //Player crosshair
            GUI.color = detectedItem ? Color.green : Color.white;
            GUI.DrawTexture(new Rect(Screen.width / 2 - 4, Screen.height / 2 - 4, 8, 8), crosshairTexture);
            GUI.color = Color.white;

            //Pick up message
            if (detectedItem)
            {
                GUI.color = new Color(0, 0, 0, 0.84f);
                GUI.Label(new Rect(Screen.width / 2 - 75 + 1, Screen.height / 2 - 50 + 1, 150, 20), detectedItem.objectName);
                GUI.color = Color.green;
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 20), detectedItem.objectName);
            }
        }
    }
}