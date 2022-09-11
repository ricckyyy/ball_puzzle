using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnGridPointer : MonoBehaviour
{
    [SerializeField]
    private GameObject selectPlane;
    [SerializeField]
    private Tilemap tilemap;
    // メソッドを追加
    private void OnEnter()
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3Int gridPos = tilemap.WorldToCell(hit.point);
            Vector3 complementPos = new Vector3(tilemap.cellSize.x / 2, 0.01f, tilemap.cellSize.y / 2);
            Vector3 worldPos = tilemap.CellToWorld(gridPos) + complementPos;
            selectPlane.transform.position = worldPos;
        }
    }

    private void OnMouseOver()
    {
        OnEnter();
    }

    // メソッドを変更
    private void OnMouseEnter()
    {
        selectPlane.SetActive(true);
        OnEnter();
    }
}
