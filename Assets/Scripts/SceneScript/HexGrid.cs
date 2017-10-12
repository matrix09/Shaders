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

    public Color TouchColor;

    public Color DefaultColor;
    #endregion

    #region 系统接口
    void Start()
    {
        InitializeHexCells();
    }

    void Update()
    {
        DetectTouchCoordinate();
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
                Vector3 position;
                position.x = (x + z * 0.5f - z/2) * HexMetrices.InnerRadius * 2;
                position.z = z * HexMetrices.OuterRadius * 1.5f;
                position.y = 0f;
                HexCell cell = cellArray[i] = Instantiate<HexCell>(CellPrefab);
                cell.HexCellColor = DefaultColor;
                cell.transform.SetParent(transform, false);
                cell.transform.localPosition = position;

                Text label = Instantiate<Text>(cellLabelPrefab);
                label.rectTransform.SetParent(gridCanvas.transform, false);
                label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
                label.text = (x - z / 2).ToString() + "\n" + z.ToString();
            }
        }

        hexMesh.Triangulate(cellArray);

    }
    #endregion


    #region 检测点中的点坐标和对应的hex cell
    void DetectTouchCoordinate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 point)       //点中了某一个cell
    {
        Debug.Log(point);

        //将点中的cell对应的x, z index显示在inspector中

        //int z = (int)(point.z / (HexMetrices.OuterRadius * 1.5f));

        float x = point.x / (HexMetrices.InnerRadius * 2f);

        float y = -x;

        float offSet = point.z / (HexMetrices.OuterRadius * 3f);
        x -= offSet;
        y -= offSet;

       
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            //出现这个情况，是由于，在上面取整的时候，出现了同时缩小了两个，而通过取整后放大了第三个。 

            //解决方案，找到被放到的那个，然后利用x + y + z = 0的规则，通过另外两个算出正确的第三个。
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {

                if (dZ < dX)
                {
                    Debug.Log(123);
                }

                iZ = -iX - iY;
            }
        }

#if UNITY_EDITOR
        Debug.LogFormat("Point x = {0}, z = {1}", iX, iZ);
#endif
        hexCoord = HexCoordinate.GetOffSetCoordinate(iX, iZ);

        //获取点中的cell的index
        int index = hexCoord.X + hexCoord.Z * width + hexCoord.Z / 2;
        if (index < 0 || index > cellArray.Length)
        {
            Debug.LogErrorFormat("Index({0}) is illegal", index);
            return;
        }

        cellArray[index].HexCellColor = TouchColor;

        hexMesh.Triangulate(cellArray);
        
    }


    #endregion

}
