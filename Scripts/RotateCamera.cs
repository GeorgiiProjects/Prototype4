using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // создаем переменную для скорости вращения камеры, скорость вращения доступна в инспекторе.
    public float rotationSpeed;
    
    void Update()
    {
        // Input.GetAxis("Horizontal"); получаем доступ к менеджеру в инспекторе Project settings - Input Manager - Axes - Horizontal - Name (Horizontal)
        // Теперь видим в инспекторе что значения движения меняются от -1 до 1, но камера не двигается.
        float horizontalInput = Input.GetAxis("Horizontal");

        // Контролируем скорость плавного вращения камеры вокруг оси y, на любом пк.
        // Заставляем вращаться плавно камеру на любом пк со скоростью rotationSpeed при нажатии влево и вправо.
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
