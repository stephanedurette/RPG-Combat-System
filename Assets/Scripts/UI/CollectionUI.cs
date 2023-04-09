using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionUI : MonoBehaviour
{
    [SerializeField] internal CollectionSO representedCollection;
    // Start is called before the first frame update
    private void OnEnable()
    {
        representedCollection.OnValueChanged += UpdateCollectionUI;
        representedCollection.OnMaxValueChanged += UpdateCollectionUI;
    }

    private void Start()
    {
        UpdateCollectionUI();
    }

    public abstract void UpdateCollectionUI();


    private void OnDisable()
    {
        representedCollection.OnValueChanged -= UpdateCollectionUI;
        representedCollection.OnMaxValueChanged -= UpdateCollectionUI;
    }
}
