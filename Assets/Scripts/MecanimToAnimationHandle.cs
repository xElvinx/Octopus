using Spine.Unity.Examples;
using UnityEngine;

public class MecanimToAnimationHandle : StateMachineBehaviour {
    SkeletonAnimationHandle animationHandle;
    bool initialized;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized) {
            animationHandle = animator.GetComponent<SkeletonAnimationHandle>();
            initialized = true;
        }

        animationHandle.PlayAnimationForState(stateInfo.shortNameHash, layerIndex);
    }
}
