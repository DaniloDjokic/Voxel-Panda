using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { UP, DOWN, LEFT, RIGHT }
public class ConveyorBehaviour : LocalUpdater<Rigidbody>
{
    public float speed;
    public Direction direction;

    private void Start()
    {
        SetRandomDirection();
    }

    private void FixedUpdate()
    {
        AddForceToRBs();
    }

    public void SetRandomDirection()
    {
        this.direction = (Direction)Random.Range(2, 4);
    }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
    }

    void AddForceToRBs()
    {
        foreach(Rigidbody rb in currentlyContained)
        {
            rb.AddForce(GetDirection(direction) * speed * Time.deltaTime);
        }
    }

    protected override void PlayerEnters(GameObject player)
    {
        base.PlayerEnters(player);
    }

    protected override void PlayerExits(GameObject player)
    {
        base.PlayerExits(player);
    }

    Vector3 GetDirection(Direction direction)
    {
        float y = ConstMoveData.universalY;

        switch(direction)
        {
            case Direction.UP:
                return new Vector3(0, y, 1);
            case Direction.DOWN:
                return new Vector3(0, y, -1);
            case Direction.LEFT:
                return new Vector3(-1, y, 0);
            case Direction.RIGHT:
                return new Vector3(1, y, 0);
            default:
                Debug.LogError("Invalid direction");
                return Vector3.zero;
        }
    }
}
