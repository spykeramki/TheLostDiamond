using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public enum MoveDirection
    {
        FORWARD,
        BACKWARD
    }

    private SpriteRenderer _spriteRenderer;

    private float _moveSpeed = 10f;
    public MoveDirection fishDirection;

    private MoveDirection[] moveDirections = new MoveDirection[2] { MoveDirection.FORWARD, MoveDirection.BACKWARD };

    private bool isOutOfBounds = false;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveSpeed = Random.Range(5f, 15f);
        fishDirection = moveDirections[ Random.Range(0, moveDirections.Length)];
        float randomScale = Random.Range(1f, 2f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (!FishManager.instance.FishBounds.Contains(this.transform.position))
        {
            isOutOfBounds = true;
        }
        else
        {
            isOutOfBounds = false;
        }

        if (isOutOfBounds)
        {
            fishDirection = (fishDirection == MoveDirection.FORWARD) ? MoveDirection.BACKWARD : MoveDirection.FORWARD;
        }

        float fishMoveSpeed;
        if(fishDirection == MoveDirection.FORWARD)
        {
            _spriteRenderer.flipX = true;
            fishMoveSpeed = _moveSpeed;
        }
        else
        {
            _spriteRenderer.flipX = false;
            fishMoveSpeed = -1 * _moveSpeed;
        }
        transform.Translate(fishMoveSpeed * Time.deltaTime, 0f, 0f);
    }
}
