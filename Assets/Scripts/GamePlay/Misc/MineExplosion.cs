using System.Collections;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
	#region Serialized Fields

	[SerializeField] private float _deleteTime = 2f;

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        StartCoroutine(DeleteTimer());
    }

    #endregion

    #region Private Methods

    private IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(_deleteTime);
        Destroy(gameObject);
    }

    #endregion

    #endregion
}