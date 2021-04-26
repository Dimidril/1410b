using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class SceneLoader : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Menu");
    }
}
