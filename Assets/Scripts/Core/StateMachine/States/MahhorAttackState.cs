using System;
using System.Collections;
using UnityEngine;

public class MahhorAttackState : MahhorState
{
    public MahhorAttackState(MahhorStateMachine unit) : base(unit) { }

    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 targetPosition;
    private Vector3 attackPointReference;
    private float animationLength;
    private bool isWalking;
    private bool teleportFinished = false;
    private int activationAmount = 0;
    private int animationCount = 0;
    string animName = "";

    public override void Enter()
    {
        activationAmount = 0;
        animationCount = 0;
        teleportFinished = false;
        isWalking = true;

        player = GameObject.FindGameObjectWithTag("Player");
        FindPlayerToWalk();

        if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            isWalking = false;
            unit.StartCoroutine(TeleportTo(targetPosition));
        }
    }

    public override void Tick()
    {
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            if (teleportFinished && activationAmount == 0)
            {
                activationAmount++;

                if (!string.IsNullOrEmpty(animName))
                {
                    unit.GetAnimator().Play(animName, 0);

                    AnimationClip clip = Array.Find(
                        unit.GetAnimator().runtimeAnimatorController.animationClips,
                        c => c.name == animName
                    );
                    animationLength = clip != null ? clip.length : 1f;

                    unit.FindPlayerOnAttack.FindPlayer(unit.Transforms.position + attackPointReference);
                    unit.StartCoroutine(WaitForAnimation(animationLength));
                }
            }
        }
        else // Default
        {
            if (Vector3.Distance(unit.Transforms.position, targetPosition) < 0.05f && !isWalking && activationAmount == 0)
            {
                activationAmount++;

                if (!string.IsNullOrEmpty(animName))
                {
                    unit.GetAnimator().Play(animName, 0);

                    AnimationClip clip = Array.Find(
                        unit.GetAnimator().runtimeAnimatorController.animationClips,
                        c => c.name == animName
                    );
                    animationLength = clip != null ? clip.length : 1f;

                    unit.FindPlayerOnAttack.FindPlayer(unit.Transforms.position + attackPointReference);
                    unit.StartCoroutine(WaitForAnimation(animationLength));
                }
            }
            else if (unit.Transforms.position == targetPosition && isWalking)
            {
                isWalking = false;
            }
            else
            {
                MoveTo(targetPosition);
            }
        }
    }

    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true;
    }

    private void FindPlayerToWalk()
    {
        playerPosition = player.transform.position;
        Vector2 direction = playerPosition - (Vector3)unit.Transforms.position;

        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x >= 0)
                {
                    animName = "MahhorBasicAttackRight";
                    targetPosition = playerPosition + Vector3.left;
                    attackPointReference = Vector3.right;
                }
                else
                {
                    animName = "MahhorBasicAttackLeft";
                    targetPosition = playerPosition + Vector3.right;
                    attackPointReference = Vector3.left;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    animName = "MahhorBasicAttackUp";
                    targetPosition = playerPosition + Vector3.down;
                    attackPointReference = Vector3.up;
                }
                else
                {
                    animName = "MahhorBasicAttackDown";
                    targetPosition = playerPosition + Vector3.up;
                    attackPointReference = Vector3.down;
                }
            }
        }
        else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            if (direction.x >= 0)
            {
                animName = "MahhorAttackRight";
                targetPosition = playerPosition + Vector3.left;
                attackPointReference = Vector3.right;
            }
            else
            {
                animName = "MahhorAttackLeft";
                targetPosition = playerPosition + Vector3.right;
                attackPointReference = Vector3.left;
            }
        }
    }

    private void MoveTo(Vector2 targetPosition)
    {
        if (animationCount == 0)
        {
            animationCount++;
            switch (animName)
            {
                case "MahhorBasicAttackRight":
                    unit.GetAnimator().Play("MahhorWalkingRight", 0);
                    break;
                case "MahhorBasicAttackLeft":
                    unit.GetAnimator().Play("MahhorWalkingLeft", 0);
                    break;
                case "MahhorBasicAttackUp":
                    unit.GetAnimator().Play("MahhorWalkingUp", 0);
                    break;
                case "MahhorBasicAttackDown":
                    unit.GetAnimator().Play("MahhorWalkingDown", 0);
                    break;
            }
        }

        unit.Transforms.position = Vector2.MoveTowards(
            unit.Transforms.position,
            targetPosition,
            unit.Status.currentMoveSpeed * Time.deltaTime
        );
    }

    private IEnumerator TeleportTo(Vector2 targetPosition)
    {
        unit.Transforms.position = targetPosition;
        yield return new WaitForSeconds(0.65f);
        teleportFinished = true;
    }

    private IEnumerator WaitForAnimation(float duration)
    {
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            yield return new WaitForSeconds(duration);
            unit.ChangeState<MahhorMoveState>();
        }
        else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            yield return new WaitForSeconds(duration + 4f);
            unit.ChangeState<MahhorMoveState>();
        }
    }
}
