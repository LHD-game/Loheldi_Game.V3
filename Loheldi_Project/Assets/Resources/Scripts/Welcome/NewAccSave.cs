//CreateAcc Scene에서 계정 정보를 저장하기 위한 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using BackEnd;
using System;

public class NewAccSave : MonoBehaviour
{
    //panel
    [SerializeField]
    private GameObject NariField;   //나리의 설명
    [SerializeField]
    private GameObject NickNameField;   //닉네임 입력 화면
    [SerializeField]
    private GameObject BirthField;   //생년월일 입력 화면
    [SerializeField]
    private GameObject ParentsNoField;   //보호자 인증번호 입력 화면
    [SerializeField]
    private GameObject ResultField;   //값 확인용

    //값 입력 필드
    [SerializeField]
    private InputField InputNickName;   //계정 닉네임
    [SerializeField]
    private Dropdown[] InputBirth = new Dropdown[3];      //계정주 생년,월,일
    [SerializeField]
    private InputField InputParentsNo;   //계정 보호자 인증번호
    public GameObject PrivacyChkBox;    //개인정보이용동의 체크박스

    public static string uNickName;   // 서버에 저장되기 전 값을 담아놓는 변수
    public static DateTime uBirth;    // 위와 같음
    public static string uParentsNo;   // 서버에 저장되기 전 값을 담아놓는 변수

    public Text nick;
    public Text birth;
    public Text parentsNo;

    bool isPrivacyChk = false;
    public static bool nari_can_talk = true;

    [SerializeField]
    private Trans trans;

    void Start()
    {
        NariField.SetActive(true);
        NickNameField.SetActive(true);
        ParentsNoField.SetActive(false);
        BirthField.SetActive(false);
        ResultField.SetActive(false);
    }

    private void Update()
    {
        //입력 확인용 결과 출력
        String name = "";
        String YMD = "";
        String ParentsN = "";
        if (!trans.tranbool)
        {
            name = "닉네임";
            YMD = "생년월일";
            ParentsN = "부모님 인증번호";
        }
        else
        {
            name = "NickName";
            YMD = "Birth";
            ParentsN = "Gurdian Number";
        }
        nick.text = name + ": " + uNickName;
        birth.text = YMD + ": " + uBirth.ToString("yyyy.M.d");
        parentsNo.text = ParentsN + ": " + uParentsNo;
    }

    public void SaveNickName()  //닉네임 입력 후 버튼을 눌렀을 경우 실행
    {
        Regex regex = new Regex(@"[a-zA-Z가-힣ㄱ-ㅎ0-9]{2,8}$"); //닉네임 정규식. 영대소문자, 한글 2~8자 가능

        if ((regex.IsMatch(InputNickName.text))) //정규식 일치시,
        {
            uNickName = InputNickName.text; //uNickName 변수에 입력값을 저장하고,
            
            ShowNHide(BirthField, NickNameField);   //닉네임 입력 비활성화, 생일 입력 활성화
            nari_can_talk = true;
        }
        else    //정규식 불일치시
        {
            //오류 팝업 활성화
            Transform t = NickNameField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    public void SaveBirth() //생년월일 입력 후 버튼을 눌렀을 경우 실행
    {
        Regex regex = new Regex(@"[0-9]{1,5}$"); //생년월일 정규식
        bool isOK = true;
        for (int i=0; i<InputBirth.Length; i++)
        {
            string birthValue = InputBirth[i].options[InputBirth[i].value].text;
            if (!(regex.IsMatch(birthValue)))  //정규식 불일치 시,
            {
                isOK = false;
                Debug.Log(birthValue);
                nari_can_talk = false;
            }
        }

        if (isOK)   //모두 정규식 일치하면
        {
            string str = InputBirth[0].options[InputBirth[0].value].text + "/";
            str += InputBirth[1].options[InputBirth[1].value].text + "/";
            str += InputBirth[2].options[InputBirth[2].value].text; //yyyy/MM/dd
            Debug.Log(str);
            uBirth = Convert.ToDateTime(str);   //uBirth 변수에 입력값 저장
            ShowNHide(ParentsNoField, BirthField);
            nari_can_talk = true;
        }
        else    //정규식 불일치시
        {
            //오류 팝업 활성화
            Transform t = BirthField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    public void SaveParentsNo()  //보호자 인증번호 입력 후 버튼을 눌렀을 경우 실행
    {
        Regex regex = new Regex(@"[0-9]{4,6}$"); //정규식. 숫자 4~6자 가능

        if ((regex.IsMatch(InputParentsNo.text))) //정규식 일치시,
        {
            uParentsNo = InputParentsNo.text; //uParentsNo 변수에 입력값을 저장하고,

            ShowNHide(ResultField, ParentsNoField);   //닉네임 입력 비활성화, 생일 입력 활성화
            nari_can_talk = true;
        }
        else    //정규식 불일치시
        {
            //오류 팝업 활성화
            Transform t = ParentsNoField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    private void ShowNHide(GameObject show, GameObject hide)    //활성화 할 것 첫번째, 비활성화 할 것은 두번째 인자로 준다.
    {
        show.SetActive(true);
        hide.SetActive(false);
    }

    public void ClosePop(GameObject go) //오류 팝업 닫기 메소드
    {
        go.SetActive(false);
    }

    public void PrivacyChk()
    {
        isPrivacyChk = PrivacyChkBox.GetComponent<Toggle>().isOn;
        if (isPrivacyChk)
        {
            nari_can_talk = true;
        }
        else
        {
            //오류 팝업 활성화
            Transform t = ResultField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    //계정 정보를 다시 입력하기 위한 메소드(아니야! 선택)
    public void ReturnToFirst()
    {
        ResultField.SetActive(false);   //결과 패널 비활성화
        NickNameField.SetActive(true);  //닉네임 입력 패널 활성화
        AccNariAdvice.print_line = 6;
    }

    //최종적으로 서버에 닉네임과 생년월일을 저장하는 메소드
    public void AccSave()
    {
        if (isPrivacyChk)
        {
            Param param = new Param();
            param.Add("NICKNAME", uNickName);
            param.Add("BIRTH", uBirth);
            param.Add("PARENTSNO", uParentsNo);


            var bro = Backend.GameData.Insert("ACC_INFO", param);

            if (bro.IsSuccess())
            {
                Debug.Log("계정 정보 설정 완료!");

                Save_Basic.LoadAccInfo();   //계정정보를 로컬에 저장
                Save_Basic.SaveBasicClothes(); //기본 의상 아이템 저장 
            }
            else
            {
                Debug.Log("계정 정보 설정 실패!");
                //todo: 오류 문의 안내 문구 띄우기
            }
        }
    }
}
