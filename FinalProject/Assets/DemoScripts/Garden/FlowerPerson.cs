using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlowerPerson : MonoBehaviour
{
    [SerializeField]
    KindOfFlower flower;
    [SerializeField]
    bool isCanAttach = true;
    [SerializeField]
    float speed;

    public bool CanAttach => isCanAttach;
    public KindOfFlower KindOfFlower => flower;

    IEnumerator Coroutine_MoveFree()
    {
        while (true)
        {
            var playerPos = transform.position;
            var randomPosX = Random.Range(playerPos.x - 8f, playerPos.x + 8f);
            var randomPosY = Random.Range(playerPos.y - 8f, playerPos.y + 8f);

            for (int i = 0; i <= 300; i++)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomPosX, randomPosY, 0f), speed * Time.deltaTime);

                yield return null;
            }

            yield return null;
        }
    }

    public void MoveToField(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        isCanAttach = (targetPos - transform.position).sqrMagnitude > 0.1f;
    }

    public void ActFree()
    {
        StartCoroutine(Coroutine_MoveFree());
    }
}
