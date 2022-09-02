using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using TMPro;

public class PlayerDescriptor : MonoBehaviour
{
    #region Variables
    public TMP_Text device; //Text that displays the controller
    #endregion

    #region Custom Methods
    //Displays what device the corresponding player is using
    public void setDevice(InputDevice newDevice, int playerIndex) {
        device.text = newDevice.displayName.ToLower();
        //If the player is using a DS4 or DualSense controller
        if(newDevice.name == "DualShock4GamepadHID" || newDevice.name == "DualSenseGamepadHID") {
            //Change the lightbar
            DualShockGamepad dualshock = (DualShockGamepad) newDevice;
            dualshock.SetLightBarColor(GameManager.instance.getColor(playerIndex));
        }
    }
    #endregion
}