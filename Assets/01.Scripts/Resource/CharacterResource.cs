using UnityEngine;
using UnityEngine.UI;

public enum EResourceType
{
    Health,Stamina
}

public class CharacterResource : MonoBehaviour
{
    public EResourceType ResourceType;

    [Header("ResourceVal")]
    [Range(1f, 100f)]
    public float startVal = 100f;
    [Range(1f,200f)]
    public float maxVal = 100f;
    [SerializeField] private float curVal;

    [Header("ResourceBar")]
    public Image uiBar;


    private void Start()
    {
        curVal = startVal;

        if(uiBar == null)
            TryGetComponent<Image>(out uiBar);
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Increase(float amount)
    {
        curVal = Mathf.Max(curVal + amount,maxVal);
    }

    public void Decrease(float amount)
    {
        curVal = Mathf.Min(curVal -amount, 0);
    }


    public float GetPercentage()
    {
        return curVal / maxVal;
    }


}
