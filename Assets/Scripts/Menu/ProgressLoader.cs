using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLoader : MonoBehaviour
{
    #region Constants

    private const string _LEVEL = "Level_";
    private const string _LEVEL_STARS = "Level_Stars_";

    private const int _TRUE = 1;

    #endregion

    #region Structs

    [Serializable]
    private struct LevelDataStruct
    {
        public Button _levelButton;
        public int _levelNumber;
        public bool _isUnlocked;
        public Image[] _StarIcon;
    }

    [Serializable]
    public struct LevelManagerSavesStruct
    {
        public int _level;
        public bool _needSave;
    }

    #endregion

    #region Serialized Fieilds

    [SerializeField] private LevelDataStruct[] _levelData = new LevelDataStruct[5];

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            LoadLevelData(i);
        }
    }

    #endregion

    #region Public Methods

    public static void SetUnlockLevelState(int level)
    {
        PlayerPrefs.SetInt(_LEVEL + level.ToString(), _TRUE);
    }

    public static void SetLevelStars(int level, int stars)
    {
        PlayerPrefs.SetInt(_LEVEL_STARS + level.ToString(), stars);
    }

    #endregion

    #region Private Methods

    private bool GetUnlockState(int level)
    {
        if (PlayerPrefs.HasKey(_LEVEL + level.ToString())) return true;
        return false;
    }

    private void SetLevelStarsGUI(int index, int level)
    {
        if (!PlayerPrefs.HasKey(_LEVEL_STARS + level.ToString())) return;
        int stars = PlayerPrefs.GetInt(_LEVEL_STARS + level.ToString());
        switch (stars)
        {
            case 1:
                _levelData[index]._StarIcon[0].gameObject.SetActive(true);
                break;
            case 2:
                _levelData[index]._StarIcon[0].gameObject.SetActive(true);
                _levelData[index]._StarIcon[1].gameObject.SetActive(true);
                break;
            case 3:
                _levelData[index]._StarIcon[0].gameObject.SetActive(true);
                _levelData[index]._StarIcon[1].gameObject.SetActive(true);
                _levelData[index]._StarIcon[2].gameObject.SetActive(true);
                break;
        }
    }

    private void LoadLevelData(int levelDataIndex)
    {
        _levelData[levelDataIndex]._isUnlocked = GetUnlockState(_levelData[levelDataIndex]._levelNumber);
        if(_levelData[levelDataIndex]._isUnlocked)
        {
            _levelData[levelDataIndex]._levelButton.interactable = true;
        }
        SetLevelStarsGUI(levelDataIndex, _levelData[levelDataIndex]._levelNumber);
    }

    #endregion

    #region Unity Editor Methods

#if UNITY_EDITOR

    [MenuItem("PlayerPrefs/Clear")]
    public static void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Successfully clean player prefs");
    }

    [MenuItem("PlayerPrefs/Test/Set First Level Complete")]
    public static void SetFirstLevelComplete()
    {
        SetUnlockLevelState(2);
        SetLevelStars(1, 3);
    }

    [MenuItem("PlayerPrefs/Test/Set Second Level Complete")]
    public static void SetSecondLevelComplete()
    {
        SetUnlockLevelState(3);
        SetLevelStars(2, 3);
    }

    [MenuItem("PlayerPrefs/Test/Set Third Level Complete")]
    public static void SetThirdLevelComplete()
    {
        SetUnlockLevelState(4);
        SetLevelStars(3, 3);
    }

    [MenuItem("PlayerPrefs/Test/Set Fourth Level Complete")]
    public static void SetFourthLevelComplete()
    {
        SetUnlockLevelState(5);
        SetLevelStars(4, 3);
    }

    [MenuItem("PlayerPrefs/Test/Set Fifth Level Complete")]
    public static void SetFifthLevelComplete()
    {
        SetLevelStars(5, 3);
    }

#endif

    #endregion

    #endregion





























    /*

        #region Serialized Fields

        [SerializeField] private Button[] _buttons = new Button[5];
        [SerializeField] private GameObject[] _stars1Icons = new GameObject[5];
        [SerializeField] private GameObject[] _stars2Icons = new GameObject[5];
        [SerializeField] private GameObject[] _stars3Icons = new GameObject[5];
        [SerializeField] private int[] _level = new int[5];
        [SerializeField] private int[] _isComplete = new int[5];
        [SerializeField] private int[] _stars = new int[5];

        #endregion

        #region Methods

        #region Unity Methods

        private void Awake()
        {
            for (int i = 0; i < 5; i++)
            {
                _level[i] = i;
                ProcessLevel(i);
            }
        }

        #endregion

        #region Private Methods

        private void ProcessLevel(int i)
        {
            if (PlayerPrefs.HasKey(_level[i] + "_level_isComplete"))
            {
                _isComplete[i] = PlayerPrefs.GetInt(_level[i] + "_level_isComplete");
                if (_isComplete[i] == 0 && i > 1)
                {
                    _buttons[i].interactable = false;
                }
                else if (_isComplete[i] > 0 && i >= 1)
                {
                    _buttons[i].interactable = true;
                }
            }
            if (PlayerPrefs.HasKey(_level[i] + "_level_stars"))
            {
                _stars[i] = PlayerPrefs.GetInt(_level[i] + "_level_stars");
                switch (_stars[i])
                {
                    case 0:
                        break;
                    case 1:
                        _stars1Icons[i].SetActive(true);
                        break;
                    case 2:
                        _stars1Icons[i].SetActive(true);
                        _stars2Icons[i].SetActive(true);
                        break;
                    case 3:
                        _stars1Icons[i].SetActive(true);
                        _stars2Icons[i].SetActive(true);
                        _stars3Icons[i].SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Interface

        public static void SaveStarsCount(int level, int stars)
        {
            PlayerPrefs.SetInt(level + "_level_stars", stars);
        }

        public static void UnlockLevel(int level)
        {
            PlayerPrefs.SetInt(level + "_level_isComplete", 1);
        }

        #endregion



        #endregion

        */
}