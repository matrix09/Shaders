using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerV2 : MonoBehaviour {

    public GameObject m_oCenter;
    Dir m_ed;
    public float RotDrgee = 10.0f;
    float curDrgee;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.x<Screen.width/2)
            {
                m_ed = Dir.Dir_Left;
            }
            else
            {
                m_ed = Dir.Dir_Right;
            }
            curDrgee = 0.0f;
        }
        if(Input.GetMouseButton(0))
        {
            if(curDrgee == 0.0f)
            {
                if (m_ed == Dir.Dir_Left)
                {
                    curDrgee = RotDrgee;
                }
                else
                {
                    curDrgee = 0 - RotDrgee;
                }
            }
            transform.LookAt(m_oCenter.transform);
            transform.Translate(new Vector3(curDrgee*Time.deltaTime, 0f, 0f));
        }
        if(Input.GetMouseButtonUp(0))
        {
            Camera.main.transform.position = new Vector3(0, 1, -5);
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30, 0, 0));
        }
    }
}
