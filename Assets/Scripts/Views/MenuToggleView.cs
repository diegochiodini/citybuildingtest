using UnityEngine;
using Game.Abstractions;
using DG.Tweening;
using UnityEngine.Assertions;

public class MenuToggleView : MonoBehaviour, IToggleMenuView
{
    [SerializeField]
    private RectTransform _contentArea;

    private bool _isOpen = false;
    public bool IsOpen
    {
        get
        {
            return _isOpen;
        }
    }

    private void Awake()
    {
        Assert.IsNotNull(_contentArea);
    }

    public void ToggleMenu()
    {
        transform.DOLocalMoveY(_isOpen ? 0f : _contentArea.rect.height, 0.15f);
        _isOpen = !_isOpen;
    }
}