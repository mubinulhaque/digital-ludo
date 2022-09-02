using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    #region Variables
    public GameObject top; //Top GameObject that changes colour
    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region Custom Methods
    //Sets the colour of the top
    public void setColour(Color newColour) {
        top.GetComponent<Renderer>().material.color = newColour;
    }

    //Effect of the board space that is activated on any token that steps on it
    public virtual void activateEffect(Token token) { token.setInvinciblility(false); }
    #endregion
}
