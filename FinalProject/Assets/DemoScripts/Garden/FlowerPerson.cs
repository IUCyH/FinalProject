using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public KindOfFlower KindOfFlower => flower;

        public bool MoveToField(Vector3 targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            isCanMove = Vector3.Distance(transform.position, targetPos) > 0f;

            return !isCanMove;
        }
    }
}
