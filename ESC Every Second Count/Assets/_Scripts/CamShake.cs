using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 origPos = transform.localPosition;

        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-.5f, .5f) * magnitude;
            float y = Random.Range(-.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(x, y, origPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }

}
