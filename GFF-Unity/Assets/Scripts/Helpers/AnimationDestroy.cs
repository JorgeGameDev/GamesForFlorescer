using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for destroying or disabling objects at the end of animations.
public class AnimationDestroy : MonoBehaviour {

	// Called by the Animation, destroys this object.
	public void DestroyAnimation()
    {
        Destroy(gameObject);
    }

    // Called by the animation, disables this object.
    public void DisableAnimation()
    {
        gameObject.SetActive(false);
    }
}
