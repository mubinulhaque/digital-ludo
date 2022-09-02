using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Public Variables
    public static GameManager instance; //Singleton instance that any other class can refer to
    public BoardManager board;
    public Sound[] sounds; //List of sounds that the game manager can play
    public List<Player> players = new List<Player>();

    //Game States
    public BoardReadyState readyState = new BoardReadyState(); //State when a player starts their turn
    public TokenMoveState tokenMoveState = new TokenMoveState(); //State when a player moves a token
    private GameBaseState currentState; //Current game state

    //Private Variables
    private int currentPlayer = 0; //Index of the current player
    private Board boardScene; //Object that contains the selected board's scene
    //Group of default colours in order: blue, red, green, yellow, magenta, orange, cyan, white
    private Color[] defaultColours = new Color[] {Color.blue, Color.red, Color.green, Color.yellow, Color.magenta, new Color(1, 0.5f, 0), Color.cyan, Color.white};
    #endregion

    #region Unity Methods
    void Start()
    {
        if(currentState != null) currentState.StartState(this);

        if(instance == null) { //If instance does not exist
            instance = this; //Set instance to this instance
            DontDestroyOnLoad(gameObject); //Do not destroy this instance whenever a new scene loads
        } else { //If instance already exists
            Destroy(this); //Destroy this instance
            return;
        }

        foreach(Sound sound in sounds) { //For every sound
            sound.source = gameObject.AddComponent<AudioSource>(); //Add an AudioSource component
            //Set the component's values accordingly
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    void Update()
    {
        //if(currentState != null) currentState.UpdateState(this);
    }
    #endregion

    #region Getter & Setter Methods
    public Player getPlayer(int index) {
        return players[index];
    }

    public int getNumberOfPlayers() {
        return players.Count;
    }

    public void addPlayer(Player newPlayer) {
        newPlayer.setIndex(players.Count);
        players.Add(newPlayer);
    }

    public void removePlayer(int index) {
        players.RemoveAt(index);
    }

    public Player getCurrentPlayer() {
        return players[currentPlayer];
    }

    //Change the current player to the next player and return the next player's index
    public int nextPlayer() {
        currentPlayer = (currentPlayer + 1) % players.Count;
        print("Player " + (currentPlayer + 1).ToString() + "'s turn!");
        return currentPlayer;
    }

    public void switchActionMap(string newActionMap) {
        foreach(Player player in players) {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap(newActionMap);
        }
    }

    public Color getColor(int index) {
        return defaultColours[index];
    }

    public void switchState(GameBaseState newState) {
        currentState = newState;
        newState.StartState(this);
    }

    //Co-routine that tracks the progress of loading a scene
    IEnumerator LoadReadyBoard(string scene) {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(scene);

        while(!operation.isDone) {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }

        switchActionMap("game");
        switchState(readyState);
    }
    #endregion

    #region Invoked Methods
    public void setBoard(Board boardObject) {
        boardScene = boardObject;
    }

    //Load an already created board Scriptable Object
    public void loadBoard() {
        //or(int i = 0; i < boardScene.numberOfPlayers; i++) board.Add(null); //Add a dummy entry for each board section
        StartCoroutine(LoadReadyBoard("Scenes/" + boardScene.scene)); //Load the board's specified scene
    }
    #endregion

    #region Other Custom Methods
    //Plays a song with a specified name
    public void PlaySound(string soundName) {
        //Find the sound with the same name as soundName
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        
        //If the song was not found, stop the method
        if(sound == null) {
            Debug.LogWarning("Sound \"" + soundName + "\" was not found and thus cannot be played!");
            return;
        }

        sound.source.Play();
    }

    //Stops playing a song with a specified name
    public void StopSound(string soundName) {
        //Find the sound with the same name as soundName
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        
        //If the song was not found, stop the method
        if(sound == null) {
            Debug.LogWarning("Sound \"" + soundName + "\" was not found and thus cannot be stopped!");
            return;
        }

        sound.source.Stop();
    }

    //Sends an input to the current game state if the input is from the current player's controller
    public void sendInput(int playerIndex, string inputMessage) {
        if(currentPlayer == playerIndex) currentState.ReceiveInput(this, inputMessage);
    }

    //Exits the game
    public void exitGame() {
        Application.Quit();
        print("The game has closed!"); //To notify those in the Unity Editor that this method has been executed
    }
    #endregion
}
