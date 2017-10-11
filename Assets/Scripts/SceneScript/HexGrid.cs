using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HexGrid : MonoBehaviour
{


    #region 变量
    public int width = 6;

    public int height = 6;

    public HexCell CellPrefab;

    public Text cellLabelPrefab;

    HexCell[] cellArray;

    Canvas gridCanvas;

    HexMesh hexMesh;

    public HexCoordinate hexCoord;

    #endregion

    #region 系统接口
    void Start()
    {
        InitializeHexCells();
    }

    void OnDisable()
    {
        cellArray = null;
    }
    #endregion

    #region 初始化所有节点对象和位置
    void InitializeHexCells()
    {

        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        
        cellArray = new HexCell[width * height];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                Vector3 position;// = new Vector3(x * 10f, 0f, z * 10f);
                position.x = (x + z * 0.5f - z/2) * HexMetrices.InnerRadius * 2;
                position.z = z * HexMetrices.OuterRadius * 1.5f;
                position.y = 0f;
                HexCell cell = cellArray[i] = Instantiate<HexCell>(CellPrefab);
                cell.transform.SetParent(transform, false);
                cell.transform.localPosition = position;
                
                Text label = Instantiate<Text>(cellLabelPrefab);
                label.rectTransform.SetParent(gridCanvas.transform, false);
                label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
                label.text = (x - z/2).ToString() + "\n" + z.ToString();
            }
        }

        hexMesh.Triangulate(cellArray);

    }
    #endregion

}
