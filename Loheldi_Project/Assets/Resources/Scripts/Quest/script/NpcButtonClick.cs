using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcButtonClick : MonoBehaviour
{
    [SerializeField]
    Text SecondButtonTxt;
    private UIButton UIB;
    public FlieChoice Chat;
    public GameObject ParentscheckUI;
    public GameObject ThankTreeUI;
    public Interaction Inter;

    [SerializeField] Trans trans;

    public string[] ButtonTxtK;
    public string[] ButtonTxtE;

    public void SecondButtonClick()
    {
        string[] ButtonTxt;
        if (trans.tranbool)
            ButtonTxt = ButtonTxtE;
        else
            ButtonTxt = ButtonTxtK;

        GameObject click = EventSystem.current.currentSelectedGameObject;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();

        if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[0]))
        {
            if (Inter.NameNPC.Equals("Bibim"))
                SceneLoader.instance.GotoBibimbapGame();
            else if (Inter.NameNPC.Equals("Fruit"))
                SceneLoader.instance.GotoDropFruitGame();
            else if (Inter.NameNPC.Equals("Wood"))
                SceneLoader.instance.GotoWoodGame();
            else
                SceneLoader.instance.GotoLobby();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[1]))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.HairShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[2]))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.clothesShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[3]))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.GaguShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[4]))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.Market.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[5]))
        {
            SceneLoader.instance.GotoPlayerCloset();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[6]))
        {
            SceneLoader.instance.GotoPlayerCustom();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[7]))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[8]))
        {
            SceneLoader.instance.GotoQuizGame();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[9]))
        {
            SceneLoader.instance.GotoGacha();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[10]))
        {
            UIB.chat.ChatEnd();
            ParentscheckUI.SetActive(true);
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[11]))
        {
            ThankTreeUI.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[12]))
        {
            SceneLoader.instance.GotoMainAcronVillage();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals(ButtonTxt[13]))
        {
            SceneLoader.instance.GotoMainField();
        }
    }
    public void CheckQuest()
    {

        GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");


        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
        
    }
}
