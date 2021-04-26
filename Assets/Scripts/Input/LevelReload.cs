using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelReload : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isNext;

    public void OnPointerClick(PointerEventData eventData)
    {
        int l = isNext ? 1 : 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+l);
    }
}
