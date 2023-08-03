
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

//namespace used for the Outline class

//Animation class made by Olly - Student Number: 33697643
public class AnimatableXYZ : Interactable
{
    
    [Tooltip("Settings of animation style")]
    public bool x, y, z, usingGameObjectAnchors;
    [Tooltip("Use If you want the animation to reset directly after finishing with no user interaction")]
    public bool resetAfterInteraction;
    public float xDistance,xDistanceMin,xDistanceMax, 
        yDistance, yDistanceMin, yDistanceMax, 
        zDistance, zDistanceMin, zDistanceMax;
    public AnimationCurve speedCurve;
    [Tooltip("This is the linear order the game object will animate between.")]
    public Transform[] gameObjectAnchors;
    public int anchorIndex;
    
    private Vector3 animationPositionEnd = new Vector3(0,0,0);
    private Vector3 originPos;
    public float animationTime;
    private float resetAnimationTime;
    //Private classes --

    //This is done in awake, so the animation cannot be editted at run time. But for debug purposes, this could all be moved to
    //the update function or linked to a function called *UpdateAnimationPositions* - To do this please copy this awake code
    //into a new function and call that function in the Update function. Then nest this in a if statement, that is controlled
    //in the editor.

    public override void AwakeBehaviour()
    {
        if (!usingGameObjectAnchors)
        {
            //Setup the animation end pos using axis animation
            originPos = transform.position;
            animationPositionEnd = new Vector3(xDistance+originPos.x,yDistance+originPos.y,zDistance+originPos.z);
            //This correctly sets the animation positions if using axis animation
            if (!x)
            {
                animationPositionEnd = new Vector3(originPos.x, animationPositionEnd.y, animationPositionEnd.z);
            }
            if (!y)
            {
                animationPositionEnd = new Vector3(animationPositionEnd.x, originPos.y, animationPositionEnd.z);
            }
            if (!z)
            {
                animationPositionEnd = new Vector3(animationPositionEnd.x, animationPositionEnd.y, originPos.z);
            }
        }
        //Tells the environment designer that no animation is linked
        //Easy debugging purposes.
        if (!x & !y & !z & !usingGameObjectAnchors)
        {
            Debug.LogError("No animation state entered, no animation will be played. Please configure this in the game objects: "+ gameObject.name +" : Animatable Window");
        }

        //outline = GetComponent<Outline>();
        //if(outline != null) outline.enabled = false;
    }

    //Only use for debug purposes
    private void DebugUpdateAnimationPositions()
    {
        if (!usingGameObjectAnchors)
        {
            //Setup the animation end pos using axis animation
            originPos = transform.position;
            animationPositionEnd = new Vector3(xDistance + originPos.x, yDistance + originPos.y, zDistance + originPos.z);
            //This correctly sets the animation positions if using axis animation
            if (!x)
            {
                animationPositionEnd = new Vector3(originPos.x, animationPositionEnd.y, animationPositionEnd.z);
            }
            if (!y)
            {
                animationPositionEnd = new Vector3(animationPositionEnd.x, originPos.y, animationPositionEnd.z);
            }
            if (!z)
            {
                animationPositionEnd = new Vector3(animationPositionEnd.x, animationPositionEnd.y, originPos.z);
            }
        }
        //Tells the environment designer that no animation is linked
        //Easy debugging purposes.
        if (!x & !y & !z & !usingGameObjectAnchors)
        {
            Debug.LogError("No animation state entered, no animation will be played. Please configure this in the game objects: " + gameObject.name + " : Animatable Window");
        }
    }
    private void ResetAnimation()
    {
        if (!usingGameObjectAnchors)
        {
            ResetAxisAnimation();
        }
        else
        {
            ResetAnchorAnimation();
        }
    }
    private void RunAnimation()
    {
        if (!usingGameObjectAnchors)
        {
            AnimationAxis();
        }
        else
        {
            AnchorAnimation();
        }
    }
    private void ResetAxisAnimation()
    {
        animationTime = 0;
        if(transform.position != originPos)
        {
            resetAnimationTime += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(transform.position,originPos, speedCurve.Evaluate(resetAnimationTime));
        }
        else
        {
            resetAnimationTime = 0;
        }
    }
    
    //This is the func to animate using just the axis' and the animation curve.
    private void AnimationAxis()
    {
        resetAnimationTime = 0;
        if (Vector3.Distance(transform.position, animationPositionEnd) > .0025f)
        {
            animationTime += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(originPos, animationPositionEnd, speedCurve.Evaluate(animationTime));   
        }
        else
        {
            //This checks if the animation should reset after finishing.
            //This works as the Interactable class - Includes in the update function a check to reset animation.
            //As interacted with is being set to false and exit is being set to true, it goes through that update function.
            if (resetAfterInteraction)
            {
                interactedWith = false;
                exitInteraction = true;
            }
            animationTime = 0;
        }

    }
    /// <summary>
    /// Function to animate game objects using a list and linearly between these objects in this list
    /// </summary>
    private void AnchorAnimation()
    {
        //Everything else.
        animationTime += Time.deltaTime;
        if (anchorIndex < gameObjectAnchors.Length - 1)
        {
            transform.position = Vector3.Lerp(gameObjectAnchors[anchorIndex].transform.position,gameObjectAnchors[anchorIndex+1].transform.position, speedCurve.Evaluate(animationTime));    
            if (Vector3.Distance(transform.position, gameObjectAnchors[anchorIndex + 1].transform.position) <  0.005)
            {
                if (GetComponent<Collider>())
                    GetComponent<Collider>().enabled = false;
                anchorIndex++;
                animationTime = 0;
            }   
        }
        else
        {
            if (GetComponent<Collider>()) 
                GetComponent<Collider>().enabled = true;
            if (resetAfterInteraction)
            {
                transform.position = Vector3.Lerp(gameObjectAnchors[anchorIndex].transform.position,gameObjectAnchors[0].transform.position, speedCurve.Evaluate(animationTime)); 
                if (Vector3.Distance(transform.position, gameObjectAnchors[0].transform.position) <  0.005)
                {
                    anchorIndex = 0;
                    animationTime = 0;
                }    
            }
        }
    }

    private void ResetAnchorAnimation()
    {
        resetAnimationTime += Time.deltaTime;
        if (anchorIndex > 0)
        {
            transform.position = Vector3.Lerp(gameObjectAnchors[anchorIndex].transform.position, gameObjectAnchors[anchorIndex - 1].transform.position, speedCurve.Evaluate(resetAnimationTime));
            if (Vector3.Distance(transform.position, gameObjectAnchors[anchorIndex - 1].transform.position) < 0.005)
            {
                if (GetComponent<Collider>())
                    GetComponent<Collider>().enabled = false;
                anchorIndex--;
                resetAnimationTime = 0;
            }
        }
        else
        {
            if (GetComponent<Collider>())
                GetComponent<Collider>().enabled = true;
        }
    }

    public override void InteractionBehaviour()
    {;
        resetAnimationTime = 0;
        RunAnimation();
    }

    public override void InteractionExitBehaviour()
    {
        animationTime = 0;
        ResetAnimation();
    }
}
