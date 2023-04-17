using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportState : UnresponsiveState
{
    private Player player;
    private float velocity;
    private Vector2 endPosition;
    private float fadeTime = 1f;

    public PlayerTeleportState(object owner) : base(owner)
    {
        player = owner as Player;
    }

    public void Setup(float velocity, Vector2 endPosition)
    {
        this.velocity = velocity;
        this.endPosition = endPosition;
    }

    private IEnumerator FadeCoroutine(bool fadingIn)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            player.spriteRenderer.color = new Color(
                player.spriteRenderer.color.r, 
                player.spriteRenderer.color.g, 
                player.spriteRenderer.color.b, 
                Mathf.Lerp(0, 1, fadingIn ? elapsedTime / fadeTime : 1 - elapsedTime / fadeTime)
            );
            yield return null;
        };
        yield return 0;
    }

    private IEnumerator MoveToExitCoroutine()
    {
        float epsilon = 0.1f;

        Vector2 startingPosition = player.transform.position;
        Vector2 moveDirectionUnitVector = (endPosition - startingPosition).normalized;
        do
        {
            player.transform.position += (Vector3)moveDirectionUnitVector * velocity * Time.deltaTime;
            yield return null;
        } while ((endPosition - (Vector2)player.transform.position).magnitude > epsilon);

        yield return 0;
    }

    private IEnumerator TeleportCoroutine()
    {
        yield return player.StartCoroutine(FadeCoroutine(false));
        yield return player.StartCoroutine(MoveToExitCoroutine());
        yield return player.StartCoroutine(FadeCoroutine(true));
        player.stateMachine.SetState(player.responsiveState);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.StartCoroutine(TeleportCoroutine());
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
