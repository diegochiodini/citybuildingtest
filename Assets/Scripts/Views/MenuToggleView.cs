using UnityEngine;
using Game.Abstractions;
using System;
using DG.Tweening;
using UnityEngine.Assertions;

public class MenuToggleView : AbstractToggleMenu
{
    [SerializeField]
    private RectTransform _scrollableArea;

    private bool _isOpen = false;
    public override bool IsOpen
    {
        get
        {
            return _isOpen;
        }
    }

    private void Awake()
    {
        Assert.IsNotNull(_scrollableArea);
    }

    public override void ToggleMenu()
    {
        transform.DOLocalMoveY(_isOpen ? 0f : _scrollableArea.rect.height, 0.15f);
        _isOpen = !_isOpen;
    }
}