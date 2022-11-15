using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    #region VARIABLES
    #region SERIALIZED VARIABLES
    #endregion

    #region STATIC VARIABLES

    #endregion

    #region PROTECTED VARIABLES

    #endregion

    #region PRIVATE VARIABLES
    private float speed;
    #endregion
    #endregion

    #region METHODS
    #region PUBLIC METHODS

    #endregion

    #region STATIC METHODS

    #endregion

    #region PROTECTED METHODS

    #endregion

    #region PRIVATE METHODS
    private void Update()
    {
        speed = Managers.GameManager.VerticalSpeed;

        Vector3 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
    #endregion
    #endregion
}