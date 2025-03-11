using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public UIResource uiResource;

    public CharacterResource Health { get { return uiResource.health; } }
    public CharacterResource Stamina { get { return uiResource.stamina; } }


    private void Update()
    {
        //스태미나 회복
        if(Stamina.CurVal < Stamina.maxVal)
            RecoverStamina();
    }

    public void Heal(float amount) //HP 회복
    {
        Health.Increase(amount);
    }
    public void St_Decrease(float amount) // 스태미나 감소
    {
        Stamina.Decrease(amount);
    }
    public void RecoverStamina()
    {
        Stamina.Increase(7f * Time.deltaTime);
    }
}
