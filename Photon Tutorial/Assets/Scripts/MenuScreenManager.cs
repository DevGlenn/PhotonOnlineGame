using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenManager : MonoBehaviour
{
    public static MenuScreenManager Instance;
    [SerializeField] MenuScreen[] menuscreens;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenuScreen(string menuScreenTitle)
    {
        for (int i = 0; i < menuscreens.Length; i++) // loop through all the menus
        {
            if (menuscreens[i].menuScreenTitle == menuScreenTitle) // check the name of the menu
            {
                menuscreens[i].OpenMenuScreen(); // if the name corresponds open the menu
            }
            else if (menuscreens[i].menuscreenIsOpen) // if this is not the case, or the menu is open and we want to close it.
            {
                CloseMenuScreen(menuscreens[i]); // close the menu
            }
        }
    }

    public void OpenMenuScreen(MenuScreen menuscreen)
    {
        for (int i = 0; i < menuscreens.Length; i++) // check everything
        {
            if (menuscreens[i].menuscreenIsOpen)
            {
                CloseMenuScreen(menuscreens[i]); // if one menu is already open, close the other one. because we only want 1 menu open at a time ofcourse
            }
        }
        menuscreen.OpenMenuScreen();
    }
    public void CloseMenuScreen(MenuScreen menuscreen) // close menu function
    {
        menuscreen.CloseMenuScreen();
    }
}
