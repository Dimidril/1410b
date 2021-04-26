using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.1f;

    #endregion

    #region Private Fields

    private Vector3 _cameraOffset;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        _cameraOffset = transform.position - _target.position;
    }

    void FixedUpdate()
    {
        Vector3 pos = new Vector3(_target.position.x, _target.position.y + _cameraOffset.y, _target.position.z + _cameraOffset.z);
        transform.position = Vector3.Lerp(transform.position, pos, _smoothSpeed);
    }

    #endregion

    #region Puplic Methods

    public void SetOffSet(float offset)
    {
        _cameraOffset *= offset;
    }

    #endregion

    #endregion
}