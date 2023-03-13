using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Audio;
using Core.Level;
using Core.Managers;
using Core.Player;
using Core.Symbol;
using Core.Utilities;
using UnityEngine;
using DG.Tweening;
using Michsky.MUIP;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameController manager = (GameController)target;

        if (GUILayout.Button("GET GAME LEVELS DATA"))
        {
            manager.GetGameLevelsData();
        }
        if (GUILayout.Button("SET GAME LEVELS DATA"))
        {
            manager.SetGameLevelsData();
        }
        if (GUILayout.Button("GET LAST LEVEL"))
        {
            manager.CallLastLevel();
        }

        if (GUILayout.Button("GET NEXT LEVEL"))
        {
            manager.SetNextLevel();
        }

        if (GUILayout.Button("RESTART LEVEL"))
        {
            manager.RestartLevel();
        }
        
        if (GUILayout.Button("SET CURRENT SYMBOLS"))
        {
            manager.SetCurrentSymbols();
        }    
    }
}
#endif

namespace Core.Managers
{

    [Serializable]
    public struct LevelBlock
    {
        public GameLevel gameLevel;
        public List<SymbolData> gameLevelSymbols;
        public int levelCount;
    }

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        private bool _isTutorialLevel = false;
        private bool _gameInstanceActive = false;
        public bool GameInstanceActive => _gameInstanceActive;
        public bool IsTutorialLevel => _isTutorialLevel;
        
        private UnityAction onLevelStart;
        private UnityAction onLevelComplete;
        private UnityAction onLoadingStart;
        private UnityAction onLoadingComplete;
        
        [SerializeField] private PlayerController player;
        [SerializeField] private List<SymbolData> PlayerSymbols;
        [SerializeField] private List<SymbolBase> currentSymbols;

        [SerializeField] LevelBlock[] levelBlock;

        [Tooltip("starting and playing level (save this variable for save game level)"), SerializeField] int currentLevel;

        #region Event Methods
        
        public void SubscribeOnLevelComplete(UnityAction action,bool subscribe)
        {
            if(subscribe)
                onLevelComplete += action;
            else
            {
                onLevelComplete -= action;
            }
        }
        public void InvokeOnLevelComplete()
        {
            onLevelComplete?.Invoke();

        }
        public void SubscribeOnLevelStart(UnityAction action,bool subscribe)
        {
            if(subscribe)
                onLevelStart += action;
            else
            {
                onLevelStart -= action;
            }
        }
        public void InvokeOnLevelStart()
        {
            onLevelStart?.Invoke();
        }
        public void SubscribeOnLoadingStart(UnityAction action,bool subscribe)
        {
            if(subscribe)
                onLoadingStart += action;
            else
            {
                onLoadingStart -= action;
            }
        }
        public void InvokeOnLoadingStart()
        {
            onLoadingStart?.Invoke();
        }
        
        public void SubscribeOnLoadingComplete(UnityAction action,bool subscribe)
        {
            if(subscribe)
                onLoadingComplete += action;
            else
            {
                onLoadingComplete -= action;
            }
        }

        public void InvokeOnLoadingComplete()
        {
            onLoadingComplete?.Invoke();
        }

        #endregion

        #region Built-In Methods
        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void OnEnable()
        {
            SubscribeOnLevelStart(OnLevelStart,true);
            SubscribeOnLevelComplete(OnLevelComplete,true);
            
            SubscribeOnLoadingComplete(OnNextLevel,true);

        }

        private void OnDisable()
        {
            SubscribeOnLevelStart(OnLevelStart,false);
            SubscribeOnLevelComplete(OnLevelComplete,false);
            
            SubscribeOnLoadingComplete(OnNextLevel,false);

        }

        #endregion
        
        #region Core Methods
        
        private void OnLevelStart()
        {
            StartCoroutine(StartLevelRoutine());
        }
        private void InitializeGame()
        {
            Debug.LogError("Game Initialized");

            GetGameLevelsData();
            SetGameLevelsData();
            SetCurrentSymbols();
            CallLevel(currentLevel);
            LoadPlayer();

        }

        private void LoadPlayer()
        {
            player.InitPlayer();
            player.gameObject.SetActive(true);
        }
        private void SetGameInstance(bool status)
        {
            _gameInstanceActive = status;
        }
        
        private void OnLevelComplete()
        {
            Debug.LogError("Level Completed");
            SetGameInstance(false);
            
        }

        private void OnNextLevel()
        {        
            Debug.ClearDeveloperConsole();

            StartCoroutine(NextLevelRoutine());

        }

        #endregion

        #region Enumerators
        /// <summary>
        /// Load first level and initialize it then wait for fade time then activate level
        /// </summary>
        /// <returns></returns>
        /// 
        private IEnumerator StartLevelRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.LogError("Level Started");
            SetGameInstance(true);

        }
        /// <summary>
        /// Load next level and initialize it then wait for fade time then activate level
        /// </summary>
        /// <returns></returns>
        private IEnumerator NextLevelRoutine()
        {
            SetNextLevel();
            GetGameLevelsData();
            SetGameLevelsData();
            SetCurrentSymbols();
            CallLevel(currentLevel);
            InvokeOnLevelStart();
            yield return new WaitForSeconds(1.5f); // wait for fade time
            Debug.LogError("Next Level Started");
            SetGameInstance(true);
        }


        #endregion
        
        
        #region Level Methods
        
        public void CallLastLevel()
        {
            if (currentLevel == 0)
            {
                RestartLevel();
                return;
            }

            int lastLevelIndex = currentLevel - 1;

            if (lastLevelIndex < 0)
            {
                lastLevelIndex = 0;
            }

            CallLevel(lastLevelIndex, currentLevel);
            currentLevel = lastLevelIndex;

        }

        public void SetNextLevel()
        {
            int lastLevelIndex = currentLevel + 1;
            if (lastLevelIndex >= levelBlock.Length)
                lastLevelIndex = levelBlock.Length - 1;
            currentLevel = lastLevelIndex;
        }

        public void GetGameLevelsData()
        {
            PlayerSymbols = new List<SymbolData>();

            for (int i = 0; i < player.transform.childCount; i++)
            {
                var isPlayerSymbol = player.transform.GetChild(i)
                    .TryGetComponent(out PlayerSymbol playerSymbol);

                if (isPlayerSymbol)
                {
                    PlayerSymbols.Add(playerSymbol.GetSymbolData());
                }
            }
            
            levelBlock = new LevelBlock[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                var isGameLevel = transform.GetChild(i).TryGetComponent(out GameLevel gameLevel);
                if (isGameLevel)
                {
                    levelBlock[i].gameLevel = gameLevel;
                    gameLevel.gameObject.SetActive(false);

                    levelBlock[i].gameLevelSymbols = new List<SymbolData>();
                    levelBlock[i].levelCount = i;
                    for (int j = 0; j < gameLevel.transform.childCount; j++)
                    {
                        var isGameSymbol = gameLevel.transform.GetChild(j).TryGetComponent(out SymbolBase symbolBase);
                        if (isGameSymbol)
                        {
                            levelBlock[i].gameLevelSymbols.Add(symbolBase.GetSymbolData());
                        }
                    }
                }
            }
        }
        public void SetGameLevelsData()
        {
            SetPlayerSymbolData();
            
            for (int i = 0; i < levelBlock.Length; i++)
            {
                var gameLevel = levelBlock[i].gameLevel;
                
                for (int j = 0; j < levelBlock[i].gameLevelSymbols.Count; j++)
                {
                    var index = 0;
                    for (int k = 0; k < gameLevel.transform.childCount; k++)
                    {
                        var isGameSymbol = gameLevel.transform.GetChild(k).TryGetComponent(out SymbolBase symbolBase);
                            
                        if (isGameSymbol)
                        {
                            symbolBase.SetSymbolData = levelBlock[i].gameLevelSymbols[index];
                            index++;

                        }
                    }
                }
            }
        }

        public void SetCurrentSymbols()
        {
            currentSymbols = new List<SymbolBase>();
            currentSymbols.Clear();
            
            for (int i = 0; i < levelBlock.Length; i++)
            {
                var gameLevel = levelBlock[i].gameLevel;
                
                for (int j = 0; j < levelBlock[i].gameLevelSymbols.Count; j++)
                {
                    for (int k = 0; k < gameLevel.transform.childCount; k++)
                    {
                        var isGameSymbol = gameLevel.transform.GetChild(k).TryGetComponent(out SymbolBase symbolBase);
                            
                        if (isGameSymbol)
                        {
                            var isPuzzleSymbol = symbolBase.TryGetComponent(out PuzzleSymbol puzzle);
                            if (isPuzzleSymbol)
                            {
                                if (!currentSymbols.Contains(symbolBase) && levelBlock[i].levelCount == currentLevel)
                                {
                                    currentSymbols.Add(symbolBase);

                                }
                            }
                        }
                    }
                }
            }

        }

        public SymbolBase GetCurrentSymbol()
        {
            foreach (var symbol in currentSymbols)
            {
                if (!symbol.Activated)
                    return symbol;
            }

            Debug.LogError("NO Symbols Found");
            return null;
        }

        private void SetPlayerSymbolData()
        {
            int playerIndex = 0;
            for (int j = 0; j < player.transform.childCount; j++)
            {
                var isPlayerSymbol = player.transform.GetChild(j).TryGetComponent(out SymbolBase symbolBase);

                if (isPlayerSymbol)
                {
                    symbolBase.SetSymbolData = PlayerSymbols[playerIndex];
                    playerIndex++;
                }
            }
        }

        private void SetPlayerData(int levelIndex, int count)
        {
            int playerIndex = 0;
            for (int j = 0; j < player.transform.childCount; j++)
            {
                var isPlayerSymbol = player.transform.GetChild(j).TryGetComponent(out SymbolBase symbolBase);
        
                if (isPlayerSymbol)
                {
                    symbolBase.SetSymbolData = levelBlock[levelIndex].gameLevelSymbols[playerIndex];
                    playerIndex++;
                    if (playerIndex == count)
                    {
                        break;
                    }
                }
            }
        }


        public void RestartLevel()
        {
            CallLevel(currentLevel);
        }

        public GameLevel GetLevelData()
        {
            return levelBlock[currentLevel].gameLevel;
        }

        private void CallLevel(int index, int closedLevelIndex = -1)
        {
            Time.timeScale = 0;
            if (index is 0 || index is 1)
                _isTutorialLevel = true;
            else
            {
                _isTutorialLevel = false;
            }
            int symbolCount = levelBlock[index].gameLevelSymbols.Count / 2;
            
            SetPlayerData(index, symbolCount);
            
            levelBlock[index].gameLevel.gameObject.SetActive(true);

            if (closedLevelIndex >= 0)
                levelBlock[index].gameLevel.gameObject.SetActive(false);
            
            Time.timeScale = 1;
        }

        

        #endregion
        


    }
}

