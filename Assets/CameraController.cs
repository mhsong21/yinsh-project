using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Camera;
    public float animationTime = 1.5f;

    private Vector3 basePos = new Vector3(20f, 5f, 0f);
    private Vector3 playPos = new Vector3(0f, 5f, 0f);
    private bool onPlay = false;

    public void OnReset()
    {
        Camera.transform.position = basePos;
    }

    public void OnStart()
    {
        if (!onPlay)
            StartCoroutine(TurnCamera());
    }
    
    private IEnumerator TurnCamera()
    {
        onPlay = true;
        float totalDelta = 0f;
        while (true)
        {
            totalDelta += Time.deltaTime;
            if (totalDelta < animationTime)
            {
                Camera.transform.position = basePos + (playPos - basePos) * totalDelta / animationTime;
            }
            else
            {
                Camera.transform.position = playPos;
                onPlay = false;
                break;
            }
            yield return null;
        }
            
    }
}
