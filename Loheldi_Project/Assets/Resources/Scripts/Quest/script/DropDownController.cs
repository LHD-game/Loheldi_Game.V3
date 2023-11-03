using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropDownController : MonoBehaviour
{
    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int currentOption;
    Dropdown options;

    QuestDontDestroy QDD;
    List<string> optionList = new List<string>();

    void Awake()
    {
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false) currentOption = 0;
        else currentOption = PlayerPrefs.GetInt(DROPDOWN_KEY);

        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
    }

    void Start()
    {
        options = this.GetComponent<Dropdown>();

        options.ClearOptions();

        optionList.Add("한국어");
        optionList.Add("English");

        options.AddOptions(optionList);

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); LanguChange(); UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name); });
        setDropDown(currentOption); //최초 옵션 실행이 필요한 경우
    }

    void setDropDown(int option)
    {
        PlayerPrefs.SetInt(DROPDOWN_KEY, option);

        // option 관련 동작
        Debug.Log("current option : " + option); 
    }
    public void LanguChange()
    {
        switch (options.captionText.text.ToString())
        {
            case "한국어":
                QDD.Language = "Korean";
                break;
            case "English":
                QDD.Language = "English";
                break;
        }
        Debug.Log("드롭다운Language" + QDD.Language);
        PlayerPrefs.SetString("Language", QDD.Language);

    }
}
