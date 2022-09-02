using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject capsule;
    
    private int playerIndex = 0;
    private int spacesTravelled = -1; //Number of board spaces travelled
    private Vector3 startingPosition; //Spawn position of the token
    private bool invincible = false; //Is the token invincible?
    #endregion

    #region Unity Methods
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        
    }
    #endregion

    #region Custom Methods
    public void setInvinciblility(bool newState)
    {
        invincible = newState;
    }

    public int getSpacesTravelled() {
        return spacesTravelled;
    }

    public void moveTo(BoardSpace newSpace, int distance) {
        transform.position = newSpace.transform.position;
        newSpace.activateEffect(this);
        spacesTravelled += distance;
    }

    //To be used when a token gets eaten
    public void resetPosition() {
        transform.position = startingPosition;
        spacesTravelled = -1;
    }

    public void setColour(Color newColour) {
        capsule.GetComponent<Renderer>().material.color = newColour;
    }

    public int getOwner() {
        return playerIndex;
    }

    public void setOwner(Player player) {
        playerIndex = player.getIndex();
    }
    #endregion
}
