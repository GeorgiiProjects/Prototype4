using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // создаем класс Rigidbody, для последующего доступа к нему в Player.
    private Rigidbody playerRb;
    // создаем класс GameObject, для последующего доступа к нему в иерархии unity.
    private GameObject focalPoint;
    // скорость передвижения Player 5, можно настроить в инспекторе.
    public float playerSpeed = 5.0f;
    // создаем переменную, чтобы определить поднят ли префаб PowerUp.
    public bool hasPowerup = false;
    // создаем переменную, чтобы знать с какой силой будет отлетать префаб enemy, после поднятия префаба PowerUp.
    private float powerupStrength = 15.0f;
    // создаем GameObject powerupIndicator, который виден в инспекторе, чтобы поместить в префаб Player - префаб Powerup Indicator.
    public GameObject powerupIndicator;

    void Start()
    {
        // получаем доступ к Rigidbody префаба Player через компонент Rigidbody GetComponent
        playerRb = GetComponent<Rigidbody>();
        // получаем доступ к focalPoint в иерархии unity через класс GameObject.Find и имя объекта "Focal Point" 
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Input.GetAxis("Vertical"); получаем доступ к менеджеру в инспекторе Project settings - Input Manager - Axes - Vertical - Name (Vertical).
        // Теперь видим в инспекторе что значения движения меняются от -1 до 1, но префаб Player не двигается.
        float forwardInput = Input.GetAxis("Vertical");

        // добавляем к Player Rigidbody силу передвижения Player AddForce,
        // forwardInput (передвижение вверх и вниз) умножаем на playerSpeed скорость передвижения Player. 
        // focalPoint.transform.forward позиция камеры в сторону которой будет передвигаться Player.
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed);
        // привязываем координаты префаба Powerup Indicator к координатам префаба Player и опускаем его позицию на -0.5 по оси y.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    // создаем метод взаимодействия boxcollider Player с boxcollider Powerup
    private void OnTriggerEnter(Collider other)
    {
        // если коллайдер Player соприкасается с префабом PowerUp у которого тэг "Powerup"
        if (other.CompareTag("Powerup"))
        {
            // префаб Powerup поднимается префабом Player.
            hasPowerup = true;
            // коллайдер игрового объекта Powerup уничтожается и не виден больше в иерархии.
            Destroy(other.gameObject);
            // запускам курутину, время усиления на 7 секунд, после поднятия усиления.
            StartCoroutine(PowerupCountdownRoutine());
            // Powerup Indicator становится активным и виден в иерархии 7 секунд.
            powerupIndicator.gameObject.SetActive(true);
        }       
    }

    // создаем интерфейс/курутину для использования таймера вне метода Update().
    IEnumerator PowerupCountdownRoutine()
    {
        // время усиления длится 7 секунд.
        yield return new WaitForSeconds(7);
        // усиление заканчивается через 7 секунд.
        hasPowerup = false;
        // Powerup Indicator становится неактивным и не виден в иерархии через 7 секунд.
        powerupIndicator.gameObject.SetActive(false);
    }

    // используем OnCollisionEnter за место OnTriggerEnter, так как будем взаимодействовать с физикой (OnTriggerEnter взаимодействует с boxcollider)
    private void OnCollisionEnter(Collision other)
    {
        // если префаб Player соприкасается с префабом Enemy содержащим тэг "Enemy" и при этом взял префаб Powerup.
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // получаем доступ к Rigidbody Enemy, для последующего взаимодействия с ним Player.
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            // при использовании данной формулы при столкновении с усиленным player, enemy будет отлетать.
            // other.gameObject.transform.position - transform.position (позиция enemy - позиция player).
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            // добавляем к enemyRigidbody силу отталкивания в 15 раз от Player, используем ForceMode.Impulse, чтобы это произошло мгновенно.
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }       
    }
}
