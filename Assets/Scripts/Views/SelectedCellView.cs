using UnityEngine;
using Game.Abstractions;
using Game.Models;
using System;

public class SelectedCellView : AbstractSelectionCell<BuildingModel>
{
    [SerializeField]
    private float _cellSize;

    public override BuildingModel Model
    {
        get
        {
            return ModelCache;
        }

        set
        {
            if (ModelCache != value)
            {
                ModelCache = value;
                if (ModelCache == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                    AdjustGraphics();
                }
            }
        }
    }

    private void Awake()
    {
        AdjustGraphics();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void AdjustGraphics()
    {
        float xUnit = 1f;
        float zUnit = 1f;

        if (Model != null)
        {
            xUnit = Model.Width;
            zUnit = Model.Height;
        }
        //Bear in mind that the quad is rotated along the X
        Vector3 scale = new Vector3(_cellSize * xUnit, _cellSize * zUnit, 1f);
        transform.localScale = scale;
    }
}