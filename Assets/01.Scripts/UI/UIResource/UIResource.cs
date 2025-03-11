using UnityEngine;

public class UIResource : MonoBehaviour
{
    [Header("ResourceBar")]
    public CharacterResource health;
    public CharacterResource stamina;


    private void Start()
    {
        CharacterManager.Instance.Player.resource.uiResource = this;

        //ResourceType이 다를 시 경고 메시지
        if (health.ResourceType != EResourceType.Health)
            Debug.LogWarning($"{health} Type is inCorrect");

        if (stamina.ResourceType != EResourceType.Stamina)
            Debug.LogWarning($"{stamina} Type is inCorrect");
        
    }

}
