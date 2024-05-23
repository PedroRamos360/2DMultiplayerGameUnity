using System.Collections;
using UnityEngine;

public class TimeoutManager : MonoBehaviour
{
    public void SetTimeout(System.Action action, float delay)
    {
        StartCoroutine(TimeoutCoroutine(action, delay));
    }

    private IEnumerator TimeoutCoroutine(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}