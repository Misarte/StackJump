using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
	[SerializeField]
	private ScoreSystem scoreSystem = null;
	private void Awake()
	{
		if (scoreSystem == null)
			scoreSystem.GetComponentInParent<ScoreSystem>();

		scoreSystem.ScoreUpdated += UpdateScore;
	}
	public void UpdateScore(int score)
	{
		scoreText.text = score.ToString();
	}
}
