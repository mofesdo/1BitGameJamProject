using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Bat : MonoBehaviour
    {
        public float moveSpeed = 3f;

        private Vector2 initialPosition;
        private bool isMovingRight = true;

        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            MoveLeftAndRight();
        }

        private void MoveLeftAndRight()
        {
            Vector2 targetPosition;

            if (isMovingRight)
            {
                targetPosition = new Vector2(initialPosition.x + 5f, transform.position.y);
            }
            else
            {
                targetPosition = new Vector2(initialPosition.x - 5f, transform.position.y);
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMovingRight = !isMovingRight;
            }
        }
    }
}