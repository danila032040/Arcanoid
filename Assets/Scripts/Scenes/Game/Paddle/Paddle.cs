namespace Scripts.Scenes.Game.Paddle
{
    using Scripts.Scenes.Game.Ball;
    using UnityEngine;
    public class Paddle : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

                ballRb.velocity = Vector3.zero;

                ballRb.AddForce((Vector3.left + Vector3.up).normalized * ball.GetCurrentVelocity());

            }
        }
    }
}
