using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demo
{
    public class FlowerPerson : MonoBehaviour
    {
        [SerializeField]
        KindOfFlower flower;
        [SerializeField]
        bool isCanMove = true;
        [SerializeField]
        float speed;

        public bool CanMove => isCanMove;
        public bool FreeAct { get; private set; }
        public KindOfFlower KindOfFlower => flower;

        IEnumerator Coroutine_Update()
        {
            while (!FreeAct) yield return null;
            
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

        void Start()
        {
            StartCoroutine(Coroutine_Update());
        }

        public bool MoveToField(Vector3 targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            isCanMove = Vector3.Distance(transform.position, targetPos) > 0f;

            return !isCanMove;
        }

        public void ActFree()
        {
            FreeAct = true;
        }
    }
}
