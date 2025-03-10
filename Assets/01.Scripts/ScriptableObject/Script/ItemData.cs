using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    SpeedUp
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float buffTime;
}

//아이템의 데이터만을 가지고 있는 클래스
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    public ConsumableType GetFirstType()
    {
        return consumables[0].type;
    }
    public float GetFirstValue()
    {
        return consumables[0].value;
    }
    public float GetFirstbuffTime()
    {
        return consumables[0].buffTime;
    }
}
