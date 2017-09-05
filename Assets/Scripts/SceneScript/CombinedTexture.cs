using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedTexture : BasicScene {


    public GameObject DetailedTex;
    bool bDetaileddTex = true;
    public GameObject CombinedTex;
    bool bCombinedTex = false;
    void Awake()
    {
        CombinedTex.SetActive(false);
    }

    protected override void OnGUI()
    {
        base.OnGUI();

        if (GUI.Button(new Rect ((Screen.width - 100) * 0.5f, 30, 100, 30), "Detailed Tex"))
        {
            bDetaileddTex = !bDetaileddTex;
         
            bCombinedTex = false;
            DetailedTex.SetActive(bDetaileddTex);
            CombinedTex.SetActive(bCombinedTex);

        }
        else if (GUI.Button(new Rect((Screen.width - 100) * 0.5f, 70, 100, 30), "Combined Tex"))
        {
            bCombinedTex = !bCombinedTex;
            bDetaileddTex = false;
            DetailedTex.SetActive(bDetaileddTex);
            CombinedTex.SetActive(bCombinedTex);
        }

    }

}
