using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreator : MonoBehaviour
{
    #region Variables
    public List<PlayerDescriptor> descriptors; //List of descriptors
    #endregion

    #region Invoked Methods
    //When a player joins
    public void onPlayerJoined(PlayerInput input) {
        PlayerDescriptor descriptor = descriptors[input.playerIndex];
        descriptor.gameObject.SetActive(true); //Enable an appropriate descriptor
        descriptor.setDevice(input.devices[0], input.playerIndex); //Show the device being used and set the lightbar colour
        GameManager.instance.addPlayer(input.GetComponent<Player>()); //Add the player to the GameManager
    }

    //When a player leaves
    public void onPlayerLeft(PlayerInput input) {
        Destroy(input.gameObject);
        print("Player disconnected!");
    }
    #endregion
}
