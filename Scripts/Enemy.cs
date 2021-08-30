using UnityEngine;

public class Enemy : MonoBehaviour
{
    // скорость передвижения префаба Enemy 3.
    public float enemySpeed = 3.0f;
    // создаем класс Rigidbody, для передвижения объекта и для последующего доступа к нему.
    private Rigidbody enemyRb;
    // создаем класс GameObject, для последующего доступа к нему в иерархии unity.
    private GameObject player;

    void Start()
    {
        // получаем доступ к Rigidbody префаба Enemy через компонент Rigidbody GetComponent.
        enemyRb = GetComponent<Rigidbody>();
        // получаем доступ к player в иерархии unity через класс GameObject.Find и строку "Player" 
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Enemy двигается вслед за Player.
        // для этого используем позицию Player - позицию Enemy (получаем Vector3 позиции по которой должен двигаться Enemy) * скорость передвижения Enemy.
        // normalized делает так, чтобы Enemy двигался с определенной скоростью, в не зависимости от расстояния до Player.
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        // добавляем силу AddForce, которая будет двигать Enemy в направлении Player.      
        enemyRb.AddForce(lookDirection * enemySpeed);

        // если позиция enemy по оси y < -10 
        if(transform.position.y < -10)
        {
            // уничтожаем enemy.
            Destroy(gameObject);
        }
    }
}
