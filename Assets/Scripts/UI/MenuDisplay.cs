using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField]
    private Button playButton = null;

    private LevelBricksSystem bricksSystem;

    [Inject]
    private void Init(LevelBricksSystem bricksSystem)
    {
        this.bricksSystem = bricksSystem;
    }
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    private void StartGame()
    {
        bricksSystem.SpawnBrick(new Vector3(0, 0.5f, 0));
        this.gameObject.SetActive(false);
    }
}
