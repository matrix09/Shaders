using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    #region 变量
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> Colors;
    Mesh hexMesh;
    MeshCollider meshCol = null;
    MeshRenderer meshRender;
    public Material VertexColor;
    #endregion

    #region 系统接口
    void OnEnable()
    {

        if (!gameObject.GetComponent<MeshCollider>() && null == meshCol)
            meshCol = gameObject.AddComponent<MeshCollider>();

        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshRender = GetComponent<MeshRenderer>();
        meshRender.sharedMaterial = VertexColor;
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        Colors = new List<Color>();

        //设置mesh renderer的material


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

        if(null != Colors) {
            Colors.Clear();
            Colors = null;
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
        Colors.Clear();
       for (int i = 0; i < cells.Length; i++)
        //for (int i = 0; i < 1; i++)
        {
            Trangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors = Colors.ToArray();
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
            AddColor(cell.HexCellColor);
        }
    }

    void AddColor(Color color)
    {
        Colors.Add(color);
        Colors.Add(color);
        Colors.Add(color);

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
