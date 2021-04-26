using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject Canvas;
    AudioListener cam1Audio;
    AudioListener cam2Audio;
    void Start()
    {
        cam1Audio = cam1.GetComponent<AudioListener>();
        cam2Audio = cam2.GetComponent<AudioListener>();
        Canvas.SetActive(true);
        cam1.enabled = true;
        cam1Audio.enabled = true;
        cam2.enabled = false;
        cam2Audio.enabled = false;
    }
}
