using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChange
{
    private Image _imageToChange;
    private Sprite _newSpriteOnHover;
    private Sprite _originalSprite;

    public ButtonImageChange(Image imageToChange, Sprite newSpriteOnHover)
    {
        _imageToChange = imageToChange;
        _newSpriteOnHover = newSpriteOnHover;

        if (_imageToChange != null)
        {
            _originalSprite = _imageToChange.sprite;
        }
    }

    public void OnPointerEnter()
    {
        if (_imageToChange != null && _newSpriteOnHover != null)
        {
            _imageToChange.sprite = _newSpriteOnHover;
        }
    }

    public void OnPointerExit()
    {
        if (_imageToChange != null)
        {
            _imageToChange.sprite = _originalSprite;
        }
    }
}
