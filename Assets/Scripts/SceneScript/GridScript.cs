using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class GridScript : BasicScene {

    MeshFilter mf;
    MeshRenderer mr;

    public int m_nXSize;//横向的顶点个数

    public int m_nYSize;//纵向顶点个数

    Vector3[] m_vVertex;

    GameObject[] m_oSpheres;

    int[] m_nTriangles;

    public Transform m_tParent;

	// Use this for initialization
	void Awake () {
      
		mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
        //GenerateMesh();
        StartCoroutine(Generating ());
	}

    IEnumerator Generating()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        m_oSpheres = new GameObject[((m_nXSize + 1) * (m_nYSize + 1))];
        Mesh mesh = null;
        mesh = mf.mesh = new Mesh();
        mesh.name = "pro Grid";


        m_nTriangles = new int[6 * m_nXSize * m_nYSize];

        for (int y = 0, index = 0, nValue = 0; y < m_nYSize; y++, nValue++)
        {
            for (int x = 0; x < m_nXSize; x++, index += 6, nValue++)
            {
                m_nTriangles[index] = nValue;
                m_nTriangles[index + 1] = nValue + m_nXSize + 1;
                m_nTriangles[index + 2] = m_nTriangles[index] + 1;
                m_nTriangles[index + 3] = m_nTriangles[index + 2];
                m_nTriangles[index + 4] = m_nTriangles[index + 1];
                m_nTriangles[index + 5] = m_nTriangles[index + 1] + 1;

            }
        }

        m_vVertex = new Vector3[((m_nXSize + 1) * (m_nYSize + 1))];
        Vector2[] uv = new Vector2[m_vVertex.Length];
        Vector4[] tangents = new Vector4[m_vVertex.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, 1f);
        for (int i = 0, y = 0; y < m_nYSize + 1; y++)
        {
            for (int x = 0; x < m_nXSize + 1; x++, i++)
            {
                //m_vVertex[]
                m_oSpheres[i] = Instantiate(Resources.Load("Sphere"), new Vector2(x - m_nXSize * 0.5f, y - m_nYSize * 0.5f), Quaternion.identity) as GameObject;
                m_oSpheres[i].transform.parent = m_tParent;
                m_vVertex[i] = (new Vector2(x - m_nXSize * 0.5f, y - m_nYSize * 0.5f));
                uv[i] = new Vector2(
                 (float)x / m_nXSize,
                 (float)y / m_nYSize
                 );
                yield return wait;

                tangents[i] = tangent;
                mesh.vertices = m_vVertex;
                mesh.triangles = m_nTriangles;
                mesh.tangents = tangents;
                mesh.uv = uv;
                mesh.RecalculateNormals();
            }
        }
    }

    void GenerateMesh()
    {

        Mesh mesh = null;

        #region create mesh
        mesh = mf.mesh = new Mesh();
        mesh.name = "pro Grid";
        #endregion

        #region 初始化三角形信息 -> 原则和相机朝向相反
        //m_nTriangles = new int[6];
        //m_nTriangles[0] = 0;
        //m_nTriangles[1] = m_nXSize + 1;
        //m_nTriangles[2] = m_nTriangles[0] + 1;
        //m_nTriangles[3] = m_nTriangles[2];
        //m_nTriangles[4] = m_nTriangles[1];
        //m_nTriangles[5] = m_nTriangles[1] + 1;

        m_nTriangles = new int[6 * m_nXSize * m_nYSize];

        for (int y = 0, index = 0, nValue = 0; y < m_nYSize; y++, nValue++)
        {
            for (int x = 0; x < m_nXSize; x++, index += 6, nValue++)
            {
                m_nTriangles[index] = nValue;
                m_nTriangles[index + 1] = nValue + m_nXSize + 1;
                m_nTriangles[index + 2] = m_nTriangles[index] + 1;
                m_nTriangles[index + 3] = m_nTriangles[index + 2];
                m_nTriangles[index + 4] = m_nTriangles[index + 1];
                m_nTriangles[index + 5] = m_nTriangles[index + 1] + 1;

            }
        }
        #endregion

        #region create vertex and uv
        m_oSpheres = new GameObject[((m_nXSize + 1) * (m_nYSize + 1))];
        m_vVertex = new Vector3[((m_nXSize + 1) * (m_nYSize + 1))];
        Vector2[] uv = new Vector2[m_vVertex.Length];
        Vector4[] tangents = new Vector4[m_vVertex.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, 1f);
        for (int i = 0, y = 0; y < m_nYSize + 1; y++)
        {
            for (int x = 0; x < m_nXSize + 1; x++, i++)
            {
                //m_vVertex[]
                m_oSpheres[i] = Instantiate(Resources.Load("Sphere"), new Vector2(x - m_nXSize * 0.5f, y - m_nYSize * 0.5f), Quaternion.identity) as GameObject;
                m_oSpheres[i].transform.parent = m_tParent;
                m_vVertex[i] = (new Vector2(x - m_nXSize * 0.5f, y - m_nYSize*0.5f));
                uv[i] = new Vector2(
                 (float)x / m_nXSize,
                 (float)y / m_nYSize
                 );
                
                tangents[i] = tangent;
            }
        }
        #endregion

        #region 初始化mesh文件信息
        mesh.vertices = m_vVertex;
        mesh.triangles = m_nTriangles;
        mesh.tangents = tangents;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        #endregion

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (null == m_vVertex)
            return;
        for (int i = 0; i < m_vVertex.Length; i++)
        {
            Gizmos.DrawSphere((m_vVertex[i]), 0.2f);
        }
    }

    protected virtual void OnGUI()
    {
        base.OnGUI();
        GUI.TextArea(new Rect(Screen.width * 0.5f - 150, 20, 300, 40), "Press Screen on LeftSide or RightSide, then Camera will rotate");
    
    }


}
