using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Entities.Enemies.Enemy
{
    public enum MinePosition
    {
        LeftUp, Up, RightUp,
        Left, Idle, Right,
        LeftBot, Bot, RightBot,
        Count
    }

    [Header("Mine Settings")]
    [SerializeField] private float movementAmount = 1;
    [SerializeField] private float idleSpeed = 1;

    [Header("Original Position")]
    [SerializeField] private Vector3 originalPos = Vector3.zero;
    [SerializeField] private bool useOriginalPosAsInitialPos = true;

    [Header("Reset trigger")]
    [SerializeField] private string triggerTagName = "";

    private Vector3 initialPos;
    private Vector3 actualPos;
    private Vector3 targetPos;

    private IEnumerator movementCoroutine;

    private void Start()
    {

        if (useOriginalPosAsInitialPos)
        {
            transform.position = originalPos;
        }
        else
        {
            originalPos = transform.position;
        }

        initialPos = originalPos;
        //rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        movementCoroutine = MoveMine(initialPos);
        StartCoroutine(movementCoroutine);
        
    }
    private float SetTargetPos(MinePosition nextPosition)
    {
        targetPos = originalPos;
        switch (nextPosition)
        {
            case MinePosition.LeftUp:
                targetPos.x -= movementAmount;
                targetPos.y += movementAmount;
                break;
            case MinePosition.Up:
                targetPos.y += movementAmount;
                break;
            case MinePosition.RightUp:
                targetPos.x += movementAmount;
                targetPos.y += movementAmount;
                break;
            case MinePosition.Left:
                targetPos.x -= movementAmount;
                break;
            case MinePosition.Right:
                targetPos.x += movementAmount;
                break;
            case MinePosition.LeftBot:
                targetPos.x -= movementAmount;
                targetPos.y -= movementAmount;
                break;
            case MinePosition.Bot:
                targetPos.y -= movementAmount;
                break;
            case MinePosition.RightBot:
                targetPos.x += movementAmount;
                targetPos.y -= movementAmount;
                break;
            case MinePosition.Idle:
            default:
                //targetPos = originalPos
                break;
        }

        return Vector3.Distance(targetPos, initialPos);

    }

    public void SetOriginalPos(Vector3 newOriginPos) => originalPos = newOriginPos;

    private IEnumerator MoveMine(Vector3 originalPos)
    {
        float targetDistance = SetTargetPos((MinePosition)Random.Range(0, (int)MinePosition.Count));

        float t = 0;
        float distanceTraveled = 0;
        Debug.LogWarning("Empiezo la corrutina");
        while (distanceTraveled < targetDistance)
        {
            t += Time.deltaTime * idleSpeed;
            actualPos = Vector3.Lerp(initialPos, targetPos, t);
            transform.position = actualPos;
            distanceTraveled = Vector3.Distance(actualPos, initialPos);
            yield return null;
        }
        Debug.LogWarning("Termine la corrutina");
        actualPos = targetPos;
        transform.position = targetPos;
        initialPos = targetPos;

        StartCoroutine(MoveMine(initialPos));
    }

    private void OnDestroy()
    {
        StopCoroutine(movementCoroutine);
        //Tirar Evento de Particulas
        //Tirar evento de Wwise(sonido)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(triggerTagName))
        {
            gameObject.SetActive(false);
        }
    }

}
