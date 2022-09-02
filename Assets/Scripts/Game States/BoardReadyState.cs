using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardReadyState : GameBaseState
{
    #region Variables
    private PlayerReady playerReady; //The PlayerReady of the board manager
    private List<int> diceRolls = new List<int>(); //List of all dice rolls performed by the current player
    #endregion

    #region State Methods
    public override void StartState(GameManager manager) {
        if(playerReady == null) playerReady = manager.board.getPlayerReady();
        playerReady.gameObject.SetActive(true);
        diceRolls.Clear();
    }

    public override void ReceiveInput(GameManager manager, string inputMessage) {
        switch(inputMessage) {
            case "select":
                if(playerReady.isActiveAndEnabled) {
                    playerReady.gameObject.SetActive(false);
                } else {
                    int diceValue = manager.board.getDice().getFaceValue();
                    Debug.Log("Dice value: " + diceValue.ToString());
                    
                    if(diceValue != 6) { //If the dice has not landed on 6
                        //Switch to the token moving state with the dice rolls
                        diceRolls.Add(diceValue);
                        manager.tokenMoveState.setDiceRolls(diceRolls);
                        manager.switchState(manager.tokenMoveState);
                    } else { //If the dice has landed on 6
                        //Add to a list of other dice rolls already made
                        diceRolls.Add(diceValue);
                        
                        if(diceRolls.Count >= 3) { //If the player has already done 3 dice rolls consecutively
                            //Switch to the next player
                            Debug.Log("3 sixes gotten!");
                            playerReady.changePlayer(manager.nextPlayer());
                            manager.switchState(manager.readyState);
                        }
                    }
                }
                break;

            default:
                Debug.Log("Input \"{inputMessage}\" is not accepted.");
                break;
        }
    }
    #endregion
}
