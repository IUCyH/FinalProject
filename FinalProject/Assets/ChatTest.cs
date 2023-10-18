using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class ChatTest : NetworkBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    int a;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RPCTest("Test : " + a++);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPCTest(string message)
    {
        text.text = message;
    }
}
