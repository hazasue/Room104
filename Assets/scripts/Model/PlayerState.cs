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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;
        if (direction.magnitude > 0)
            player.direction = direction;
      
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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = direction.normalized;
        if (direction.magnitude > 0)
            player.direction = direction;    
    }

    public override void FixedUpdate(Player player) 
    {
        player.transform.position += new Vector3(direction.x, direction.y, 0f) * Time.deltaTime * DEFAULT_MOVE_SPEED_RUN;
    }

    public override void end(Player player) { }
}

public class InteractState : PlayerState
{
    bool isFind = false;
    private string objectName = "";
    public override void start(Player player)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.direction, 2.0f);
        if (hit.collider != null)
        {
            isFind = true;
            objectName = hit.collider.name;
            Debug.Log("실행됨");
            Debug.Log(hit.collider.name);
            //오브젝트에 따라 행동 변경 추가.
        }
    }

    public override PlayerState TransitionState(eKeyAction action)
    {
        if(isFind == false)
        {
            return new IdleState();
        }
        else
        {
            InputManager.Instance.ChangeUIToggle(true);
            if(objectName == "Desk")
            {
                return new StudyState();
            }
            else if(objectName == "Chair")
            {
                return new ListeningMusicState();
            }
            else if (objectName == "Bed")
            {
                return new RestState();
            }
            else if(objectName == "Park")
            {
                return new ExerciseState();
            }

        }
        return null;
    }

    public override void Update(Player player) { }
    public override void FixedUpdate(Player player) { }
    public override void end(Player player) { }
}

public abstract class BehaviorState : PlayerState
{
    protected float time = 0.0f;
    public abstract override void start(Player player);
    public override PlayerState TransitionState(eKeyAction action)
    {
        if(action == eKeyAction.Interact)
        {
            return new IdleState();
        }
        return null;
    }
    public override void Update(Player player)
    {
        time += Time.deltaTime;
    }
    public abstract override void FixedUpdate(Player player);
    public abstract override void end(Player player);
}

public class ExerciseState : BehaviorState
{
    public override void start(Player player)
    {

    }

    public override void Update(Player player)
    {
        base.Update(player);
        if(time >= 5.0f)
        {
            time = 0.0f;
            player.Stat.IncreaseStamina();
            player.Stat.DecreaseHealth();
            player.Stat.DecreaseStress();
            player.StatEventHandler();
            player.TimeEventHandler();
        }
    }

    public override void FixedUpdate(Player player)
    {

    }

    public override void end(Player player)
    {

    }
}

public class StudyState : BehaviorState
{
    public override void start(Player player)
    {

    }

    public override void Update(Player player)
    {
        base.Update(player);
        if (time >= 5.0f)
        {
            time = 0.0f;
            player.Stat.IncreaseIntelligence();
            player.Stat.DecreaseHealth();
            player.Stat.IncreaseStress();
            player.StatEventHandler();
            player.TimeEventHandler();
        }
    }

    public override void FixedUpdate(Player player)
    {

    }

    public override void end(Player player)
    {

    }
}

public class RestState : BehaviorState
{
    public override void start(Player player)
    {

    }

    public override void Update(Player player)
    {
        base.Update(player);
        if (time >= 5.0f)
        {
            time = 0.0f;
            player.Stat.DecreaseIntelligence();
            player.Stat.DecreaseHealth();
            player.Stat.DecreaseStress();
            player.StatEventHandler();
            player.TimeEventHandler();
        }
    }

    public override void FixedUpdate(Player player)
    {

    }

    public override void end(Player player)
    {

    }
}

public class ListeningMusicState : BehaviorState
{
    public override void start(Player player)
    {

    }

    public override void Update(Player player)
    {
        base.Update(player);
        if (time >= 5.0f)
        {
            time = 0.0f;
            player.Stat.DecreaseIntelligence();
            player.Stat.DecreaseHealth();
            player.Stat.DecreaseStress();
            player.StatEventHandler();
            player.TimeEventHandler();
        }
    }

    public override void FixedUpdate(Player player)
    {

    }

    public override void end(Player player)
    {

    }
}

public class SleepState : BehaviorState
{
    public override void start(Player player)
    {
        player.Stat.IncreaseHealth();
        player.StatEventHandler();
        player.TimeEventHandler();
    }

    public override void Update(Player player)
    {
        base.Update(player);
    }

    public override void FixedUpdate(Player player)
    {

    }

    public override void end(Player player)
    {

    }
}
