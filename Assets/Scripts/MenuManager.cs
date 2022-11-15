using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> menusList;
    public int menusToSkip;

    public void Start()
    {
        HideAllMenusSkiping(menusToSkip);
    }
    public void setMenusList(List<GameObject> menus)
    {
        foreach(GameObject menu in menus)
        {
            menusList.Add(menu);
        }
    }
    public void HideAllMenusSkiping(int menusToSkip)
    {
        foreach (GameObject menu in menusList.Skip(menusToSkip))
        {
            menu.SetActive(false);
        }
    }

    public void ShowMenu(int menuIndex)
    {
        if (menusList.Count > menuIndex)
            menusList[menuIndex].SetActive(true);
    }
    public void HideMenu(int menuIndex)
    {
        if (menusList.Count > menuIndex)
            menusList[menuIndex].SetActive(false);
    }

    public void ShowHideMenu(int menuIndex)
    {
        if (menusList.Count > menuIndex)
        {
            bool reverseActive = !menusList[menuIndex].activeSelf;
            menusList[menuIndex].SetActive(reverseActive);
        }
    }
}
