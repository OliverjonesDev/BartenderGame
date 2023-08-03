using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectSelector : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float maxDistance = 2;
    
    private Interactable lastInteracted;
    private Interactable tempObject;

    Interactable[] interactableChildren;

    [SerializeField] private TMP_Text hoverText;

    private void Start()
    {
        //get the interactable layer
        if (hoverText)
            hoverText.enabled = false;
    }

    private void Update()
    {
        if(lastInteracted != null){
            lastInteracted.hovering = false;
            hoverText.enabled = false;
        }  

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance))
        {
            //check if the target object has the interactable script
            lastInteracted = hit.collider.GetComponent<Interactable>();
            if (lastInteracted != null)
            {
                lastInteracted.hovering = true;
                hoverText.enabled = true;
                hoverText.text = lastInteracted.promptMessage;
            }
        }
        Debug.DrawRay(cam.position, cam.forward * maxDistance, Color.green);
    }

    public void Select()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance))
        {
            //check if the target object has the interactable script
            Interactable[] interactables = hit.collider.GetComponents<Interactable>();
            foreach (Interactable interactable in interactables)
            {
                if (tempObject != null)
                {
                    if (!interactable.transform.IsChildOf(tempObject.transform))
                    {
                        if (interactable != tempObject)
                        {
                            tempObject.playExitAudio = false;
                        }
                        tempObject.interactedWith = false;
                        tempObject.exitInteraction = true;
                    }
                }

                interactable.Interaction();
                tempObject = interactable;
            }

        }

    }
}

