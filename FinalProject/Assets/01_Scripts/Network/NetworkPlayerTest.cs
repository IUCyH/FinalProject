using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayerTest : NetworkBehaviour
{
    [SerializeField]
    TextMeshProUGUI chatTextPrefab;
    TextMeshProUGUI chatText;
    
    int count;
    [SerializeField]
    float speed;

    void Awake()
    {
        chatText = Instantiate(chatTextPrefab);
        TitleManager.Instance.SetParentToTitleCanvas(chatText.GetComponent<RectTransform>(), Vector2.zero);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RPCTest("Test : " + count++);
        }
    }

    public override void FixedUpdateNetwork()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var dir = new Vector3(horizontal, vertical, 0f);

        transform.position += speed * Runner.DeltaTime * dir;
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPCTest(string message)
    {
        chatText.text = message;
    }
}
