using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public string menuScreenTitle;
    public bool menuscreenIsOpen;
    public void OpenMenuScreen()
    {
        menuscreenIsOpen = true;
        gameObject.SetActive(true);
    }

    public void CloseMenuScreen()
    {
        menuscreenIsOpen = false;
        gameObject.SetActive(false);
    }

}
