using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    #region Defines

    private static readonly int VELOCITY_X = Animator.StringToHash("velocityX");
    private static readonly int GROUNDED = Animator.StringToHash("grounded");
    private static readonly int HURT = Animator.StringToHash("hurt");
    private static readonly int DEAD = Animator.StringToHash("dead");
    private static readonly int VICTORY = Animator.StringToHash("victory");

    #endregion

    #region Component Configs

    private Animator animator;

    #endregion

    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Move(float speed)
    {
        animator.SetFloat(VELOCITY_X, speed);
    }

    public void Jump(bool isGrounded)
    {
        animator.SetBool(GROUNDED, isGrounded);
    }

    public void Hurt()
    {
        animator.SetTrigger(HURT);
    }

    public void Dead(bool isDead)
    {
        animator.SetBool(DEAD, isDead);
    }

    public void Victory()
    {
        animator.SetTrigger(VICTORY);
    }
}
