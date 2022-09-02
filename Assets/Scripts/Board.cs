using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Board")]
public class Board : ScriptableObject
{
    public new string name; //Name of the board
    public string description; //Description for the board
    public string scene; //Scene that the board is in

    public int numberOfPlayers; //Maximum number of players that the board supports
}
