using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class FieldOfLand : MonoBehaviour
    {
        [SerializeField]
        KindOfFlower kindOfFlower;
        FlowerPerson person;
        
        [SerializeField]
        bool isFull;
        bool canClick;
        int solar;
        int maxSolar = 5;
        float maxTime = 1f;
        float timer;

        void OnMouseDown()
        {
            if (!canClick) return;

            Debug.Log("Take All Solar : " + solar);

            solar = 0;
            canClick = false;
        }

        void Update()
        {
            if (!isFull)
            {
                if (ReferenceEquals(person, null))
                    person = GardenManger.Instance.GetPerson(kindOfFlower);

                if (!ReferenceEquals(person, null))
                {
                    isFull = person.MoveToField(transform.position);
                }

                if (isFull) person = null;
            }
            else
            {
                timer += Time.deltaTime;

                if (timer > maxTime)
                {
                    timer = 0f;
                    Debug.Log(solar++);
                }

                if (solar >= maxSolar)
                {
                    canClick = true;
                }
            }
        }
    }
}
