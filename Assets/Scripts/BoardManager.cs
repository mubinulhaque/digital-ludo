using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerReady playerReady; //GameObject that describes the player's index, colour and controller
    [SerializeField] private List<BoardSection> board; //List of all board section objects
    [SerializeField] private Dice dice;
    [SerializeField] private new Camera camera; //Main camera
    private int boardLength; //Total number of board spaces that the board has
    #endregion

    #region Unity Methods
    private void Start() {
        //Set the game manager's board manager to this instance
        GameManager.instance.board = this;

        //Set each board section's tokens to their appropriate players
        for(int i = 0; i < GameManager.instance.getNumberOfPlayers(); i++) {
            GameManager.instance.getPlayer(i).addToken(board[i].getToken(0));
            GameManager.instance.getPlayer(i).addToken(board[i].getToken(1));
            GameManager.instance.getPlayer(i).addToken(board[i].getToken(2));
            GameManager.instance.getPlayer(i).addToken(board[i].getToken(3));
        }

        //Calculate the board's number of board by adding each board section's spaces
        boardLength = board[0].boardSpaces.Count;

        for(int section = 1; section < board.Count; section++) {
            boardLength += board[section].boardSpaces.Count;
        }
    }
    #endregion

    #region Getter & Setter Methods
    public PlayerReady getPlayerReady() {
        return playerReady;
    }

    public Dice getDice() {
        return dice;
    }
    #endregion

    #region Other Custom Methods
    //Move a specified token by a specified number of spaces
    public void moveToken(Token token, int diceRoll) {
        //Index of board space that the token will land on
        int newSpaceIndex = token.getSpacesTravelled() + diceRoll;

        //When the token goes beyond the board
        if(newSpaceIndex >= boardLength + board[token.getOwner()].endBoardSpaces.Count) {
            //To contain code for when the token goes beyond the board
        //If the token has reached their goal path
        } else if(newSpaceIndex > boardLength - 2) {
            //Move the token to a board space with an index that is subtracted by the board's length up to the goal path (i.e. boardLength - 1)
            newSpaceIndex = newSpaceIndex - boardLength + 1;
            token.moveTo(board[token.getOwner()].endBoardSpaces[newSpaceIndex], diceRoll);
        //If the specified token has gone past the starting point
        } else if(newSpaceIndex - diceRoll > -1) {
            //Get the specified token's board section
            int newSectionIndex = token.getOwner();

            //Until newSpaceIndex is less than the current section's number of spaces
            while(newSpaceIndex >= board[newSectionIndex].boardSpaces.Count) {
                //Subtract the current board section's number of spaces from newSpaceIndex
                newSpaceIndex = newSpaceIndex - board[newSectionIndex].boardSpaces.Count;
                //Change the current board section to the next one
                newSectionIndex = (newSectionIndex + 1) % board.Count;
            }
            
            //Move the specified token to the calculated board space
            token.moveTo(board[newSectionIndex].boardSpaces[newSpaceIndex], diceRoll);
        //If the specified token has not gone past the starting point
        } else if(diceRoll > 5) {
            //Move the token if the dice roll is 6
            token.moveTo(board[token.getOwner()].boardSpaces[0], 1);
        }
    }

    //Change the camera's position and rotation to point at a specfied token
    public void targetToken(Transform token) {
        //Move the camera that is at an offset to the token's position
        camera.transform.position = new Vector3(token.position.x - 2, token.position.y + 3, token.position.z - 2);
        //Make the camera look at the token
        camera.transform.LookAt(token);
        //Rotate the camera to look at the token's center
        camera.transform.Rotate(new Vector3(-10, 0, 0));
    }
    #endregion
}
