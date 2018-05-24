using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using LastOneOut;

[TestFixture]
public class EndToEndPlayMode
{
    public int numExpectedItems = 20;
    public int minMoves = 1;
    public int maxMoves = 3;

    //[OneTimeSetUp]
    //public void SetUp()
    //{
    //    Debug.Log("SetUp");
    //    SceneManager.LoadScene("Main");
    //}

    //[OneTimeTearDown]
    //public void TearDown()
    //{
    //    Debug.Log("TearDown");
    //    SceneManager.UnloadSceneAsync("Main");
    //}

    //private IEnumerator WaitForGameState(GameState expectedState)
    //{
    //    yield return new WaitUntil(() => GameManager.instance.gameState == expectedState);
    //}

    //[UnityTest, Timeout(10000)]
    //public IEnumerator State_Loading_True()
    //{
    //    Debug.Log("State_Loading_True");
    //    yield return WaitForGameState(GameState.LOADING);
    //    Debug.Log("gameState: " + GameManager.instance.gameState);
    //    Assert.True(GameManager.instance.gameState == GameState.LOADING);
    //}

    //[UnityTest, Timeout(10000)]
    //public IEnumerator State_Setup_True()
    //{
    //    Debug.Log("State_Setup_True");
    //    yield return WaitForGameState(GameState.MENU);
    //    Debug.Log("gameState: " + GameManager.instance.gameState);
    //    GameInfo gameInfo = new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON);
    //    GameManager.instance.StartNewGame(gameInfo);
    //    yield return WaitForGameState(GameState.SETUP);
    //    Assert.True(GameManager.instance.gameState == GameState.SETUP);
    //}

    //[UnityTest, Timeout(10000)]
    //public IEnumerator BoarItems_Equals_Expected()
    //{
    //    Debug.Log("BoarItems_Equals_Expected");
    //    yield return WaitForGameState(GameState.SETUP);
    //    Debug.Log("gameState: " + GameManager.instance.gameState);
    //    int numItems = GameObject.FindObjectsOfType<BoardItem>().Length;
    //    Assert.True(numItems == numExpectedItems);
    //}

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

    [UnityTest, Repeat(10)]
    public IEnumerator Scene_AIInput_ExistsAndIsUnique()
    {
        GameObject aiInputGO = new GameObject();
        aiInputGO.AddComponent<AIInput>();
        yield return new WaitForEndOfFrame();
        int numAIInput = GameObject.FindObjectsOfType<AIInput>().Length;
        Assert.True(numAIInput == 1);
    }
}
