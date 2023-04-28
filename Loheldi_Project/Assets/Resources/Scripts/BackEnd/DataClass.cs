using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//정보 저장 클래스

//차트 번호
public static class ChartNum
{
    public static string BasicCustomItemChart = "53501";
    public static string BasicClothesItemChart = "53497";
    public static string CustomItemChart = "64479";
    public static string ClothesItemChart = "78775";
    public static string AllItemChart = "57488";
    public static string BadgeChart = "54754";
    public static string GachaClothesChart = "56858";
    public static string GachaGaguChart = "56925";

}

//계정 정보
public class AccInfo
{
    public string NICKNAME;
    public DateTime BIRTH;
    public string PARENTSNO;
}

public class PlayInfo
{
    public int Wallet;
    public int Level;
    public float NowExp;
    public float MaxExp;
    public string QuestPreg;
    public int LastQTime;
    public int HP;
    public int LastHPTime;
    //public string WeeklyQuestPreg;
    public int HouseLv;
    public string HouseShape;
}
public class MySubQuest
{
    public int LastThankTreeTime;
}

public class MyItem
{
    public string ICode;
    public int Amount;
}

public class MyCustomItem
{
    public string ICode;
}

//상점 아이템 정보
public class StoreItem
{
    public string ICode;
    public string IName;
    public string Price;
    public string Category;
    public string ItemType;
}
public class CustomStoreItem
{
    public string ICode;
    public string IName;
    public string Price;
    public string Category;
    public string ItemType;
    public string Texture;
}

public class GachaItem
{
    public string ICode;
    public string ItemType;
    public string Amount;
    public string Percent;

}

public class Badge
{
    public string BCode;
    public string BName;
    public string Bcontent;
    public string Category;
}

//
public class GardenData
{
    public string G1;
    public DateTime G1Time;
    public string G2;
    public DateTime G2Time;
    public string G3;
    public DateTime G3Time;
    public string G4;
    public DateTime G4Time;
    public string Tree;
}
public class HousingData
{
    public string bed;
    public string closet;
    public string bookshelf;
    public string desk;
    public string table;
    public string coffeetable;
    public string sidetable;
    public string chair;
    public string chair2;
    public string chair3;
    public string sunbed;
    public string sunbed2;
    public string kitchen;
    public string fridge;
    public string standsink;
    public string wallshelf;
    public string sofa;
}

public class QuestInfo{
    public string QID;
    public string QName;
    public string From;
    public string Content;
    public string Reward;
    public string authorName;
    public string Type;
}
public class QuestPresent
{
    public string Q0;
    public string Q1;
    public string Q3;
    public string Q6;
    public string Q9;
    public string Q12;
    public string Q15;
    public string Q18;
    public string Q21;
    public string Q24;
    public string Q27;
    public string Q30;
    public string Q33;
}
