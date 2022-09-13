using UnityEngine;
using System;

    public class InputManager : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES

        #endregion

        #region STATIC VARIABLES

        public static Action<MovementDirection> OnMovementPress;
        public static Action OnPausePress;
        public static Action<Vector3> OnClicPress;

        #endregion

        #region PRIVATE VARIABLES

        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        #region PRIVATE METHODS

        private void Update()
        {
            if (!PauseSystem.Paused)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) OnMovementPress?.Invoke(MovementDirection.Backward);

                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) OnMovementPress?.Invoke(MovementDirection.Forward);

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) OnMovementPress?.Invoke(MovementDirection.Left);

                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) OnMovementPress?.Invoke(MovementDirection.Right);

                //if (Input.GetMouseButtonDown(0))
                //{
                //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //    RaycastHit[] info;

                //    info = Physics.RaycastAll(ray);

                //    foreach (RaycastHit item in info)
                //    {
                //        if (item.collider.tag == "Floor") OnClicPress?.Invoke(item.point); //Should enter only 1 time
                //    }

                //}
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                OnPausePress?.Invoke();
            }

            
        }
        #endregion
        #endregion
    }

