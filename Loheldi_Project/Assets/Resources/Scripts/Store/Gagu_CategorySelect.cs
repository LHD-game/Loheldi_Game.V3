using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gagu_CategorySelect : MonoBehaviour
{
    public GameObject UpgradePanel;
    public GameObject WoodPanel;
    public GameObject ModernPanel;
    public GameObject KitschPanel;
    public GameObject ClassicPanel;
    public GameObject ThemePanel;

    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        WoodPanel.SetActive(true);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void PopWood()
    {
        WoodPanel.SetActive(true);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void PopModern()
    {
        WoodPanel.SetActive(false);
        ModernPanel.SetActive(true);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void PopKitsch()
    {
        WoodPanel.SetActive(false);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(true);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void PopClassic()
    {
        WoodPanel.SetActive(false);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(true);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void PopTheme()
    {
        WoodPanel.SetActive(false);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(true);
        UpgradePanel.SetActive(false);
    }

    public void PopUpgrade()
    {
        WoodPanel.SetActive(false);
        ModernPanel.SetActive(false);
        KitschPanel.SetActive(false);
        ClassicPanel.SetActive(false);
        ThemePanel.SetActive(false);
        UpgradePanel.SetActive(true);
    }
}
