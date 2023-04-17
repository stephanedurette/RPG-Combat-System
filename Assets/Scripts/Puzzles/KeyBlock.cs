using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBlock : MonoBehaviour
{
    [SerializeField] CollectionSO checkCollection;
    [SerializeField] private int requiredAmount = 1;
    [SerializeField] private SpriteRenderer keyImage;
    [SerializeField] private Image UIKeyImage;
    [SerializeField] private TMPro.TMP_Text UIText;

    AreaCollider areaCollider;
    Animator animator;

    private void Start()
    {
        keyImage.sprite = checkCollection.collectionType.Image;
        UIKeyImage.sprite = checkCollection.collectionType.Image;
        UIText.text = requiredAmount.ToString("00");
    }

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        areaCollider = GetComponentInChildren<AreaCollider>();
        animator = GetComponent<Animator>();

        areaCollider.OnAreaColliderHit += HandlePlayerCollision;
    }

    private void OnDisable()
    {
        areaCollider.OnAreaColliderHit -= HandlePlayerCollision;
    }

    void HandlePlayerCollision(GameObject other)
    {
        if (checkCollection.CurrentValue >= requiredAmount)
        {
            checkCollection.CurrentValue -= requiredAmount;
            animator.SetTrigger("Destroy");
        }
    }
}
