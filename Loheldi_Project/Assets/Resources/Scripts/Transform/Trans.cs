using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Trans : MonoBehaviour
{
    public GameObject[] KList;
    public GameObject[] EList;
    QuestDontDestroy QDD;
    public bool tranbool = false;  //f-한글 t-영어
    Dropdown options;

    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int currentOption;

    private void Awake()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Debug.Log("Trans 실행");
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        string Lang;
        if (PlayerPrefs.HasKey("Language") == false) Lang = "English";
        else Lang = PlayerPrefs.GetString("Language");
        tranbool = CheckLanguage(Lang);
        foreach (GameObject e in KList)
        {
            if (tranbool)
                e.SetActive(false);
            else
                e.SetActive(true);
        }

        foreach (GameObject e in EList)
        {
            if (tranbool)
                e.SetActive(true);
            else
                e.SetActive(false);
        }
        chartNumChange();
    }

    void chartNumChange()
    {
        if (tranbool)
        {
            ChartNum.AllItemChart = "83387";
            ChartNum.BadgeChart = "87537";
            ChartNum.ClothesItemChart = "83388";
            ChartNum.CustomItemChart = "83391";
        }
        else
        {
            ChartNum.AllItemChart = "57488";
            ChartNum.BadgeChart = "54754";
            ChartNum.ClothesItemChart = "64485";
            ChartNum.CustomItemChart = "64479";
        }
    }

        private bool CheckLanguage(string NowLang)
        {
            bool activeL;
            switch(NowLang)
            {
                case "한국어":
                    activeL = false;
                    break;
                case "English":
                    activeL = true;
                    break;
                default:
                    activeL = false;
                    break;
            }

            return activeL;
        }

    void setDropDown(int option)
    {

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
        setDropDown(currentOption); //최초 옵션 실행이 필요한 경우

        PlayerPrefs.SetInt(DROPDOWN_KEY, option);

        // option 관련 동작
        Debug.Log("current option : " + option);
    }
}
