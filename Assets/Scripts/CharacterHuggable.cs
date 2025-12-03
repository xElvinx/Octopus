using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterHuggable : CharacterStun
{
	public float timeGapBetweenButtonPress = 1f;
	public int requiredLeftRightPress = 5;
	public float knockbackForce = 20.0f;

	public TopDownController Hugger { get; private set; }
	
	private int _leftRightCounter;
	private float _prevX;
	private float _t;
	private bool _isInit;

	/// <summary>
	/// Called at the very start of the ability's cycle, and intended to be overridden, looks for input and calls methods if conditions are met
	/// </summary>
	protected override void HandleInput()
	{
		if (!Hugger)
		{
			return;
		}
		
		float movementX = _inputManager.PrimaryMovement.x;
		
		if (!_isInit)
		{
			_t = 0.0f;
			_isInit = true;
			_prevX = movementX;
			_leftRightCounter = 0;
			return;
		}
		
		if (_t > timeGapBetweenButtonPress)
		{
			_isInit = false;
			return;
		}
		
		if (movementX != 0 && (int)Mathf.Sign(movementX) != (int)Mathf.Sign(_prevX))
		{
			_leftRightCounter++;
			_prevX = movementX;
			_t = 0.0f;
		}
		
		if (_leftRightCounter >= requiredLeftRightPress)
		{
			var huggerPosition = Hugger.transform.position;
			var selfPosition = transform.position;

			Vector3 knockbackDirection = new Vector3(
				Random.value,
				huggerPosition.y,
				Random.value
			).normalized;

			Hugger.CollisionsOn();
			Hugger.Impact(knockbackDirection, knockbackForce);
			//Hugger.CollisionsOff();
			
			_isInit = false;
			return;
		}
		
		_t += Time.deltaTime;
	}

	public void Hug(TopDownController hugger)
	{
		Hugger = hugger;
		Hugger.CollisionsOff();
		var huggerTransform = Hugger.transform;
		var characterPosition = transform.position;
		huggerTransform.position = new Vector3(
			characterPosition.x,
			huggerTransform.position.y,
			characterPosition.z - 0.1f
		);
		base.Stun();
	}

	public void UnHug()
	{
		Hugger.CollisionsOn();
		Hugger = null;
		base.ExitStun();
	}
}
