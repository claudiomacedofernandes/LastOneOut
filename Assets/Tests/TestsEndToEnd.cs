using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using LastOneOut;

[TestFixture]
public class TestsEndToEnd
{
    public int numExpectedItems = 20;
    public int minTurnMoves = 1;
    public int maxTurnMoves = 3;
    public float aiWaitTime = 0.1f;
    public AIDificulty aIDifficulty = AIDificulty.HARD;

    [OneTimeSetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Main");
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        SceneManager.UnloadSceneAsync("Main");
    }

    private IEnumerator WaitForGameState(GameState expectedState)
    {
        yield return new WaitUntil(() => GameManager.instance.gameState == expectedState);
    }

    [UnityTest, Timeout(10000)]
    public IEnumerator AAA_State_Loading_True()
    {
        yield return WaitForGameState(GameState.LOADING);
        GameManager.instance.minTurnMoves = minTurnMoves;
        GameManager.instance.maxTurnMoves = maxTurnMoves;
        GameManager.instance.aiWaitTime = aiWaitTime;
        GameManager.instance.aIDifficulty = aIDifficulty;
        Assert.True(GameManager.instance.gameState == GameState.LOADING);
    }

    [UnityTest, Timeout(10000)]
    public IEnumerator AAB_State_Setup_True()
    {
        yield return WaitForGameState(GameState.MENU);
        GameInfo gameInfo = new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON);
        GameManager.instance.StartNewGame(gameInfo);
        yield return WaitForGameState(GameState.SETUP);
        Assert.True(GameManager.instance.gameState == GameState.SETUP);
    }

    [UnityTest, Timeout(10000)]
    public IEnumerator AAC_BoarItems_Equals_Expected()
    {
        yield return WaitForGameState(GameState.SETUP);
        int numItems = GameObject.FindObjectsOfType<BoardItem>().Length;
        Assert.True(numItems == numExpectedItems);
    }

    [Test, Timeout(10000)]
    public void AAD_StartTurn_Called_True()
    {
        GameManager.instance.StartNewTurn(PlayerIndex.PLAYER_ONE);
        Assert.True(GameManager.instance.currentGameData.currentPlayer != null);
    }

    [UnityTest, Timeout(10000)]
    public IEnumerator AAE_State_End_True()
    {
        yield return WaitForGameState(GameState.END);
        Assert.True(GameManager.instance.gameState == GameState.END);
    }

    [UnityTest, Timeout(30000)]
    public IEnumerator AAF_BardItems_Equals_Zero()
    {
        yield return new WaitUntil(() => GameObject.FindObjectsOfType<BoardItem>().Length == 0);
        Assert.True(GameObject.FindObjectsOfType<BoardItem>().Length == 0);
    }
}
