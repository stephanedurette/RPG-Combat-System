using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    [SerializeField] private AreaCollider areaCollider;
    [SerializeField] private Transform exitPosition;
    [SerializeField] private PortalMode portalMode;
    [SerializeField] private float shortRangePlayerMoveSpeed = 20f;

    enum PortalMode
    {
        ShortRange,
        LongRange
    }

    private void OnEnable()
    {
        areaCollider.OnAreaColliderHit += OnAreaColliderHit;
    }

    private void OnDisable()
    {
        areaCollider.OnAreaColliderHit -= OnAreaColliderHit;
    }

    void OnAreaColliderHit(GameObject other)
    {
        Player player;
        if (!other.TryGetComponent(out player)) return;

        foreach (var s in FindObjectsOfType<EnemySlime>())
            s.ReturnToPatrolState();

        switch (portalMode)
        {
            case PortalMode.LongRange:
                StartCoroutine(LongTeleportCoroutine(player));
                break;
            case PortalMode.ShortRange:
                ShortTeleport(player);
                break;
            default:
                break;
        }

    }

    void ShortTeleport(Player player)
    {
        player.Teleport(shortRangePlayerMoveSpeed, exitPosition.position);
    }

    IEnumerator LongTeleportCoroutine(Player player)
    {
        player.SetResponsive(false);
        
        yield return StartCoroutine(Singleton.Instance.ScreenFadeManager.FadeAroundCoroutine(() => {
            Singleton.Instance.CinemachineManager.EnableFollowCameraSnapping(true);

            player.transform.position = exitPosition.position;
        }));

        Singleton.Instance.CinemachineManager.EnableFollowCameraSnapping(false);
        player.SetResponsive(true);
    }
}
