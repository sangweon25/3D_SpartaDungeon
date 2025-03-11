using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerResource resource;

    private ItemData itemData;
    public ItemData ItemData 
    { 
        get { return itemData; }
        set
        {
            itemData = value;
           

        }
            
    }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        resource = GetComponent<PlayerResource>();

        //null일시 오류 메시지
        if (controller == null)
            Debug.LogError("cotnroller is Null");
        if (resource == null)
            Debug.LogError("resource is Null");
    }

}
