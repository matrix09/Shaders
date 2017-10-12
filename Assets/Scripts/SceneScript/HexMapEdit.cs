using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HexMapEdit : MonoBehaviour {

    public HexGrid hexGrid;

    public Color[] Colors;

    Color ActiveColor;


    void Awake()
    {
        SelectColor(0);
    }

    void Update()
    {
        DetectTouchCoordinate();
    }

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
            hexGrid.TouchCell(hit.point, ActiveColor);
        }
    }

    public void SelectColor (int index)
    {
        ActiveColor = Colors[index];
    }

}
