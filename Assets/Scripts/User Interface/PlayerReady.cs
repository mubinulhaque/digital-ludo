using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerReady : MonoBehaviour
{
    [SerializeField] private Image colour;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text actionText;

    public void changePlayer(int playerIndex) {
        colour.color = GameManager.instance.getColor(playerIndex);
        playerName.text = "player " + (playerIndex + 1).ToString();
    }
}
