using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSection : MonoBehaviour
{
    #region Variables
    public int index = 0; //Index of the board section that is used to load into the GameManager correctly

    [SerializeField] private Color startingColour;
    [SerializeField] private List<Token> tokens;

    public List<BoardSpace> boardSpaces = new List<BoardSpace>(); //List of board spaces in the section
    public List<BoardSpace> endBoardSpaces = new List<BoardSpace>(); //List of board spaces that lead to the goal
    #endregion

    #region Unity Methods
    void Start()
    {
        setColour(startingColour);
    }

    void Update()
    {
        
    }
    #endregion

    #region Getter & Setter Methods
    public Token getToken(int index) {
        //Only return a token if it exists
        if(index >= 0 && index < tokens.Count) return tokens[index];
        else return null;
    }

    //Sets the colour of each board space and token to a specified colour
    public void setColour(Color newColour)
    {
        foreach(BoardSpace space in boardSpaces)
        {
            space.setColour(newColour);
        }

        foreach (BoardSpace space in endBoardSpaces)
        {
            space.setColour(newColour);
        }

        foreach(Token token in tokens) {
            token.setColour(newColour);
        }
    }
    #endregion
}
