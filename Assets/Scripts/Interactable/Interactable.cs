using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the master class for the interactable objects in the game.
/// This will be the parent of all objects that can be interacted with, and their functionality scripts
/// This means we only have to access one component as they all inherit from this class.
/// </summary>

//By Oliver Jones
public class Interactable : MonoBehaviour
{
    public bool interactedWith;
    public bool exitInteraction;
    public string promptMessage;
    public bool hovering;
    private bool interactionSoundPlayed;
    private bool interactionExitSoundPlayed;
    public AudioClip interactionSound, interactionExitSound;
    public bool playExitAudio;

    private void Start()
    {
        AdditionalStartFunctions();
    }

    private void Awake()
    {
        AwakeBehaviour();

        hovering = false;
    }

    private void Update()
    {

        if (interactedWith)
        {
            InteractionBehaviour();
            if (!interactionSoundPlayed)
            {
                if (interactionSound != null)
                {
                    playExitAudio = true;
                }
                interactionSoundPlayed = true;interactionExitSoundPlayed = false;
            }
        }
        if (exitInteraction)
        {
            InteractionExitBehaviour();
            if (!interactionExitSoundPlayed)
            {
                interactionSoundPlayed = false;
                interactionExitSoundPlayed = true;
            }
        }
    }

    public void Interaction()
    {
        if (!interactedWith)
        {
            interactedWith = true;
            exitInteraction = false;
        }
        else
        {
            interactedWith = false;
            exitInteraction = true;
        }
    }
    public virtual void AwakeBehaviour(){}

    public virtual void InteractionBehaviour(){}

    public virtual void InteractionExitBehaviour(){}

    public virtual void AdditionalStartFunctions(){}
    /**
    * Hightlight function made by: Richard Bennett - Student Number: 33625957
    * Toggles the Outline script when the crosshair hovers over object
    */
}


