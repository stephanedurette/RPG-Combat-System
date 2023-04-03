using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health representedHealth;
    [SerializeField] private List<Image> heartImages;

    [SerializeField] private Sprite filledHeartSprite, emptyHeartSprite;

    // Start is called before the first frame update
    private void OnEnable()
    {
        representedHealth.OnHealthChanged += RepresentedHealth_OnHealthChanged;
    }

    private void Start()
    {
        SetHealth(representedHealth.CurrentHealth);
    }

    private void RepresentedHealth_OnHealthChanged(object sender, Health.OnHealthChangedEventArgs e)
    {
        SetHealth(e.newHealthValue);
    }

    private void SetHealth(int health)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            var heartHealth = i + 1;

            heartImages[i].enabled = (representedHealth.MaxHealth >= heartHealth);
            heartImages[i].sprite = health >= heartHealth ? filledHeartSprite : emptyHeartSprite;
        }
    }

    private void OnDisable()
    {
        representedHealth.OnHealthChanged -= RepresentedHealth_OnHealthChanged;
    }
}
