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

    private Vector3 _scaledCell;
    private Vector3 _scaledOffset;

    private void Awake()
    {
        Assert.IsNotNull(_raycaster, "You must specify a raycaster");
        _model = Locator.GetModel<SparseBuildingGridModel>();

        _scaledCell = new Vector3(
            (_cellSize.x * transform.localScale.x) / _model.Rows,
            0f,
            (_cellSize.y * transform.localScale.z) / _model.Columns);

        _scaledOffset = new Vector3(
            (_cellSize.x / 2f) * transform.localScale.x,
            0f,
            (_cellSize.y / 2f) * transform.localScale.z);
    }

    public void OnCLick(BaseEventData data)
    {
        _results.Clear();
        _raycaster.Raycast(data as PointerEventData, _results);
        foreach (var hit in _results)
        {
            Vector2 coordinates = GetGridCoordinate(hit.worldPosition);
            Debug.LogFormat("{0}, {1}", (int)coordinates.x, (int)coordinates.y);
        }
    }

    private Vector2 GetGridCoordinate(Vector3 worldPosition)
    {
        Vector2 coordinates = new Vector2();

        Vector3 localPosition = _scaledOffset - (transform.position - worldPosition);
        coordinates.x = _model.Rows - (localPosition.z / _scaledCell.z);
        coordinates.y = (localPosition.x / _scaledCell.x);
        return coordinates;
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