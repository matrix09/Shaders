using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : BasicScene {
    public Transform m_tCube;
    public Transform Parent;
    public int m_nGridSolution;

    Transform[] m_tArray;
    Matrix4x4 m_MatrixTrans;
    void Awake()
    {
        InitializeScripts();
        GenerateMatrices();
    }

    void GenerateMatrices()
    {
        m_tArray = new Transform[m_nGridSolution * m_nGridSolution * m_nGridSolution];

        transformations = new List<MatrixTransformation>();

        for (int k = 0, index = 0; k < m_nGridSolution; k++)
        {
            for (int j = 0; j < m_nGridSolution; j++)
            {
                for (int i = 0; i < m_nGridSolution; i++, index++)
                {
                    m_tArray[index] = CreateSingleMatrix(i, j, k);
                }
            }
        }
    }

    Transform CreateSingleMatrix(int i, int j, int k)
    {
        Transform t = Instantiate<Transform>(m_tCube);

        t.parent = Parent;
        t.localPosition = GetCoordinate(i, j, k);

        t.GetComponent<MeshRenderer>().materials[0].color = new Color(
                (float)i / m_nGridSolution,
                (float)j / m_nGridSolution,
                (float)k / m_nGridSolution
            );

        return t;
    }

    Vector3 GetCoordinate(int i, int j, int k)
    {
        return new Vector3(
                (i - m_nGridSolution * 0.5f),
                (j - m_nGridSolution * 0.5f),
                (k - m_nGridSolution * 0.5f)
            );
    }

	void Update () {

        //更新变换矩阵
        UpdateMatrix();

        for (int z = 0, i = 0; z < m_nGridSolution; z++)
        {
            for (int y = 0; y < m_nGridSolution; y++)
            {
                for (int x = 0; x < m_nGridSolution; x++, i++)
                {
                    m_tArray[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
	}

    Vector3 TransformPoint(int x, int y, int z)
    {
        Vector3 coordinate = GetCoordinate(x, y, z);

        return m_MatrixTrans.MultiplyPoint(coordinate);

    }

    List<MatrixTransformation> transformations;

    void UpdateMatrix()
    {

        GetComponents<MatrixTransformation>(transformations);

        if (transformations.Count > 0)
        {
            m_MatrixTrans = transformations[0].Matrix;
            for (int i = 1; i < transformations.Count; i++)
            {
                m_MatrixTrans = transformations[i].Matrix * m_MatrixTrans;
            }
        }

    }

    PositionTransformation pt;
    RotationTransformation rt;
    void InitializeScripts()
    {
        pt = gameObject.GetComponent<PositionTransformation>();
        rt = gameObject.GetComponent<RotationTransformation>();
    }

    float vSliderValue = 0.0F;
    protected override void OnGUI()
    {
        base.OnGUI();

        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width ) * 0.3f, 25, 130, 30), pt.position.x, 0.0F, 10.0F);
        if (vSliderValue != pt.position.x)
        {
            pt.position = new Vector3(vSliderValue, pt.position.y, pt.position.z);
        }

        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width ) * 0.3f, 55, 130, 30), pt.position.y, 0.0F, 10.0F);
        if (vSliderValue != pt.position.y)
        {
            pt.position = new Vector3(pt.position.x, vSliderValue, pt.position.z);
        }

        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width ) * 0.3f, 85, 130, 30), pt.position.z, 0.0F, 10.0F);
        if (vSliderValue != pt.position.z)
        {
            pt.position = new Vector3(pt.position.x, pt.position.y, vSliderValue);
        }


        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width) * 0.7f, 25, 130, 30), rt.rotation.x, 0.0F, 1800.0F);
        if (vSliderValue != rt.rotation.x)
        {
            rt.rotation = new Vector3(vSliderValue, rt.rotation.y, rt.rotation.z);
        }

        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width) * 0.7f, 55, 130, 30), rt.rotation.y, 0.0F, 180.0F);
        if (vSliderValue != rt.rotation.y)
        {
            rt.rotation = new Vector3(rt.rotation.x, vSliderValue, rt.rotation.z);
        }

        vSliderValue = GUI.HorizontalSlider(new Rect((Screen.width) * 0.7f, 85, 130, 30), rt.rotation.z, 0.0F, 180.0F);
        if (vSliderValue != rt.rotation.z)
        {
            rt.rotation = new Vector3(rt.rotation.x, rt.rotation.y, vSliderValue);
        }
    



    }
}
