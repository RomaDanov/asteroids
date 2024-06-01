using Contexts.Game.Components.Fence;
using System.Collections.Generic;

namespace Contexts.Game.Components.Fence
{
	public class Deadzone : Fence<IDeadzoneVisitor>
	{
		protected override void VisitObjects(List<IDeadzoneVisitor> visitors)
		{
			for (int i = 0; i < visitors.Count; i++)
			{
				IDeadzoneVisitor visitor = visitors[i];
				visitor.Visit();
			}
		}
	}
}