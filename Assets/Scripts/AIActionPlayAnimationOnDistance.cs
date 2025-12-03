using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionPlayAnimationOnDistance : AIAction
{
	[Tooltip("the name of the animation parameter : this will be true once target in specific distance")]
	public string startAnimationParameterName;
	public string continueAnimationParameterName;
	public string stopAnimationParameterName;

	[Tooltip("trigger distance")]
	public float distance;

	private Character _character;
	private Animator _animator;

	private int _startAnimationParameter;
	private int _continueAnimationParameter;
	private int _stopAnimationParameter;
	private float _prevDistance;
	
	
	/// <summary>
	/// On init we grab our CharacterMovement ability
	/// </summary>
	public override void Initialization()
	{
		_character = this.gameObject.GetComponentInParent<Character>();
		_animator = _character._animator;
		RegisterAnimatorParameter (startAnimationParameterName, AnimatorControllerParameterType.Float, out _startAnimationParameter);
		RegisterAnimatorParameter (continueAnimationParameterName, AnimatorControllerParameterType.Float, out _continueAnimationParameter);
		RegisterAnimatorParameter (stopAnimationParameterName, AnimatorControllerParameterType.Float, out _stopAnimationParameter);
	}
	
	public override void PerformAction()
	{
		if (!_brain.Target)
		{
			return;
		}

		float currentDistance = Vector3.Distance(_brain.Target.position, transform.position);

		if (currentDistance < distance && _prevDistance >= distance)
		{
			MMAnimatorExtensions.UpdateAnimatorBool(_animator, _stopAnimationParameter, true,_character._animatorParameters, _character.RunAnimatorSanityChecks);
		}
		else if (currentDistance >= distance && _prevDistance < distance)
		{
			MMAnimatorExtensions.UpdateAnimatorBool(_animator, _startAnimationParameter, true,_character._animatorParameters, _character.RunAnimatorSanityChecks);
		}
		
		MMAnimatorExtensions.UpdateAnimatorBool(_animator, _continueAnimationParameter, currentDistance < distance,_character._animatorParameters, _character.RunAnimatorSanityChecks);
	}
	
	/// <summary>
	/// Describes what happens when the brain exits the state this action is in. Meant to be overridden.
	/// </summary>
	public virtual void OnExitState()
	{
		base.OnEnterState();
		MMAnimatorExtensions.UpdateAnimatorBool(_animator, _continueAnimationParameter, false,_character._animatorParameters, _character.RunAnimatorSanityChecks);
	}
	
	
	/// <summary>
	/// Registers a new animator parameter to the list
	/// </summary>
	/// <param name="parameterName">Parameter name.</param>
	/// <param name="parameterType">Parameter type.</param>
	protected virtual void RegisterAnimatorParameter(string parameterName, AnimatorControllerParameterType parameterType, out int parameter)
	{
		parameter = Animator.StringToHash(parameterName);

		if (_animator == null) 
		{
			return;
		}
		if (_animator.MMHasParameterOfType(parameterName, parameterType))
		{
			if (_character != null)
			{
				_character._animatorParameters.Add(parameter);	
			}
		}
	}
}
