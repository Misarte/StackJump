using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[System.Serializable]
public class PlayerData
{
    public int score;

}
public class ScoreSystem : MonoBehaviour
{
    public Action<int> ScoreUpdated;
    
    [SerializeField]
    private PlayerBehaviour player = null;
    [SerializeField]
    private Button exitButton = null;

    private PlayerData playerData;
    private string path = "/PlayerScore.json";

    private LevelBricksSystem bricksSystem;

    private int savedScore;
    private int score = 0;

    private int bonusCounter;

    [Inject]
    private void Init(LevelBricksSystem bricksSystem)
	{
        this.bricksSystem = bricksSystem;
	}
    void Start()
    {
        bonusCounter = 1;
        player.OnPlayerUpperFloor += UpdateScore;
        player.BonusActivate += AddBonusScore;
        exitButton.onClick.AddListener(SaveScore);
        score = LoadScore();
    }

    private int LoadScore()
	{
        string fullPath = Application.persistentDataPath + path;
        playerData = new PlayerData();

        try
        {
			if (!File.Exists(fullPath))
                SaveScore();

			string json = File.ReadAllText(fullPath);

            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        catch
        {
            Debug.LogWarning("Error loading file.");
            throw;
        }
        savedScore = playerData.score;
        return playerData.score;
    }

    public void SaveScore()
    {

        if (score < savedScore)
            return;


        string fullPath = Application.persistentDataPath + path;
        PlayerData currentPlayeryData = new PlayerData() ;
        playerData.score = score;

        string jsonString = null;
        
        jsonString = JsonUtility.ToJson(currentPlayeryData);

        if (jsonString != null)
            File.WriteAllText(fullPath, jsonString);

        QuitGame();
    }

    private void UpdateScore(Vector3 playerPosition)
    {
        CheckBricks();
        score += bonusCounter;
        ScoreUpdated?.Invoke(score);
    }

    private void CheckBricks()
	{
        if (bricksSystem.Bricks.Count <= 1)
            return;

        List<BrickBehaviour> bricksList = new List<BrickBehaviour>(bricksSystem.Bricks.Count);
        BrickBehaviour lastBrick = bricksSystem.Bricks.Peek();
        BrickBehaviour previousBrick;
       
        foreach (var brick in bricksSystem.Bricks)
            bricksList.Add(brick);

        previousBrick = bricksList[1];

        if (Mathf.Abs(lastBrick.transform.position.x - previousBrick.transform.position.x) < 0.1)
            AddBonusScore();
        else
            ResetBonusCounter();

    }
    private void AddBonusScore()
    {
        bonusCounter += 1;
    }
    private void ResetBonusCounter()
    {
        bonusCounter = 1;
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
