using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void start(Player player);
    public abstract PlayerState TransitionState(eKeyAction action);
    public abstract void Update(Player player);
    public abstract void FixedUpdate(Player player);
    public abstract void end(Player player);
}

public class IdleState : PlayerState
{
    public override void start(Player player)
    {
        
    }

    public override PlayerState TransitionState(eKeyAction action)
    {
        switch (action)
        {
            case eKeyAction.Move:
                return new WalkState();
            case eKeyAction.Run:
                return new RunState();
            case eKeyAction.Interact:
                return new InteractState();
            default:
                return null;
        }
    }

    public override void Update(Player player) {}
    public override void FixedUpdate(Player player) {}
    public override void end(Player player) {}
}

public class WalkState : PlayerState
{
    Vector2 direction;
    private const float DEFAULT_MOVE_SPEED_WALK = 3.0f;

    public override void start(Player player)
    {
        direction = new Vector2(0,0);
    }

    public override PlayerState TransitionState(eKeyAction action)
    {
        switch (action)
        {
            case eKeyAction.None:
                return new IdleState();
            case eKeyAction.Run:
                return new RunState();
            case eKeyAction.Interact:
                return new InteractState();
            default:
                return null;
        }
    }

    public override void Update(Player player)
    {
        direction += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;       
    }
    
    public override void FixedUpdate(Player player) 
    {
        player.transform.position += new Vector3(direction.x, direction.y, 0f) * Time.deltaTime * DEFAULT_MOVE_SPEED_WALK;
    }

    public override void end(Player player) { }
}

public class RunState : PlayerState
{
    Vector2 direction;
    private const float DEFAULT_MOVE_SPEED_RUN = 6.0f;

    public override void start(Player player)
    {

    }

    public override PlayerState TransitionState(eKeyAction action)
    {
        switch (action)
        {
            case eKeyAction.None:
                return new IdleState();
            case eKeyAction.Move:
                return new WalkState();
            case eKeyAction.Interact:
                return new InteractState();
            default:
                return null;
        }
    }

    public override void Update(Player player)
    {
        direction += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;       
    }

    public override void FixedUpdate(Player player) 
    {
        player.transform.position += new Vector3(direction.x, direction.y, 0f) * Time.deltaTime * DEFAULT_MOVE_SPEED_RUN;
    }

    public override void end(Player player) { }
}

public class InteractState : PlayerState
{
    public override void start(Player player)
    {

    }

    public override PlayerState TransitionState(eKeyAction action)
    {
        return new IdleState();
    }

    public override void Update(Player player) { }
    public override void FixedUpdate(Player player) { }
    public override void end(Player player) { }
}