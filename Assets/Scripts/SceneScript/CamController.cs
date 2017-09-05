using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public GameObject m_oCenter;
    Vector3 m_vStartPoint;
    Dir m_eDir = Dir.Dir_Left;
    public float rotDegree = 10f;
    float m_fCurDeg = 0f;
	// Update is called once per frame
	void Update () {

        //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    UpdateTouchHandler();
        //}
        //else
        //{
        UpdateMouseHandler();
        //}
    }
    void UpdateMouseHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!(Input.mousePosition.x < Screen.width * 0.5f))
            {
                m_eDir = Dir.Dir_Right;
            }
            else
            {
                m_eDir = Dir.Dir_Left;
            }
            m_fCurDeg = 0f;
        }
        else if (Input.GetMouseButton(0))
        {
            if (m_fCurDeg == 0f)
            {
                if (m_eDir == Dir.Dir_Left)
                {
                    m_fCurDeg = rotDegree;
                }
                else
                {
                    m_fCurDeg = 0 - rotDegree;
                }
            }

            transform.LookAt(m_oCenter.transform);
            transform.Translate(new Vector3(m_fCurDeg, 0f, 0f));

        }
        else if (Input.GetMouseButtonUp(0))
        {
         
        }
    }

    void UpdateTouchHandler()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        if (!(touch.position.x < Screen.width * 0.5f))
                        {
                            m_eDir = Dir.Dir_Right;
                        }
                        else
                        {
                            m_eDir = Dir.Dir_Left;
                        }
                        m_fCurDeg = 0f;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        if (m_fCurDeg == 0f)
                        {
                            if (m_eDir == Dir.Dir_Left)
                            {
                                m_fCurDeg = rotDegree;
                            }
                            else
                            {
                                m_fCurDeg = 0 - rotDegree;
                            }
                        }
                        transform.LookAt(m_oCenter.transform);
                        transform.Translate(new Vector3(m_fCurDeg, 0f, 0f));
                        break;
                    }
            }

        }
    }


    


}
