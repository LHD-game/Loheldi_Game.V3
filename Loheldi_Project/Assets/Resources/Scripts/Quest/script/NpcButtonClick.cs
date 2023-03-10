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


    public void SecondButtonClick()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();

        if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미니게임 하기"))
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
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미용실 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.HairShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("의상실 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.clothesShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("가구점 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.GaguShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("마켓 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.Market.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("옷장 이용하기"))
        {
            SceneLoader.instance.GotoPlayerCloset();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("머리 다시하기"))
        {
            SceneLoader.instance.GotoPlayerCustom();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 하기") || click.transform.GetChild(0).GetComponent<Text>().text.Equals("어서오세요!"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("준비됐어!") || click.transform.GetChild(0).GetComponent<Text>().text.Equals("준비됐어요!"))
        {
            SceneLoader.instance.GotoQuizGame();
        }
        /*else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 힘찬"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 수호"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 여미"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 요미"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 메이"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 나리"))
        {
            Chat.Quest();
            CheckQuest();
        }*/
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("뽑기하기"))
        {
            SceneLoader.instance.GotoGacha();
        }
        /*else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("어서오세요!"))
        {
            Chat.Quest();
            CheckQuest();
        }*/
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미션 인증하기"))
        {
            UIB.chat.ChatEnd();
            ParentscheckUI.SetActive(true);
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("감사나무 가꾸기"))
        {
            ThankTreeUI.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("이동"))
        {
            SceneLoader.instance.GotoMainAcronVillage();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("이동한다"))
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
