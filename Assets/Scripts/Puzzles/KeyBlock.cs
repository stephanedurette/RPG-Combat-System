using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : MonoBehaviour
{
    [SerializeField] CollectionSO checkCollection;
    [SerializeField] private int requiredAmount = 1;

    AreaCollider areaCollider;
    Animator animator;

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

    void HandlePlayerCollision()
    {
        if (checkCollection.CurrentValue >= requiredAmount)
        {
            checkCollection.CurrentValue -= requiredAmount;
            animator.SetTrigger("Destroy");
        }
    }
}
