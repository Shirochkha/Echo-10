using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChange
{
    private List<SpriteMenuChangeData> _spriteChangeData;

    public ButtonImageChange(List<SpriteMenuChangeData> spriteChangeData)
    {
        _spriteChangeData = spriteChangeData;
    }

    public void OnPointerEnter()
    {
        foreach (var spriteData in _spriteChangeData)
        {
            if (spriteData.ImageToChange != null && spriteData.HoverSprites != null)
            {
                spriteData.ImageToChange.sprite = spriteData.HoverSprites;
            }
        }
    }

    public void OnPointerExit()
    {
        foreach (var spriteData in _spriteChangeData)
        {
            if (spriteData.ImageToChange != null)
            {
                spriteData.ImageToChange.sprite = spriteData.OriginalSprites;
            }
        }
    }

}

[System.Serializable]
public struct SpriteMenuChangeData
{
    public Image ImageToChange;
    public Sprite OriginalSprites;
    public Sprite HoverSprites;
}
