using System.Collections.Generic;

namespace Contexts.Game.Components.Zone
{
	public class Deadzone : Zone<IDeadzoneVisitor>
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