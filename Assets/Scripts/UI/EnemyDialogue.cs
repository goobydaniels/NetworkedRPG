using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameStateHandler;

public class EnemyDialogue : NetworkBehaviour
{
    [SerializeField] private GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        //canvas.SetActive(true);
        if (other.CompareTag("Player"))
        {
            //canvas.SetActive(true);
            RPC_PlayerEnter(other.GetComponent<NetworkCharacterController>().Object.InputAuthority);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //canvas.SetActive(false);
        if (other.CompareTag("Player"))
        {
            //canvas.SetActive(false);
            RPC_PlayerExit(other.GetComponent<NetworkCharacterController>().Object.InputAuthority);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerEnter([RpcTarget] PlayerRef playerID)
    {
        canvas.SetActive(true);
        
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerExit([RpcTarget] PlayerRef playerID)
    {
        canvas.SetActive(false);
    }
}
