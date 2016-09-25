using Game.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class BuildingsGridView : MonoBehaviour
{
    [SerializeField]
    private Vector2 _cellSize;

    [SerializeField]
    private PhysicsRaycaster _raycaster;

    private List<RaycastResult> _results = new List<RaycastResult>();
    private SparseBuildingGridModel _model;

    private void Awake()
    {
        Assert.IsNotNull(_raycaster, "You must specify a raycaster");
        _model = Locator.GetModel<SparseBuildingGridModel>();
    }

    public void OnCLick(BaseEventData data)
    {
        _results.Clear();
        _raycaster.Raycast(data as PointerEventData, _results);
        foreach (var hit in _results)
        {
            Debug.Log(hit.worldPosition);
        }
    }

    private void OnDrawGizmos()
    {
        int rows = 20; int columns = 20;
        Gizmos.color = Color.red;
        Vector3 scaledSize = new Vector3(
            (_cellSize.x * transform.localScale.x) / rows,
            0f,
            (_cellSize.y * transform.localScale.z) / columns);

        Vector3 offset = new Vector3(
            (_cellSize.x / 2f) * transform.localScale.x, 
            0f,
            (_cellSize.y / 2f) * transform.localScale.z);

        for (int i = 0; i < rows + 1; i++) //rows
        {
            float zRow = i * scaledSize.z - offset.z;
            Vector3 startRow = new Vector3(-offset.x, 0f, zRow);
            Vector3 endRow = new Vector3(offset.x, 0f, zRow);
            Gizmos.DrawLine(startRow, endRow);


            for (int j = 0; j < columns + 1; j++) //columns
            {
                float xCol = j * scaledSize.x - offset.x;
                Vector3 startCol = new Vector3(xCol, 0f, -offset.z);
                Vector3 endCol = new Vector3(xCol, 0f,  offset.z);
                Gizmos.DrawLine(startCol, endCol);
            }
        }
    }
}