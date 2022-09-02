using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    #region Variables
    public UserInterfaceManager manager; //User interface manager
    public TMP_Text text; //Text of the button
    private float centerY;
    #endregion

    #region Unity Methods
    private void Start() {
        //Get the center of the button
        centerY = GetComponent<RectTransform>().rect.min.y + GetComponent<RectTransform>().rect.height / 2;
    }

    //When the mouse starts hovering over the button
    public void OnPointerEnter(PointerEventData data) {
        GetComponent<Button>().Select();
    }

    //When the button is clicked / pressed
    public void OnSelect(BaseEventData data) {
        manager.getHoverBar().moveTo(transform.position.y + centerY); //Move the hover bar over the button
    }
    #endregion
}
