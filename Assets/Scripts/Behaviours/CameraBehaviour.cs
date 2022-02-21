using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerBehaviour player = null;

    private Vector3 initPos;
    void Start()
    {
        initPos = transform.position;
        player.OnPlayerUpperFloor += MoveCamera;
    }

    //Adjust Camera position (height) to be following player 
    private void MoveCamera(Vector3 playerPosition)
	{
        Vector3 newCameraPos = new Vector3(initPos.x, playerPosition.y, initPos.z);
         transform.position = newCameraPos;

	}
}
