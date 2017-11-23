using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 1 确认纵向能存放多少行
 * 
 * THeight =  button.height + heightGap;
 * 
 *  (Screen.Height / THeight)
 * 2 确认横向要存放多少列
 * 
 * 
 * */


public class Map_Login : BasicScene {

    Vector3[] m_vPos;
    //public int SceneNum;
    public float UnitWidth, UnitHeight, UnitWidthGap, UnitHeightGap;

    void Awake()
    {
        if (GlobalHelper.m_bFirstRun)
        {
            GlobalHelper.m_bFirstRun = false;
            GlobalHelper.InitializeGlobaData();
        }

        m_vPos = new Vector3[(int)eSceneType.Scene_Size];
    }

    int m_nColumnNum;
    void Start()
    {
        //计算当前屏幕一列能显示的行数
        m_nColumnNum = (int) ((Screen.height) / (UnitHeight + UnitHeightGap));
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        for (int i = 0; i < (int)eSceneType.Scene_Size; i++)
        {

            if(GUI.Button (
                new Rect ( (i/m_nColumnNum)*(UnitWidth + UnitWidthGap), i * (UnitHeightGap + UnitHeight), UnitWidth, UnitHeight),
                GlobalHelper.g_SceneDic[(eSceneType)i].SceneName
                ))
            {
                GlobalHelper.LoadLevel((eSceneType)i);
            }

            
        }
    }

}
