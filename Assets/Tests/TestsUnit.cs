using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;
using LastOneOut;

[TestFixture]
public class TestUnit
{
    public int numExpectedItems = 20;
    public int minTurnMoves = 1;
    public int maxTurnMoves = 3;
    public float aiWaitTime = 1.0f;
    public AIDificulty aIDifficulty = AIDificulty.HARD;

    [SetUp]
    public void SetUp()
    {
    }

    [TearDown]
    public void TearDown()
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject o in objects)
        {
            GameObject.Destroy(o.gameObject);
        }
    }

    private GameManager MockGameManager()
    {
        GameObject gameManagerGO = new GameObject();
        GameManager gameManager = gameManagerGO.AddComponent<GameManager>();
        return gameManager;
    }

    [Test]
    public void CheckPlayerMaxMoves_OnFail_ReturnsFalse()
    {
        GameManager gameManager = MockGameManager();
        gameManager.maxTurnMoves = maxTurnMoves;
        Assert.False(gameManager.CheckPlayerMaxMoves(maxTurnMoves + 1));
    }

    [Test]
    public void CheckPlayerMinMoves_OnFail_ReturnsFalse()
    {
        GameManager gameManager = MockGameManager();
        gameManager.minTurnMoves = minTurnMoves;
        Assert.False(gameManager.CheckPlayerMinMoves(minTurnMoves - 1));
    }

    [Test]
    public void CheckGameEnd_GameStateEnd_True()
    {
        GameManager gameManager = MockGameManager();
        gameManager.currentGameData = new GameData();
        gameManager.currentGameData.boardItems = new Dictionary<string, BoardItem>();
        gameManager.CheckGameEnd();
        Assert.True(gameManager.gameState == GameState.END);
    }

    [Test]
    public void PlayerStartTurn_Received_True()
    {
        GameManager gameManager = MockGameManager();
        gameManager.currentGameData = new GameData();
        gameManager.currentGameData.boardItems = new Dictionary<string, BoardItem>();
        IPlayer player = Substitute.For<IPlayer>();
        GameManager.instance.currentGameData.currentPlayer = player;
        GameManager.instance.currentGameData.currentPlayer.StartTurn();
        player.Received().StartTurn();
    }
}
