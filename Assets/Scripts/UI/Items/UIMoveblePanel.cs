using Architecture.ObjectPool;
using UnityEngine;

public class UIMoveblePanel : PoolableObject<UIMoveblePanel>
{
	[Header("Base")]
	[SerializeField] private Vector3 followOffset;

	private RectTransform rectTransform;
	private Transform target;
	private Vector3 anchorPosition;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
		rectTransform.position = target.position + followOffset;
	}

	public void SetTarget(Vector3 anchorPosition)
	{
		this.anchorPosition = anchorPosition;
		rectTransform.position = anchorPosition + followOffset;
	}

	protected virtual void Update()
	{
		if (target != null)
		{
			rectTransform.position = target.position + followOffset;
		}
		else if (anchorPosition != Vector3.zero)
		{
			rectTransform.position = anchorPosition + followOffset;
		}
	}
}
