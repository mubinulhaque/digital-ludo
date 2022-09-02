using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region Custom Methods
    //Gets the value of the face that is the closest to facing upwards
    public int getFaceValue() {
        return Random.Range(1, 7); //Placeholder code; to be replaced by code that calculates face facing upwards and return its value
    }
    #endregion
}
