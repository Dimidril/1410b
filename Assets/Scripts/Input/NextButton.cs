using UnityEngine.EventSystems;
using UnityEngine;

public class NextButton : MonoBehaviour, IPointerClickHandler
{
[SerializeField] private Camera cam1;
[SerializeField] private Camera cam2;
[SerializeField] private GameObject Canvas;
AudioListener cam1Audio;
AudioListener cam2Audio;

private void Start()
{
    cam1Audio = cam1.GetComponent<AudioListener>();
    cam2Audio = cam2.GetComponent<AudioListener>();
    Canvas.SetActive(true);
    cam1.enabled = true;
    cam1Audio.enabled = true;
    cam2.enabled = false;
    cam2Audio.enabled = false;
}
public void OnPointerClick(PointerEventData eventData)
{
    cam1Audio = cam1.GetComponent<AudioListener>();
    cam2Audio = cam2.GetComponent<AudioListener>();
    Canvas.SetActive(false);
    cam1.enabled = false;
    cam1Audio.enabled = false;
    cam2.enabled = true;
    cam2Audio.enabled = true;
}

}
