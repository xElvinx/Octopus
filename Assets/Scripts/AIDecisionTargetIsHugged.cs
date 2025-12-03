using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIDecisionTargetIsHugged : AIDecision
{
	public override bool Decide()
	{
		var targetHug = _brain.Target.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterHuggable>();
		if (targetHug != null && targetHug.Hugger)
		{
			if (targetHug.Hugger.gameObject == this.gameObject)
			{
				return false;
			}
			return true;
		}

		return false;
	}
}
