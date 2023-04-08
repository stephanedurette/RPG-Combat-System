using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUI : CollectionUI
{
    [SerializeField] private TMPro.TMP_Text text;

    public override void UpdateCollectionUI()
    {
        text.text = representedCollection.CurrentValue.ToString();
    }
}
