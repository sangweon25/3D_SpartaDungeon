using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public UIResource uiResource;

    CharacterResource Health { get { return uiResource.health; } }
    CharacterResource Stamina { get { return uiResource.stamina; } }


    private void Update()
    {
        //스태미나 회복
    }

    public void Heal(float amount) //HP 회복
    {
        Health.Increase(amount);
    }
    public void St_Decrease(float amount) // 스태미나 감소
    {
        Stamina.Decrease(amount);
    }
}
