using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public enum KindOfFlower
    {
        Red,
        Blue,
        Green
    }
    
    public class GardenManger : Singleton<GardenManger>
    {
        FlowerPerson[] persons;

        protected override void OnStart()
        {
            persons = GetComponentsInChildren<FlowerPerson>();
        }

        public FlowerPerson GetPerson(KindOfFlower kindOfFlower)
        {
            for (int i = 0; i < persons.Length; i++)
            {
                if (persons[i].CanMove && persons[i].KindOfFlower == kindOfFlower)
                {
                    return persons[i];
                }
            }

            return null;
        }
    }
}
