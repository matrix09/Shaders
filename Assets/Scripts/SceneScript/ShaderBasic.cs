using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//更换纹理
//Mipmap
//filter mode
//aniso level
public class ShaderBasic : BasicScene {
    public float speed; 
	// Use this for initialization
	void Start () {
		
	}
    bool m_bRot = false;
    void Update()
    {
        if (!m_bRot)
            return;
        transform.rotation *= Quaternion.Euler(
            new Vector3 (
                    Random.Range(0f, 360f) * Time.deltaTime * speed,
                       Random.Range(0f, 360f) * Time.deltaTime * speed,
                   Random.Range(0f, 360f) * Time.deltaTime * speed
                )
            );
    }

    protected override void OnGUI()
    {
        base.OnGUI();

        if (GUI.Button(new Rect((Screen.width - 100) * 0.5f, 50, 100, 50), "Rot"))
        {
            m_bRot = !m_bRot;
        }


    }
    
}
