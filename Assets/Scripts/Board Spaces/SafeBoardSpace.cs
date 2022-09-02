using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBoardSpace : BoardSpace
{
    //Effect of the board space that is activated on any token that steps on it
    public override void activateEffect(Token token)
    {
        token.setInvinciblility(true); //Makes the token invincible
    }
}
