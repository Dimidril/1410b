using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Serialized Field

    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private Button Pausebutton;
    [SerializeField] private Button Resumebutton;
    [SerializeField] private Button Quitebutton;
    [SerializeField] private GameObject Sure;
    [SerializeField] private Button Yesbutton;
    [SerializeField] private Button Nobutton;
    
    #endregion

    #region Unity Methods
    void Awake()
    {
        Pausebutton.onClick.AddListener(()=>
        {
            Time.timeScale = 0f;
            PauseMenuUI.SetActive(true);
            Canvas.SetActive(false);
        });   
        Resumebutton.onClick.AddListener(()=>
        {
            Time.timeScale = 1f;
            PauseMenuUI.SetActive(false);
            Canvas.SetActive(true);
        });   
        Quitebutton.onClick.AddListener(()=>
        {
            Buttons.SetActive(false);
            Sure.SetActive(true);
        });  
        Yesbutton.onClick.AddListener(()=>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        });
        Nobutton.onClick.AddListener(()=>
        {
            Buttons.SetActive(true);
            Sure.SetActive(false);
        });
    }
    #endregion
}