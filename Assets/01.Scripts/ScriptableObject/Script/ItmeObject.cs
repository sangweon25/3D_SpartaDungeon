using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

//ItemData의 데이터의 정보를 가지고 있는 클래스 
public class ItmeObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    private new SphereCollider collider;

    private void Awake()
    {
        collider = GetComponentInChildren<SphereCollider>();
    }
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        //CharacterManager.Instance.Player.ItemData = data;
        OnItemAbility(data.GetFirstType());
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnInteract();
        }
    }
    public void OnItemAbility(ConsumableType type) // 아이템 먹었을때 Type에 따라서 value 리턴
    {
        switch (type)
        {
            case ConsumableType.Health:
                CharacterManager.Instance.Player.resource.Heal(data.GetFirstValue());
                break;
            case ConsumableType.SpeedUp:
                CharacterManager.Instance.Player.controller.BoostSpeed(data.GetFirstValue(),data.GetFirstbuffTime());
                break;
        }
    }
}
