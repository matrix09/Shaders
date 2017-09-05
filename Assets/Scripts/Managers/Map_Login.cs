using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Login : BasicScene {

    Vector3[] m_vPos;
    //public int SceneNum;
    public float UnitWidth, UnitHeight;
    void Awake()
    {
        if (GlobalHelper.m_bFirstRun)
        {
            GlobalHelper.m_bFirstRun = false;
            GlobalHelper.InitializeGlobaData();
        }

        m_vPos = new Vector3[(int)eSceneType.Scene_Size];
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        for (int i = 0; i < (int)eSceneType.Scene_Size; i++)
        {
            if (GUI.Button(
                new Rect ((Screen.width - UnitWidth) * 0.5f,  (0.1f+ 1.2f*i) * UnitHeight, UnitWidth, UnitHeight),
                GlobalHelper.g_SceneDic[(eSceneType)i].SceneName
                ))
            {
                GlobalHelper.LoadLevel((eSceneType)i);
            }
        }
    }

}
