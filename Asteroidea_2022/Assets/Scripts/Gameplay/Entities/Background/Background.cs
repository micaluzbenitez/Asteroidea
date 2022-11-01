using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Background
{
    public class Background : MonoBehaviour
    {
        [Header("Background data")]
        [SerializeField] private float maxPosition = 0;
        [SerializeField] private float resetPosition = 0;
        [SerializeField] private float speed = 0;

        [Header("Assets")]
        [SerializeField] private Sprite[] assets = null;

        [Header("Entities")]
        [SerializeField] private float maxInitPosition = 0;
        [SerializeField] private float minInitPosition = 0;
        [SerializeField] private SpriteRenderer[] entities = null;
        [SerializeField] private UnityEvent initEntity = null;

        private void Update()
        {
            VerticalMovement();

            if (transform.localPosition.y > maxPosition)
            {
                Reset();
                InitEntities();
            }
        }
        
        private void VerticalMovement()
        {
            Vector3 pos = transform.localPosition;
            pos.y += speed * Time.deltaTime;
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }
        
        private void Reset()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, resetPosition, transform.localPosition.z);
        }

        private void InitEntities()
        {
            for (int i = 0; i < entities.Length; i++)
            {
                int index = Random.Range(0, assets.Length);
                entities[i].sprite = assets[index];

                float position = Random.Range(minInitPosition, maxInitPosition);
                entities[i].gameObject.transform.position = new Vector2(position, entities[i].gameObject.transform.position.y);
            }

            initEntity?.Invoke();
        }
    }
}