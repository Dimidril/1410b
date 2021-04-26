using UnityEngine;

public class SpritesFaceCam : MonoBehaviour
{
    #region Fields

    #region Private Fields

    private Transform cam;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(cam.forward);
    }

    #endregion

    #endregion
}