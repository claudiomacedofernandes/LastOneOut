using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;
using LastOneOut;

[TestFixture]
public class TestPlayMode
{
    public int numExpectedItems = 20;
    public int minMoves = 1;
    public int maxMoves = 3;

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

    private UIManager MockUIManager()
    {
        GameObject uiManagerGO = new GameObject();
        UIManager uiManager = uiManagerGO.AddComponent<UIManager>();
        return uiManager;
    }

    private BoardManager MockBoardManager()
    {
        GameObject boardManagerGO = new GameObject();
        BoardManager boardManager = boardManagerGO.AddComponent<BoardManager>();
        return boardManager;
    }

    private BoardItem CreateBoardItem()
    {
        GameObject boardItemGO = Resources.Load<GameObject>("BoardItem");
        BoardItem boardItem = boardItemGO.GetComponent<BoardItem>();
        boardItem.Setup();
        return boardItem;
    }

    [UnityTest, Repeat(10)]
    public IEnumerator Scene_GameManager_ExistsAndIsUnique()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitForEndOfFrame();
        int numGameManagers = GameObject.FindObjectsOfType<GameManager>().Length;
        Assert.True(numGameManagers == 1);
    }

    [UnityTest, Repeat(10)]
    public IEnumerator Scene_UserInput_ExistsAndIsUnique()
    {
        GameObject userInputGO = new GameObject();
        userInputGO.AddComponent<UserInput>();
        yield return new WaitForEndOfFrame();
        int numUserInput = GameObject.FindObjectsOfType<UserInput>().Length;
        Assert.True(numUserInput == 1);
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

    [UnityTest]
    public IEnumerator Scene_InitialState_IsLoading()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitForEndOfFrame();
        Assert.True(gameManager.gameState == GameState.LOADING);
    }

    [UnityTest, Timeout(5000)]
    public IEnumerator GameState_Menu_IsActivated()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitUntil(() => gameManager.gameState == GameState.MENU);
        Assert.True(gameManager.gameState == GameState.MENU);
    }

    [UnityTest]
    public IEnumerator NewGame_GameData_IsCreated()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitUntil(() => gameManager.gameState == GameState.MENU);
        Assert.True(gameManager.currentGameData == null);
        gameManager.StartNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        yield return new WaitForEndOfFrame();
        Assert.True(gameManager.currentGameData != null);
    }

    [UnityTest]
    public IEnumerator NewGame_State_IsUpdated()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitUntil(() => gameManager.gameState == GameState.MENU);
        Assert.True(gameManager.gameState != GameState.NEW_GAME);
        gameManager.StartNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        yield return new WaitForEndOfFrame();
        Assert.True(gameManager.gameState == GameState.NEW_GAME);
    }

    [UnityTest]
    public IEnumerator StartNewTurn_PlayerIndex_IsUpdated()
    {
        GameManager gameManager = MockGameManager();
        yield return new WaitUntil(() => gameManager.gameState == GameState.MENU);
        gameManager.StartNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        yield return new WaitForEndOfFrame();

        Assert.True(gameManager.currentGameData.currentPlayerIndex == PlayerIndex.NONE);
        gameManager.StartNewTurn(PlayerIndex.PLAYER_TWO);
        yield return new WaitForEndOfFrame();
        Assert.True(gameManager.currentGameData.currentPlayerIndex == PlayerIndex.PLAYER_TWO);
    }

    [UnityTest]
    public IEnumerator StartNewTurn_onGameTurnChange_IsCalled()
    {
        GameManager gameManager = MockGameManager();
        UIManager uiManager = MockUIManager();
        yield return new WaitUntil(() => gameManager.gameState == GameState.MENU);

        gameManager.StartNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        Received.InOrder(() => { uiManager.OnGameTurnChangeHandler(); });
    }

    [Test]
    public void SetGameState_onGameStateChange_IsCalled()
    {
        GameManager gameManager = MockGameManager();
        UIManager uiManager = MockUIManager();
        gameManager.SetGameState(GameState.PAUSE);
        Received.InOrder(() => { uiManager.OnGameStateChangeHandler(); });
    }

    [Test]
    public void CheckGameReadyState_onManagersReady_ReturnsTrue()
    {
        GameManager gameManager = MockGameManager();
        gameManager.SetGameState(GameState.NEW_GAME);
        gameManager.SetBoardManagerReady(true);
        gameManager.SetPlayerManagerReady(true);

        Assert.True(gameManager.CheckGameReadyState());
    }

    [Test]
    public void SelectBoardItem_ItemSelected_True()
    {
        GameManager gameManager = MockGameManager();
        BoardManager boardMAnager = MockBoardManager();
        BoardItem boardItem = CreateBoardItem();
        gameManager.SelectBoardItem(boardItem);
        Received.InOrder(() => { boardMAnager.OnGameItemSelectedHandler(boardItem); });
    }

    [Test]
    public void CheckPlayerMaxMoves_OnFail_ReturnsFalse()
    {
        GameManager gameManager = MockGameManager();
        gameManager.maxTurnMoves = maxMoves;
        Assert.False(gameManager.CheckPlayerMaxMoves(maxMoves + 1));
    }

    [Test]
    public void CheckPlayerMinMoves_OnFail_ReturnsFalse()
    {
        GameManager gameManager = MockGameManager();
        gameManager.minTurnMoves = minMoves;
        Assert.False(gameManager.CheckPlayerMinMoves(minMoves - 1));
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
}
