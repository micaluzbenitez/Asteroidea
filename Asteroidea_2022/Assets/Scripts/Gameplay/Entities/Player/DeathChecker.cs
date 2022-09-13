using UnityEngine;
using System;

namespace Entities.Player
{
    public class DeathChecker : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES

        #endregion

        #region STATIC VARIABLES
        public static Action OnReachLimit;
        #endregion

        #region PROTECTED VARIABLES

        #endregion

        #region PRIVATE VARIABLES

        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS
        public void DeadPlayer()
        {
            OnReachLimit?.Invoke();
        }
        #endregion

        #region STATIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Limit")/* || collision.tag.Equals("Enemy") || collision.tag.Equals("Shot")*/) DeadPlayer();
        }
        #endregion
        #endregion
    }
}
