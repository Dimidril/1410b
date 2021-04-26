using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Season1LvlPannel : Page
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private Button _level1Button;
    [SerializeField] private Button _level2Button;
    [SerializeField] private Button _level3Button;
    [SerializeField] private Button _level4Button;
    [SerializeField] private Button _level5Button;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    #endregion

    #region Unity Methods
    /*
        private void Awake()
        {

        }
        */
    #endregion

    #endregion
}