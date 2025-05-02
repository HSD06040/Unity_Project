using UnityEngine;

public class PlayerState
{
    protected Player player;              
    protected StateMachine stateMachine;  
    protected Animator anim => player.anim;
    protected Rigidbody rb => player.rigid;
    protected StateController stateCon => player.stateCon;
    private string animBoolName;
    protected float stateTimer;
    protected bool isFinishAnim;

    public PlayerState(Player _player, StateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        isFinishAnim = false;
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if(stateTimer >= 0)
            stateTimer -= Time.deltaTime;
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void Transition()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(stateCon.rollState);
    }

    public void OnAnimatorMove()
    {
        if (anim.applyRootMotion)
        {
            Vector3 delta = anim.deltaPosition;
            delta.y = 0f;
            player.transform.position += delta;
        }
    }

    public void AnimFinishEvent() => isFinishAnim = true;
}
