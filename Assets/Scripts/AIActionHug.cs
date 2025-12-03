using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionHug : AIActionMoveTowardsTarget3D
{
    public DamageOnTouch damageOnTouch;
    protected CharacterHuggable _target;
    private TopDownController _topDownController;
    
    /// <summary>
    /// Initializes the action. Meant to be overridden
    /// </summary>
    public override void Initialization()
    {
        base.Initialization();
        _topDownController = gameObject.MMGetComponentNoAlloc<TopDownController>();
    }
    
    public override void PerformAction()
    {
        base.PerformAction();

        float distance = Vector3.Distance(this.transform.position, _brain.Target.position);
        if (!_target.Hugger && distance < MinimumDistance + 0.5f)
        {
            transform.parent = _brain.Target;
            _target.Hug(_topDownController);
            damageOnTouch.enabled = true;
        }
        else if (_target.Hugger == _topDownController && distance > MinimumDistance)
        {
            
            transform.parent = null;
            _target.UnHug();
            damageOnTouch.enabled = false;
            _brain.ResetBrain();
        }
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        _target = _brain.Target.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterHuggable>();
    }
    
    /// <summary>
    /// Describes what happens when the brain exits the state this action is in. Meant to be overridden.
    /// </summary>
    public override void OnExitState()
    {
        base.OnExitState();
        if (_target.Hugger == _topDownController)
        {
            transform.parent = null;
            _target.UnHug();
            damageOnTouch.enabled = false;
        }
    }
}