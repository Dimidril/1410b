using UnityEngine;

public class ShapitoCam : MonoBehaviour
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private CameraController cameraController;
    [SerializeField] private float height;

    #endregion

    #region Private Fields

    private bool isactive;

    #endregion

    #endregion

    #region Methods

    #region Private Methods

    private void OnTriggerEnter (Collider col)
    {
        if (col.GetComponent<Hero>())
        {
            cameraController.SetOffSet(isactive ? 1 / height : height);
            isactive = !isactive;
        }
    }

    #endregion

    #endregion
}
