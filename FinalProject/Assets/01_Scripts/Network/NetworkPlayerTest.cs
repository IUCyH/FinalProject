using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayerTest : NetworkBehaviour
{    
    [SerializeField]
    float speed;

    public override void FixedUpdateNetwork()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var dir = new Vector3(horizontal, vertical, 0f);

        transform.position += speed * Runner.DeltaTime * dir;
    }
}
