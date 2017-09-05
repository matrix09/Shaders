using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScene : MonoBehaviour {

    protected virtual void OnGUI()
    {
        for (int i = 0; i < 1; i++)
        {
            if (GUI.Button(
                new Rect(0, 0, 100, 50), "Leave Scene"
                ))
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Map_Login")
                {
                    Application.Quit();
                }
                else
                GlobalHelper.LoadLevel(eSceneType.Scene_MapLogin);
            }
        }
    }
}
