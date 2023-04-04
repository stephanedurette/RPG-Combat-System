using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    Action onElapsed;
    MonoBehaviour coroutineHolder;
    Coroutine coroutine;

    public Timer(float timeInSeconds, Action onElapsed, MonoBehaviour coroutineHolder)
    {
        this.coroutineHolder = coroutineHolder;

        coroutine = coroutineHolder.StartCoroutine(TimerCoroutine(timeInSeconds, onElapsed));
    }

    private IEnumerator TimerCoroutine(float timeInSeconds, Action onElapsed)
    {
        yield return new WaitForSeconds(timeInSeconds);
        onElapsed?.Invoke();
    }

    public void Stop()
    {
        coroutineHolder.StopCoroutine(coroutine);
    }
}
