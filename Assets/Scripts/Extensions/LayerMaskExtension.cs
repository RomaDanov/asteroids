using UnityEngine;

namespace Extensions
{
	public static class LayerMaskExtension
	{
		public static bool Contains(this LayerMask mask, int layer)
		{
			return mask == (mask | 1 << layer);
		}
	}
}