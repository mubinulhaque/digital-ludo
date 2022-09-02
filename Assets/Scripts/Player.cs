using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    #region Variables
    private List<Token> tokens = new List<Token>(); //List of the player's tokens
    private int index = 0; //Index of the player in the game manager
    #endregion

    #region Unity Methods
    private void Start() {
        DontDestroyOnLoad(this); //Do not destroy the player when transitioning between scenes!
    }
    #endregion

    #region Getter & Setter Methods
    //Returns the index of the player in the game manager
    //Used by tokens to set their owner
    public int getIndex() {
        return index;
    }

    //Used by the game manager to set the index
    public void setIndex(int newIndex) {
        index = newIndex;
        name = "Player " + index.ToString();
    }


    public Token getToken(int index) {
        return tokens[Mathf.Clamp(index, 0, 3)];
    }

    public void addToken(Token token) {
        if(tokens.Count < 4) {
            token.gameObject.name = "Token " + tokens.Count;
            token.transform.SetParent(transform);
            token.setOwner(this);
            tokens.Add(token);
        }
    }
    #endregion

    #region Invoked Methods
    //Remove the player when device is lost
    public void OnDeviceLost(PlayerInput input) {
        GameManager.instance.removePlayer(input.playerIndex);
        Destroy(input.gameObject);
    }

    //Send input when button is pressed
    public void select(InputAction.CallbackContext buttonPress) {
        if(buttonPress.action.triggered) GameManager.instance.sendInput(index, "select");
    }

    //For navigating between tokens
    public void navigate(InputAction.CallbackContext context) {
        float axis = context.ReadValue<float>();

        if(context.action.triggered && axis > 0.5) {
            GameManager.instance.sendInput(index, "navigateRight");
        } else if(context.action.triggered && axis < -0.5) {
            GameManager.instance.sendInput(index, "navigateLeft");
        }
    }
    #endregion
}
