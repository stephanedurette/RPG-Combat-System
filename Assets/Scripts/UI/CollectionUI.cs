using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionUI : MonoBehaviour
{
    [SerializeField] internal Collection representedCollection;
    // Start is called before the first frame update
    private void OnEnable()
    {
        representedCollection.OnValueChanged += RepresentedCollection_OnValueChanged;
        representedCollection.OnMaxValueChanged += RepresentedCollection_OnValueChanged;
    }

    private void RepresentedCollection_OnValueChanged(object sender, Collection.OnValueChangedEventArgs e)
    {
        UpdateCollectionUI();
    }

    private void Start()
    {
        UpdateCollectionUI();
    }

    public abstract void UpdateCollectionUI();


    private void OnDisable()
    {
        representedCollection.OnValueChanged -= RepresentedCollection_OnValueChanged;
        representedCollection.OnMaxValueChanged -= RepresentedCollection_OnValueChanged;
    }
}
