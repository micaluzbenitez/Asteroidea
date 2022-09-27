using UnityEngine;

namespace Utils
{
    public class ColorRandomizer : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [Header("RGB SETTINGS")]
        [Space(5)]
        [Header("Red")]
        [SerializeField] private float MinR = 0;
        [SerializeField] private float MaxR = 1;
        [Header("Green")]
        [SerializeField] private float MinG = 0;
        [SerializeField] private float MaxG = 1;
        [Header("Blue")]
        [SerializeField] private float MinB = 0;
        [SerializeField] private float MaxB = 1;
        [Header("Alpha")]
        [SerializeField] private float MinAlpha = 1.0f;
        [SerializeField] private float MaxAlpha = 1.0f;

        #endregion

        #region STATIC VARIABLES

        #endregion

        #region PROTECTED VARIABLES

        #endregion

        #region PRIVATE VARIABLES
        private SpriteRenderer spriteRenderer;
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
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void OnEnable()
        {
            Color newColor = new Color(
                Random.Range(MinR, MaxR),
                Random.Range(MinG, MaxG),
                Random.Range(MinB, MaxB),
                Random.Range(MinAlpha, MaxAlpha)
            );

            spriteRenderer.color = newColor;
        }
        #endregion
        #endregion
    }
}