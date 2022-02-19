using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBricksSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerBehaviour player = null;
    [SerializeField]
    private BrickBehaviour brickBehaviour = null;
    [SerializeField]
    private float[] spawnPositionsX;

    private Coroutine spawnBrickCoroutine;
    private Queue<BrickBehaviour> bricks;

    public Queue<BrickBehaviour> Bricks => bricks;
    void Start()
    {
        bricks = new Queue<BrickBehaviour>();
        player.OnPlayerUpperFloor += SpawnBrick;
        
    }

    public void SpawnBrick(Vector3 playerPos)
	{
        int indexR = Random.Range(0, spawnPositionsX.Length);
        float currentSpawnPositionX = spawnPositionsX[indexR];
        float currentSpawnPositionY = playerPos.y + brickBehaviour.transform.localScale.y / 2;

        Vector3 brickSpawnPosition = new Vector3(currentSpawnPositionX, currentSpawnPositionY, 0);


        BrickBehaviour brick = Instantiate(brickBehaviour, brickSpawnPosition, Quaternion.identity);

		if (bricks.Count >= 2)
			bricks.Dequeue();

		bricks.Enqueue(brick);

		KillAllCoroutines();
        if (spawnBrickCoroutine == null)  
            spawnBrickCoroutine = StartCoroutine(brick.MoveBrick(playerPos));
        
    }

    private void KillAllCoroutines()
	{
        if(spawnBrickCoroutine != null)
            StopCoroutine(spawnBrickCoroutine);
        spawnBrickCoroutine = null;

    }
}
