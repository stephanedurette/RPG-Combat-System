using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float extendTime = 1f, retractTime = 1f, startDelay = 0f;
    [SerializeField] private Sprite retractedSprite;
    [SerializeField] private bool canRetract;

    private Hitbox hitbox;
    private Sprite extendedSprite;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponentInChildren<Hitbox>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        extendedSprite = spriteRenderer.sprite;

        SetSpikes(true);

        if (canRetract)
            StartCoroutine(StartDelayCoroutine());
    }

    private IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(RetractSpikesCoroutine());
    }

    private IEnumerator RetractSpikesCoroutine()
    {
        yield return new WaitForSeconds(extendTime);
        SetSpikes(false);
        StartCoroutine(ExtendSpikesCoroutine());
    }

    private IEnumerator ExtendSpikesCoroutine()
    {
        yield return new WaitForSeconds(retractTime);
        SetSpikes(true);
        StartCoroutine(RetractSpikesCoroutine());
    }

    void SetSpikes(bool on)
    {
        hitbox.gameObject.SetActive(on);
        spriteRenderer.sprite = on ? extendedSprite : retractedSprite;
    }


}
