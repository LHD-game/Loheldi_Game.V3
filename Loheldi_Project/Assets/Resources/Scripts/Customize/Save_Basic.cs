using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Basic //초기값을 서버에 저장해주는 클래스
{
    public static void SaveBasicCustom()
    {
        string BasicCSV = ChartNum.BasicCustomItemChart;

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // 새 객체 생성

                param.Add("ICode", rows[i]["ICode"][0]);    //객체에 값 추가

                Backend.GameData.Insert("ACC_CUSTOM", param);   //객체를 서버에 업로드
            }
            PlayerCustomInit();
        } 
    }

    public static void SaveBasicClothes()
    {
        string BasicCSV = ChartNum.BasicClothesItemChart;

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // 새 객체 생성
                param.Add("ICode", rows[i]["ICode"][0]);    //객체에 값 추가

                Backend.GameData.Insert("ACC_CLOSET", param);   //객체를 서버에 업로드
            }
            PlayerClothesInit();
        }
    }

    static void PlayerCustomInit()  //유저의 초기 커스터마이징 정보를 서버에 저장
    {
        Param param = new Param();
        param.Add("Skin", "3030101");
        param.Add("Eyes", "3040101");
        param.Add("EColor", "gray");
        param.Add("Mouth", "3050101");
        param.Add("Hair", "3010101");
        param.Add("HColor", "black");

        Backend.GameData.Insert("USER_CUSTOM", param);
        Debug.Log("PlayerCustomInit");
    }

    static void PlayerClothesInit()  //유저의 초기 의상 정보를 서버에 저장
    {
        Param param = new Param();
        param.Add("Upper", "4010102");
        param.Add("UColor", "white");
        param.Add("Lower", "4020104");
        param.Add("LColor", "white");
        param.Add("Socks", "4030101");
        param.Add("SColor", "white");
        param.Add("Shoes", "4030105");
        param.Add("ShColor", "white");
        param.Add("Hat", "null");
        param.Add("Glasses", "null");
        param.Add("Bag", "null");

        Backend.GameData.Insert("USER_CLOTHES", param);
        Debug.Log("PlayerClothesInit");
    }

    //계정의 초기 재화, 레벨, 경험치, 최대 경험치량, 퀘스트 진행도, 지난 퀘스트 완료 시각 저장 메소드
    public static void PlayerInfoInit()
    {
        DateTime today = DateTime.Today;
        Param param = new Param();
        
        param.Add("Wallet", 10);
        param.Add("Level", 1);
        param.Add("NowExp", 0);
        param.Add("MaxExp", 10);
        param.Add("QuestPreg", "0_0");
        //param.AddNull("WeeklyQuestPreg");
        param.Add("LastQTime", today.Day);
        param.Add("HP", 5);
        param.Add("LastHPTime", today.Day);
        param.Add("HouseLv", 1);
        param.Add("HouseShape", "Plane");

        var bro = Backend.GameData.Insert("PLAY_INFO", param);

        if (bro.IsSuccess())
        {
            Debug.Log("PlayerInfoInit() Success");
        }
        else
        {
            Debug.Log("PlayerInfoInit() Fail");
        }
        
    }

    public static void SubQuestInfoInit()
    {
        Param param = new Param();

        param.Add("LastThankTreeTime", 0);

        var bro = Backend.GameData.Insert("USER_SUBQUEST", param);

        if (bro.IsSuccess())
        {
            Debug.Log("SubQuestInfoInit() Success");
        }
        else
        {
            Debug.Log("SubQuestInfoInit() Fail");
        }
    }
    public static void QuestStatusInfoInit()
    {
        Param param = new Param();

        param.Add("Q0", "false");
        param.Add("Q1", "false");
        param.Add("Q3", "false");
        param.Add("Q6", "false");
        param.Add("Q9", "false");
        param.Add("Q12", "false");
        param.Add("Q15", "false");
        param.Add("Q18", "false");
        param.Add("Q21", "false");
        param.Add("Q24", "false");
        param.Add("Q27", "false");
        param.Add("Q30", "false");
        param.Add("Q33", "false");

        var bro = Backend.GameData.Insert("QUEST_PRESENT", param);
        if (bro.IsSuccess())
        {
            Debug.Log("QuestStatusInfoInit() Success");
        }
        else
        {
            Debug.Log("QuestStatusInfoInit() Fail");
        }
    }

    //USER_GARDEN 테이블 초기값 저장
    public static void UserGardenInit()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_GARDEN", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserGardenInit() Success");
        }
        else
        {
            Debug.Log("UserGardenInit() Fail");
        }
    }
    //USER_HOUSE 테이블 초기값 저장
    public static void UserHousingInit()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_HOUSE", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserHousingInit() Success");
        }
        else
        {
            Debug.Log("UserHousingInit() Fail");
        }
    }
    public static void UserHousingInit2()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_HOUSE2F", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserHousingInit() Success");
        }
        else
        {
            Debug.Log("UserHousingInit() Fail");
        }
    }



    //play info 테이블 가져와 로컬에 저장하는 메소드
    public static void LoadPlayInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("PLAY_INFO", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                PlayInfo data = pj.ParseBackendData<PlayInfo>(json_data);
                //Debug.Log("퀘스트 진행도:" + data.QuestPreg);
                PlayerPrefs.SetInt("Wallet", data.Wallet);
                PlayerPrefs.SetInt("Level", data.Level);
                PlayerPrefs.SetFloat("NowExp", data.NowExp);
                PlayerPrefs.SetFloat("MaxExp", data.MaxExp);
                PlayerPrefs.SetString("QuestPreg", data.QuestPreg);
                PlayerPrefs.SetInt("LastQTime", data.LastQTime);
                PlayerPrefs.SetInt("HP", data.HP);
                PlayerPrefs.SetInt("LastHPTime", data.LastHPTime);
                //PlayerPrefs.SetString("WeeklyQuestPreg", data.WeeklyQuestPreg);
                PlayerPrefs.SetInt("HouseLv", data.HouseLv);
                PlayerPrefs.SetString("HouseShape", data.HouseShape);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
    public static void LoadQuestPresentInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("QUEST_PRESENT", new Where(), 13);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                QuestPresent data = pj.ParseBackendData<QuestPresent>(json_data);
                //Debug.Log("퀘스트 진행도:" + data.QuestPreg);
                PlayerPrefs.SetString("Q0", data.Q0);
                PlayerPrefs.SetString("Q1", data.Q1);
                PlayerPrefs.SetString("Q3", data.Q3);
                PlayerPrefs.SetString("Q6", data.Q6);
                PlayerPrefs.SetString("Q9", data.Q9);
                PlayerPrefs.SetString("Q12", data.Q12);
                PlayerPrefs.SetString("Q15", data.Q15);
                PlayerPrefs.SetString("Q18", data.Q18);
                PlayerPrefs.SetString("Q21", data.Q21);
                PlayerPrefs.SetString("Q24", data.Q24);
                PlayerPrefs.SetString("Q27", data.Q27);
                PlayerPrefs.SetString("Q30", data.Q30);
                PlayerPrefs.SetString("Q33", data.Q33);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log("ex");
            }
        }
    }

    public static void LoadSubQuestInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_SUBQUEST", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                MySubQuest data = pj.ParseBackendData<MySubQuest>(json_data);
                PlayerPrefs.SetInt("LastThankTreeTime", data.LastThankTreeTime);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
    //acc info 테이블 가져와 로컬에 저장하는 메소드: 닉네임, 생년월일, 보호자인증번호
    public static void LoadAccInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("ACC_INFO", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                AccInfo data = pj.ParseBackendData<AccInfo>(json_data);

                PlayerPrefs.SetString("Nickname", data.NICKNAME);
                PlayerPrefs.SetString("Birth", data.BIRTH.ToString("yyyy년 M월 d일"));
                PlayerPrefs.SetString("ParentsNo", data.PARENTSNO);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //user garden 테이블에서 값 가져와 로컬에 저장
    public static void LoadUserGarden()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_GARDEN", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                GardenData data = pj.ParseBackendData<GardenData>(json_data);

                string g1_t = data.G1Time.ToString("g");
                string g2_t = data.G2Time.ToString("g");
                string g3_t = data.G3Time.ToString("g");
                string g4_t = data.G4Time.ToString("g");

                PlayerPrefs.SetString("G1", data.G1);
                PlayerPrefs.SetString("G1Time", g1_t);
                PlayerPrefs.SetString("G2", data.G2);
                PlayerPrefs.SetString("G2Time", g2_t);
                PlayerPrefs.SetString("G3", data.G3);
                PlayerPrefs.SetString("G3Time", g3_t);
                PlayerPrefs.SetString("G4", data.G4);
                PlayerPrefs.SetString("G4Time", g4_t);
                PlayerPrefs.SetString("Tree", data.Tree);

            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //user house 테이블에서 값 가져와 로컬에 저장
    public static void LoadUserHousing()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_HOUSE", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                HousingData data = pj.ParseBackendData<HousingData>(json_data);

                PlayerPrefs.SetString("bed", data.bed);
                PlayerPrefs.SetString("closet", data.closet);
                PlayerPrefs.SetString("table", data.table);
                PlayerPrefs.SetString("chair", data.chair);
                PlayerPrefs.SetString("chair2", data.chair2);
                PlayerPrefs.SetString("side table", data.sidetable);
                PlayerPrefs.SetString("kitchen", data.kitchen);
                PlayerPrefs.SetString("fridge", data.fridge);
                PlayerPrefs.SetString("standsink", data.standsink);
                PlayerPrefs.SetString("wallshelf", data.wallshelf);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
    public static void LoadUserHousing2()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_HOUSE2F", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                HousingData data = pj.ParseBackendData<HousingData>(json_data);

                PlayerPrefs.SetString("bookshelf", data.bookshelf);
                PlayerPrefs.SetString("desk", data.desk);
                PlayerPrefs.SetString("coffee table", data.coffeetable);
                PlayerPrefs.SetString("chair3", data.chair3);
                PlayerPrefs.SetString("sunbed", data.sunbed);
                PlayerPrefs.SetString("sunbed2", data.sunbed2);
                PlayerPrefs.SetString("sofa", data.sofa);
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
}
