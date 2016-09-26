using UnityEngine;
using Game.Abstractions;
using Game.Models;
using System;

public class SelectedCellView : AbstractSelectionCell<BuildingModel>
{
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
                }
            }
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}