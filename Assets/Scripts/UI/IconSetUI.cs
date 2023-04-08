using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSetUI : CollectionUI
{
    [SerializeField] private List<Image> images;

    [SerializeField] private Sprite filledImageSprite, emptyImageSprite;

    public override void UpdateCollectionUI()
    {
        for (int i = 0; i < images.Count; i++)
        {
            bool iconWithinCurrentValue = representedCollection.CurrentValue >= i + 1;
            bool iconWithinMaxValue = representedCollection.MaxValue >= i + 1;

            images[i].enabled = ((iconWithinMaxValue && emptyImageSprite != null) || iconWithinCurrentValue);
            images[i].sprite = iconWithinCurrentValue ? filledImageSprite : emptyImageSprite;
        }
    }
}
