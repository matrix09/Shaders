using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    #region 变量
    List<Vector3> vertices;
    List<int> triangles;
    Mesh hexMesh;
    MeshCollider meshCol = null;
    #endregion

    #region 系统接口
    void OnEnable()
    {

        if (!gameObject.GetComponent<MeshCollider>() && null == meshCol)
            meshCol = gameObject.AddComponent<MeshCollider>();

        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    void OnDisable()
    {
        hexMesh = null;
        if (null != triangles)
        {
            triangles.Clear(); triangles = null;
        }

        if (null != vertices)
        {
            vertices.Clear(); vertices = null;
        }
    }

    #endregion

    #region 处理顶点信息
    public void Triangulate(HexCell[] cells)
    {
        
        if (null == cells)
        {
            Debug.LogError("Exception Error:  HexCell Array is Empty");
            return;
        }
        vertices.Clear();
        triangles.Clear();
       for (int i = 0; i < cells.Length; i++)
        //for (int i = 0; i < 1; i++)
        {
            Trangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCol.sharedMesh = hexMesh;

    }

    void Trangulate(HexCell cell)
    {
        Vector3 center = cell.transform.position;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                                    center, 
                                    center + HexMetrices.Corners[i], 
                                    center + HexMetrices.Corners[i + 1]
                              );
        }
    }

    void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        int index = vertices.Count;
        //添加顶点
        vertices.Add(v0);
        vertices.Add(v1);
        vertices.Add(v2);

        //添加三角形
        triangles.Add(index);
        triangles.Add(index + 1);
        triangles.Add(index + 2);

    }
    #endregion

}
