using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : UIMoveblePanel
{
	[Space]
	[Header("HealthBar")]
	[SerializeField] private Image progress;
	[SerializeField] private Gradient gradient;

	public void SetHealth(float normalizedValue)
	{
		progress.fillAmount = normalizedValue;
		progress.color = gradient.Evaluate(normalizedValue);
	}
}
