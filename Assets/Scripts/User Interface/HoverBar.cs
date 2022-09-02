using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBar : MonoBehaviour
{
    #region Variables
    private float moveTransitionTime = 0.5f; //How long the transition from its old position to its new position should take
    private float moveTimer; //Timer that counts from 0 to moveTransitionTime
    private Vector3 targetPosition; //Position that the hover bar should move to
    #endregion

    #region Unity Methods
    void Start()
    {
        targetPosition = transform.position; //Keep it in its current position
        moveTimer = moveTransitionTime; //Set the timer off
    }

    void Update()
    {
        if(moveTimer < moveTransitionTime) { //If the timer is on
            moveTimer += Time.deltaTime;  //Add the time between each frame
            //Linearly interpolate from the current position to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveTimer / moveTransitionTime);
        } else { //If the timer is off
            transform.position = targetPosition; //Set its current position to the target position
        }
    }
    #endregion

    #region Custom Methods
    //Sets a new target position and starts the timer
    public void moveTo(float newPosY) {
        targetPosition = new Vector3(transform.position.x, newPosY, 0);
        moveTimer = 0f;
    }

    //Moves the hover bar away from the screen
    public void resetPosition() {
        targetPosition = new Vector3(transform.position.x, -90, 0);
        moveTimer = 0f;
    }
    #endregion
}
