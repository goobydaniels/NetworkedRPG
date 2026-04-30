using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameStateHandler;

enum NPCTYPE
{
    enemy = 0,
    potion,
    smith,
};

public class EnemyDialogue : NetworkBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private NPCTYPE type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        itemGive();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerExit([RpcTarget] PlayerRef playerID)
    {
        canvas.SetActive(false);
    }

    //Function lets NPC interact with players beyond just transitioning to battle
    private void itemGive()
    {
        switch(type)
        {
            case NPCTYPE.potion:
                //call function to give potion
                break;
            case NPCTYPE.smith:
                //call function to give player sword
                break;
            case NPCTYPE.enemy:
                //nothing
                break;
            default:
                //nothing
                break;
        }
    }
}
