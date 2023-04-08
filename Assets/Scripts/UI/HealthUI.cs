using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : CollectionUI
{
    [SerializeField] private List<Image> heartImages;

    [SerializeField] private Sprite filledHeartSprite, emptyHeartSprite;

    public override void UpdateCollectionUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            var heartHealth = i + 1;

            heartImages[i].enabled = (representedCollection.MaxValue >= heartHealth);
            heartImages[i].sprite = representedCollection.CurrentValue >= heartHealth ? filledHeartSprite : emptyHeartSprite;
        }
    }
}
