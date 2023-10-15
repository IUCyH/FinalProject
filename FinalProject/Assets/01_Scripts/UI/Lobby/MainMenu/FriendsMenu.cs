using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject friendProfileBarPrefab;
    [SerializeField]
    GridLayoutGroup gridLayoutGroup;
    ObjectPool<GameObject> friendProfileBarPool;

    void Awake()
    {
        friendProfileBarPool = new ObjectPool<GameObject>(5, () =>
        {
            var obj = Instantiate(friendProfileBarPrefab);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.position = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            return obj;
        });
    }
}
