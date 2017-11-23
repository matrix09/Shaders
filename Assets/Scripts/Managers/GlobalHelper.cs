using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
//public class Test : System.Object
//{
//    public int p = 5;
//    public Color c = Color.white;
//}


public class GlobalHelper {

    public static Dictionary<eSceneType, SceneInfo> g_SceneDic = new Dictionary<eSceneType, SceneInfo>();

    public static bool m_bFirstRun = true;

    public static void InitializeGlobaData()
    {
        SceneInfo si = new SceneInfo(); si.SceneName = "Map_Login";
        g_SceneDic.Add(eSceneType.Scene_MapLogin, si);
        si = new SceneInfo(); si.SceneName = "Map_ProGrid";
        g_SceneDic.Add(eSceneType.Scene_ProGrid, si);
        si = new SceneInfo(); si.SceneName = "Map_Matrices";
        g_SceneDic.Add(eSceneType.Scene_Matrices, si);
        si = new SceneInfo(); si.SceneName = "Map_ShaderFunda";
        g_SceneDic.Add(eSceneType.Scene_ShaderFunda, si);
        si = new SceneInfo(); si.SceneName = "Map_CombineTexture";
        g_SceneDic.Add(eSceneType.Scene_CombinedTexture, si);
        si = new SceneInfo(); si.SceneName = "Map_SingleLighting";
        g_SceneDic.Add(eSceneType.Scene_SingleLight, si);
    }

    public static void LoadLevel(eSceneType type)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(g_SceneDic[type].SceneName);
    }



}
