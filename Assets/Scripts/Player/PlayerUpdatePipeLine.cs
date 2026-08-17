using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
public class PlayerUpdatePipeLine : MonoBehaviour
{
    [SerializeField] List<PlayerModule> playerUpdateLists;


    void Awake()
    {
       gameObject.tag = "Player";
       gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void Update()
    {
        for (int i = 0; i < playerUpdateLists.Count; i++)
        {
            playerUpdateLists[i].UpdateModule();
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < playerUpdateLists.Count; i++)
        {
            playerUpdateLists[i].FixedUpdateModule();
        }
    }
}
