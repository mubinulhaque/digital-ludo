using UnityEngine;

public abstract class GameBaseState
{
    public abstract void StartState(GameManager manager); //To be executed once the game manager switches to this state
    //The method below is commented out since it went unused in this version and the last. To be removed if unused in the next version.
    //public abstract void UpdateState(GameManager manager); //To be executed every frame the Game Manager is using the state
    public abstract void ReceiveInput(GameManager manager, string inputMessage); //When the current player's controller sends an input
}
