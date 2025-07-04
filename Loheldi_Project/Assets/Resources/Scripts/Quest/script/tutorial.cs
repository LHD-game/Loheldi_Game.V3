using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public static GameObject button;
    public LodingTxt chat;
    private FlieChoice Chat;
    public int tutoi;                            //튜토리얼 하이라이트 이미지용
    public string tutoButtonName;

    public void Tutorial()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            chat.Chat.SetActive(false);
        if(chat.o ==2 && !chat.DontDestroy.tutorialLoading)
        {
            chat.Main_UI.SetActive(true);
            chat.block.transform.Find("Button8").gameObject.SetActive(true);
        }

        if (chat.o==5||(tutoi<=4&&tutoi>0))
        {
            if (tutoi < 4)
            {
                if (tutoi == 0)
                {
                    chat.Main_UI.SetActive(true);
                }
                else if (tutoi == 1)
                    chat.Main_UI.SetActive(false);
                chat.block.transform.GetChild(tutoi).gameObject.SetActive(true);
                if(tutoi>0)
                    chat.block.transform.GetChild(tutoi-1).gameObject.SetActive(false);
                tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                chat.block.transform.GetChild(3).gameObject.SetActive(false);
                chat.tutoFinish = true;
                chat.Chat.SetActive(true);
                chat.Line();
            }
        }

        if (chat.o==11)
        {
            if (tutoi < 7)
            {
                if (tutoi == 4)
                {
                    chat.Main_UI.SetActive(true);
                    chat.tutoclick = true;
                }
                else
                    chat.Main_UI.SetActive(false);
                chat.block.transform.GetChild(tutoi).gameObject.SetActive(true);
                if (tutoi > 4)
                    chat.block.transform.GetChild(tutoi - 1).gameObject.SetActive(false);
                tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                tutoi++;
                chat.block.transform.GetChild(6).gameObject.SetActive(false);
                chat.tutoFinish = true;
                chat.Chat.SetActive(true);
                chat.Line();
                GameObject SoundManager = GameObject.Find("SoundManager");
                SoundManager.GetComponent<SoundManager>().Sound("BGMField");
                chat.DontDestroy.tutorialLoading = false;
            }
        }
    }

}
