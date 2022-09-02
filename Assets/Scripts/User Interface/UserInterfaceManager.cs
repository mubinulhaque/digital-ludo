using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterfaceManager : MonoBehaviour
{
    #region Variables
    public Menu[] menus; //List of sub-menus
    private Menu currentMenu; //Menu that is currently displaying
    [SerializeField] private HoverBar hoverBar;
    #endregion

    #region Unity Methods
    private void Start() {
        if(menus.Length > 0) currentMenu = menus[0]; //Set the current menu to the first one
    }
    #endregion

    #region Custom Methods
    //Switch to a specified menu
    public void switchMenu(string menuName) {
        Menu newMenu = Array.Find(menus, m => m.parent.name == menuName); //Find the menu that has the specified menu
        newMenu.parent.SetActive(true); //Activate the new menu
        currentMenu.parent.SetActive(false); //Deactivate the old menu
        if(newMenu.firstButton != null) {
            //newMenu.firstButton.Select(); //Select the first button of the new menu
        } else {
            hoverBar.resetPosition();
            EventSystem.current.SetSelectedGameObject(null);
        }
        GameManager.instance.PlaySound("Button Click"); //Play a 'Button Click' sound
        currentMenu = newMenu;
    }

    public HoverBar getHoverBar() {
        return hoverBar;
    }
    #endregion
}
