using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f;
   
    public IEnumerator MoveBrick(Vector3 targetPosition)
	{
        float timeElapsed = 0;
        float distance = Vector3.Distance(transform.position, targetPosition);
		float lerpDuration = distance / movementSpeed;

		while (timeElapsed < lerpDuration)
		{
			float t = timeElapsed / lerpDuration;

            transform.position = Vector3.Lerp(transform.position, targetPosition, t);

			timeElapsed += Time.deltaTime;

			yield return null;
		}
	}
}
