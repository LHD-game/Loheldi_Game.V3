using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcButtonClick : MonoBehaviour
{
    [SerializeField]
    Text SecondButtonTxt;
    //private Text TButtonTxt;
    private UIButton UIB;
    private FlieChoice Chat;
    // Start is called before the first frame update

    public void SecondButtonClick()
    {
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();
        Debug.Log(SecondButtonTxt.text);
        if (SecondButtonTxt.text.Equals("�̴ϰ��� �ϱ�"))
            SceneLoader.instance.GotoLobby();
        else if (SecondButtonTxt.text.Equals("�̿�� �̿��ϱ�"))
            SceneLoader.instance.GotoPlayerCloset();
        else if (SecondButtonTxt.text.Equals("������ �̿��ϱ�"))
        {
            UIB.shop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (SecondButtonTxt.text.Equals("����Ʈ �Ϲ�"))
        {
            Chat.Quest1();

            GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");

            for (int i = 0; i < clone.Length; i++)
            {
                Destroy(clone[i]);
            }
        }
    }
}