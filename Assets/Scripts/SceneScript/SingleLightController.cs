using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLightController : BasicScene {

    public Transform[] m_tArray;
    public Texture m_tTex;
    public Light m_Light;
    Material[] m_arrayMat;
    enum eSingleLight
    {
        eNormal = 0,
        eDiffuse,
        eDiffuseAlbedo,
        eSpecular,
        eDiffuseSpecular,
        eSingleLightSize,
    }
    const float CWIDTH = 150f;
    const float CHEIGHT = 30f;

    class  LightDataStore{
        public Vector3 vLightColor;
        public Vector3 vSurfaceColor;
        public Vector3 vLightDir;
        public float fSpecularSmooth;
        public Vector3 vSpecularColor;
        public float fMetallic;
        public Vector3 vAlbedoColor;
    }

    LightDataStore m_LightDataStore;

    eSingleLight m_eSingleLight = eSingleLight.eNormal;
    string[] m_str;
    void Awake()
    {
        m_LightDataStore = new LightDataStore();

        m_str = new string[(int)eSingleLight.eSingleLightSize];
        m_str[0] = "eNormal";
        m_str[1] = "eDiffuse";
        m_str[2] = "eAlbedoDiffuse";
        m_str[3] = "eSpecular";
        m_str[4] = "eDiffuseSpecular";
        m_arrayMat = new Material[3];
        m_LightDataStore.vLightDir = m_Light.transform.rotation.eulerAngles;
    }

    void SetShader(eSingleLight type)
    {
        string name = "Custom/Normals";

        m_eSingleLight = type;
        switch (type)
        {
            case eSingleLight.eNormal:
                {
                    break;
                }
            case eSingleLight.eDiffuse:
                {
                    name = "Custom/MyDiffuse";
                    //name = "Unity Shaders Book/Chapter 6/Diffuse Pixel-Level";
                    break;
                }
            case eSingleLight.eDiffuseAlbedo:
                {
                    name = "Custom/MyDiffuseAlbedo";
                    break;
                }
            case eSingleLight.eSpecular:
                {
                    name = "Custom/MySpecular";
                    //name = "UnityShadersBook/Chapter6/SpecularPixel-Level";
                    break;
                }
            case eSingleLight.eDiffuseSpecular:
                {
                    name = "Custom/MyDiffuseAndSpecular";
                    break;
                }
        }

        //m_mCurMat = m_tArray[0].GetComponent<MeshRenderer>().materials[0];
            
        for (int i = 0; i < m_tArray.Length; i++)
        {

            m_tArray[i].GetComponent<MeshRenderer>().materials[0].shader = Shader.Find(name);
            m_arrayMat[i] = m_tArray[i].GetComponent<MeshRenderer>().materials[0];
            if (type == eSingleLight.eDiffuseAlbedo || type == eSingleLight.eDiffuseSpecular)
            {
                m_tArray[i].GetComponent<MeshRenderer>().materials[0].mainTexture = m_tTex;
            }
        }
    }

    Vector3 VectorField(Rect r, Vector3 v, float left = 0f,  float right = 1f)
    {
        float x = GUI.HorizontalSlider(
                new Rect (r.x, r.y, r.width, r.height),
                v.x, left, right
                );

        float y = GUI.HorizontalSlider(
                new Rect(r.x, r.y + 1.1f*r.height, r.width, r.height),
                v.y, left, right
                );

        float z = GUI.HorizontalSlider(
                new Rect(r.x, r.y + 2.2f * r.height, r.width, r.height),
                v.z, left, right
                );

        return new Vector3(x, y, z);
    }

    void SetColor(Vector3 lightcolor, Vector3 objectcolor)
    {

        m_LightDataStore.vLightColor = lightcolor;
        //光照颜色
        m_Light.color = new Color(
                m_LightDataStore.vLightColor.x,
                m_LightDataStore.vLightColor.y,
                m_LightDataStore.vLightColor.z
            );

        m_LightDataStore.vSurfaceColor = objectcolor;
        //物体表面颜色
        for (int i = 0; i < 3; i++)
        {
            m_arrayMat[i].SetColor("_TintColor", new Color(m_LightDataStore.vSurfaceColor.x,
                                                                                    m_LightDataStore.vSurfaceColor.y,
                                                                                     m_LightDataStore.vSurfaceColor.z
                                                                                    ));
        }

    }

    protected override void OnGUI()
    {
        base.OnGUI();

        for (int i = 0; i < (int)eSingleLight.eSingleLightSize; i++)
        {
            if (GUI.Button(new Rect((Screen.width - 100) * 0.5f, 30 + i * 40, CWIDTH, CHEIGHT), m_str[i]))
            {
                SetShader((eSingleLight)i);
            }
        }

        //如果是normal状态，不处理后续逻辑
        if (m_eSingleLight == eSingleLight.eNormal)
            return;

        //获取表面光照颜色
        Color surfacecolor = m_arrayMat[0].GetColor("_TintColor");

        //light color
        m_LightDataStore.vLightColor = new Vector3(m_Light.color.r, m_Light.color.g, m_Light.color.b);
        Vector3 newlightcolor = VectorField(new Rect(0, (Screen.height - CHEIGHT) * 0.2f, CWIDTH, CHEIGHT), m_LightDataStore.vLightColor);

        //object's surface color
        m_LightDataStore.vSurfaceColor = new Vector3(surfacecolor.r, surfacecolor.g, surfacecolor.b);
        Vector3 newobjectcolor = VectorField(new Rect(0, (Screen.height - CHEIGHT) * 0.5f, CWIDTH, CHEIGHT), m_LightDataStore.vSurfaceColor);

        if (newlightcolor != m_LightDataStore.vLightColor || newobjectcolor != m_LightDataStore.vSurfaceColor)
        {
            //设置光照颜色，设置物体表面颜色
            SetColor(newlightcolor, newobjectcolor);
        }

        #region light direction x - y - z
        m_LightDataStore.vLightDir = new Vector3(m_Light.transform.rotation.eulerAngles.x, m_Light.transform.rotation.eulerAngles.y, m_Light.transform.rotation.eulerAngles.z);
        Vector3 newlightdir = VectorField(new Rect(0, (Screen.height - CHEIGHT) * 0.7f, CWIDTH, CHEIGHT), m_LightDataStore.vLightDir, 1f, 89f);
       
        //光照 : x轴方向, y轴方向, z轴方向
        if (newlightdir != m_LightDataStore.vLightDir)
        {
            //重新赋值光照角度
            m_LightDataStore.vLightDir = newlightdir;
            //光照反向
            m_Light.transform.rotation = Quaternion.Euler(
                m_LightDataStore.vLightDir.x,
                m_LightDataStore.vLightDir.y,
                m_LightDataStore.vLightDir.z
                );
        }
        #endregion

        #region specular color
        switch (m_eSingleLight)
        {
            case eSingleLight.eSpecular:
            case eSingleLight.eDiffuseSpecular:
                {
                    float _Smoothness = m_arrayMat[0].GetFloat("_Smoothness");
                    m_LightDataStore.fSpecularSmooth = _Smoothness;
                    //Vector3 newspecularcolor = VectorField(new Rect((Screen.width - CWIDTH), (Screen.height - CHEIGHT) * 0.2f, CWIDTH, CHEIGHT), m_LightDataStore.vSpecularColor);
                    float newsmoothness = GUI.HorizontalSlider(new Rect((Screen.width - CWIDTH), (Screen.height - CHEIGHT) * 0.2f, CWIDTH, CHEIGHT), m_LightDataStore.fSpecularSmooth, 0f, 1f);
                    if (newsmoothness != m_LightDataStore.fSpecularSmooth)
                    {

                        m_LightDataStore.fSpecularSmooth = newsmoothness;

                        //物体高光颜色
                        for (int i = 0; i < 3; i++)
                        {
                            m_arrayMat[i].SetFloat("_Smoothness", m_LightDataStore.fSpecularSmooth);
                        }

                    }

                    if (m_eSingleLight == eSingleLight.eDiffuseSpecular)
                    {
                        float _Metallic = m_arrayMat[0].GetFloat("_Metallic");
                        m_LightDataStore.fMetallic = _Metallic;
                        //Vector3 newspecularcolor = VectorField(new Rect((Screen.width - CWIDTH), (Screen.height - CHEIGHT) * 0.2f, CWIDTH, CHEIGHT), m_LightDataStore.vSpecularColor);
                        float newmetallic = GUI.HorizontalSlider(new Rect((Screen.width - CWIDTH), (Screen.height - CHEIGHT) * 0.5f, CWIDTH, CHEIGHT), m_LightDataStore.fMetallic, 0f, 1f);
                        if (newmetallic != m_LightDataStore.fMetallic)
                        {

                            m_LightDataStore.fMetallic = newmetallic;

                            //物体高光颜色
                            for (int i = 0; i < 3; i++)
                            {
                                m_arrayMat[i].SetFloat("_Metallic", m_LightDataStore.fMetallic);
                            }

                        }
                    }

                    break;
                }
        }
        #endregion
    }

}
