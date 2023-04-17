using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

    public void EnableFollowCameraSnapping(bool enabled)
    {
        playerFollowCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = enabled ? 0 : 1;
        playerFollowCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = enabled ? 0 : 1;
        playerFollowCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = enabled ? 0 : 1;
    }
}
