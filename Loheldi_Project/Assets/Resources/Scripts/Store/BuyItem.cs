using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    private GameObject StoreBuyPanel;
    [SerializeField]
    private GameObject MirrorInfoPanel;

    static GameObject main_ui;
    static GameObject buy_suc_panel;
    static string iCode = "";

    static int this_cost = 0;
    static int buycount = 0;

    public void PopBuyBtn()
    {
        //아이템 코드 가져와서 iCode 변수에 저장
        GameObject item_code = this.transform.Find("ItemCode").gameObject;
        Text item_code_txt = item_code.GetComponent<Text>();
        iCode = item_code_txt.text;

        //아이템 이름 가져와서 txt에 저장
        GameObject item_name = this.transform.Find("ItemName").gameObject;
        Text txt = item_name.GetComponent<Text>();
 
        //아이템 가격 가져와서 price_txt에 저장
        GameObject price_parent = this.transform.Find("CostImg").gameObject;
        GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: 구매 패널 객체 생성
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(StoreBuyPanel, main_ui.transform);

        buy_suc_panel = canvas.transform.Find("StoreBuySucPanel").gameObject;

        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;

        //아이템 이름 띄우기
        GameObject buy_item_name = Asset_StorePopup.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;

        //내 보유 코인 띄우기
        GameObject my_coin_parent = Asset_StorePopup.transform.Find("MyCoin").gameObject;
        GameObject my_coin = my_coin_parent.transform.Find("Text").gameObject;
        Text my_coin_txt = my_coin.GetComponent<Text>();
        my_coin_txt.text = PlayerPrefs.GetInt("Wallet").ToString(); //현재 나의 코인 수 띄운다.

        //아이템 가격 띄우기
        GameObject item_cost_parent = Asset_StorePopup.transform.Find("ItemCost").gameObject;
        GameObject item_cost = item_cost_parent.transform.Find("Text").gameObject;
        Text item_cost_txt = item_cost.GetComponent<Text>();
        item_cost_txt.text = price_txt.text;

        this_cost = Int32.Parse(item_cost_txt.text);    //현재 아이템 가격

        //잔액 띄우기
        int result_cost = Convert.ToInt32(my_coin_txt.text) - Convert.ToInt32(item_cost_txt.text);

        GameObject change_cost_parent = Asset_StorePopup.transform.Find("ChangeCost").gameObject;
        GameObject change_cost = change_cost_parent.transform.Find("Text").gameObject;
        Text change_cost_txt = change_cost.GetComponent<Text>();
        change_cost_txt.text = result_cost.ToString();

        GameObject DisableBuyBtn = Asset_StorePopup.transform.Find("DisableBuyBtn").gameObject;
        if(result_cost >= 0) //보유 코인>아이템 가격
        {
            change_cost_txt.color = Color.black;
            DisableBuyBtn.SetActive(false);
        }
        else
        {
            change_cost_txt.color = Color.red;
            DisableBuyBtn.SetActive(true);
        }
    }

    public void PopUpgradeBtn() {
        //아이템 이름 가져와서 txt에 저장
        GameObject name_parent = this.transform.Find("NameBg").gameObject;
        GameObject item_name = name_parent.transform.Find("ItemTxt").gameObject;
        Text txt = item_name.GetComponent<Text>();

        //아이템 가격 가져와서 price_txt에 저장
        GameObject price_parent = this.transform.Find("BtnPrefab").gameObject;
        GameObject item_price = price_parent.transform.Find("Text").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: 구매 패널 객체 생성
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(StoreBuyPanel, main_ui.transform);

        buy_suc_panel = canvas.transform.Find("StoreBuySucPanel").gameObject;

        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;

        //아이템 이름 띄우기
        GameObject buy_item_name = Asset_StorePopup.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;

        //내 보유 코인 띄우기
        GameObject my_coin_parent = Asset_StorePopup.transform.Find("MyCoin").gameObject;
        GameObject my_coin = my_coin_parent.transform.Find("Text").gameObject;
        Text my_coin_txt = my_coin.GetComponent<Text>();
        my_coin_txt.text = PlayerPrefs.GetInt("Wallet").ToString(); //현재 나의 코인 수 띄운다.

        //아이템 가격 띄우기
        GameObject item_cost_parent = Asset_StorePopup.transform.Find("ItemCost").gameObject;
        GameObject item_cost = item_cost_parent.transform.Find("Text").gameObject;
        Text item_cost_txt = item_cost.GetComponent<Text>();
        item_cost_txt.text = price_txt.text;

        this_cost = Int32.Parse(item_cost_txt.text);    //현재 아이템 가격

        //잔액 띄우기
        int result_cost = Convert.ToInt32(my_coin_txt.text) - Convert.ToInt32(item_cost_txt.text);

        GameObject change_cost_parent = Asset_StorePopup.transform.Find("ChangeCost").gameObject;
        GameObject change_cost = change_cost_parent.transform.Find("Text").gameObject;
        Text change_cost_txt = change_cost.GetComponent<Text>();
        change_cost_txt.text = result_cost.ToString();

        GameObject DisableBuyBtn = Asset_StorePopup.transform.Find("DisableBuyBtn").gameObject;
        if (result_cost >= 0) //보유 코인>아이템 가격
        {
            change_cost_txt.color = Color.black;
            DisableBuyBtn.SetActive(false);
        }
        else
        {
            change_cost_txt.color = Color.red;
            DisableBuyBtn.SetActive(true);
        }
    }

        public void BuyItemBtn()
    {
        //Inventory 테이블 불러와서, 여기에 해당하는 아이템과 일치하는 코드가 있을 경우 개수를 1증가시켜서 업데이트
        
        Where where = new Where();
        where.Equal("ICode", iCode);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //없을 경우 아이템 행 추가
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", 1);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("아이템 구입 완료: " +iCode);
                    PlayInfoManager.GetCoin(-this_cost);
                    CancleBtn();
                    buy_suc_panel.SetActive(true);
                    Gagu_Category.instance.PopGaguStore();  //목공방이었을 경우 업데이트 필요
                }
                else
                {
                    Debug.Log("아이템 구입 오류");
                }
            }
            //있을 경우 해당 아이템 indate찾고, 개수 수정
            else
            {
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount++;
                Debug.Log(item_amount);

                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("아이템 구입 완료: " + iCode);
                    PlayInfoManager.GetCoin(-this_cost);
                    CancleBtn();
                    buy_suc_panel.SetActive(true);
                    
                }
                else
                {
                    Debug.Log("아이템 구입 오류");
                }
            }
        }
    }
    //의상 아이템 구입
    public void BuyClothesBtn()
    {
        Param param = new Param();
        param.Add("ICode", iCode);

        var insert_bro = Backend.GameData.Insert("ACC_CLOSET", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("아이템 구입 완료: " + iCode);
            PlayInfoManager.GetCoin(-this_cost);
            CancleClothesBtn();
            buy_suc_panel.SetActive(true);
            Clothes_Category.instance.PopClothesStore();
            if (buycount == 0)
                MInfoPanel();
        }
        else
        {
            Debug.Log("아이템 구입 오류");
        }
    }

    //의상 아이템 구입
    public void BuyCustomBtn()
    {
        Param param = new Param();
        param.Add("ICode", iCode);

        var insert_bro = Backend.GameData.Insert("ACC_CUSTOM", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("아이템 구입 완료: " + iCode);
            PlayInfoManager.GetCoin(-this_cost);
            CancleCustomBtn();
            buy_suc_panel.SetActive(true);
            Custom_Category.instance.PopCustomStore();
            if (buycount == 0)
                MInfoPanel();
        }
        else
        {
            Debug.Log("아이템 구입 오류");
        }
    }

    public void BuyUpgradeBtn(GameObject ItemnameObject)
    {
        string Itemname = ItemnameObject.GetComponent<Text>().text;
        if (Itemname == "기본집" || Itemname == "알록달록집" || Itemname == "한옥" || Itemname == "통나무집")
        {
            string Themename;
            switch (Itemname)
            {

                case "기본집":
                    Themename = "Plane";
                    break;
                case "알록달록집":
                    Themename = "RRok";
                    break;
                case "한옥":
                    Themename = "Hanok";
                    break;
                case "통나무집":
                    Themename = "Wood";
                    break;
                default:
                    Debug.Log("집 이름 다름");
                    Themename = "Plane";
                    break;
            }

            PlayInfoManager.GetCoin(-this_cost);
            Param param = new Param();
            param.Add("HouseShape", Themename);

            var bro = Backend.GameData.Get("PLAY_INFO", new Where());
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();
            //해당 row의 값을 update
            var bro2 = Backend.GameData.UpdateV2("PLAY_INFO", rowIndate, Backend.UserInDate, param);

            if (bro2.IsSuccess())
            {
                Debug.Log(Themename);
                Debug.Log("SaveUserTheme 성공. PLAY_INFO 업데이트 되었습니다.");
            }
            else
            {
                Debug.Log("SaveUserTheme 실패.");
            }

        }
        else
        {
            int new_house_lv = PlayerPrefs.GetInt("HouseLv");
            new_house_lv++;
            PlayerPrefs.SetInt("HouseLv", new_house_lv);
            PlayInfoManager.GetCoin(-this_cost);

            if (new_house_lv == 2)
            {
                BadgeManager.GetBadge("B11");
            }
            else if (new_house_lv == 3)
            {
                BadgeManager.GetBadge("B12");
            }
        }
        CancleUpgradeBtn();
        buy_suc_panel.SetActive(true);
        Gagu_Category.instance.ChkUpgrade();
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().GotoWelcome();
    }


    public void CancleBtn()
    {
        GameObject panel = main_ui.transform.Find("StoreBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleClothesBtn()
    {
        GameObject panel = main_ui.transform.Find("ClothesBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleCustomBtn()
    {
        GameObject panel = main_ui.transform.Find("CustomBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleUpgradeBtn()
    {
        GameObject panel = main_ui.transform.Find("UpgradeBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleMInfoPanel()
    {
        GameObject panel = main_ui.transform.Find("MirrorInfoPanel(Clone)").gameObject;
        Destroy(panel);
    }

    public void MInfoPanel()
    {
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(MirrorInfoPanel, main_ui.transform);

        buy_suc_panel = canvas.transform.Find("StoreBuySucPanel").gameObject;

        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;
        buycount++;
    }
}
