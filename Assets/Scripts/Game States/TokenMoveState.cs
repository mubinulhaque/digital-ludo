using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenMoveState : GameBaseState
{
    #region Variables
    private List<int> diceRolls = new List<int>(); //List of all dice rolls performed by the current player
    private int targetedToken = 0; //Index of the currently targeted token
    #endregion

    #region State Methods
    public override void StartState(GameManager manager) {
        targetedToken = 0;
        manager.board.targetToken(manager.getCurrentPlayer().getToken(targetedToken).transform);
    }

    public override void ReceiveInput(GameManager manager, string inputMessage) {
        switch(inputMessage) {
            case "navigateLeft":
                //Target the previous token
                targetedToken = (targetedToken + 3) % 4;
                manager.board.targetToken(manager.getCurrentPlayer().getToken(targetedToken).transform);
                break;

            case "navigateRight":
                //Target the next token
                targetedToken = (targetedToken + 1) % 4;
                manager.board.targetToken(manager.getCurrentPlayer().getToken(targetedToken).transform);
                break;

            case "select":
                int firstRoll = diceRolls[0];
                diceRolls.RemoveAt(0);
                manager.board.moveToken(manager.getCurrentPlayer().getToken(targetedToken), firstRoll);
                if(diceRolls.Count == 0) {
                    manager.board.getPlayerReady().changePlayer(manager.nextPlayer());
                    manager.switchState(manager.readyState);
                }
                break;

            default:
                Debug.Log("Input \"{inputMessage}\" is not accepted.");
                break;
        }
    }

    public void setDiceRolls(List<int> newDiceRolls) {
        if(newDiceRolls.Count < 4 && newDiceRolls.Count > 0) diceRolls = newDiceRolls;
    }
    #endregion
}
