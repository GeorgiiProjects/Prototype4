using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // создаем GameObject enemyPrefab, для чтобы поместить в него префаб Enemy в инспекторе.
    public GameObject enemyPrefab;
    // создаем GameObject powerupPrefab, для чтобы поместить в него префаб PowerUp в инспекторе.
    public GameObject powerupPrefab;
    // создаем переменную для рандомного спавна мобов по координатам от -9 до 9.
    private float spawnRange = 9.0f;
    // создаем переменую для отслеживания количества врагов на экране.
    public int enemyCount;
    // создаем переменную для подсчета номера волны.
    public int waveNumber = 1;

    void Start()
    {
        // Вызываем метод SpawnEnemyWave() в методе Start(), иначе префабы enemy не будут появляться, количество появляющихся enemy со старта 1.
        SpawnEnemyWave(waveNumber);
        // Instantiate создаем клоны powerupPrefab, в позиции координат new Vector3 используем метод рандомного спавна GenerateSpawnPosition(), 
        // powerupPrefab.transform.rotation оставляем по умолчанию.
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    void Update()
    {
        // Получаем доступ к скрипту Enemy и ищем количество всех врагов на экране через массив.
        enemyCount = FindObjectsOfType<Enemy>().Length;
        // Если количество врагов равно 0.
        if (enemyCount == 0)
        {
            // количество врагов с каждой волной увеличивается на один.
            waveNumber++;
            // Вызываем метод SpawnEnemyWave() в методе Update(), иначе следующая волна префабов Enemy не будут появляться.
            SpawnEnemyWave(waveNumber);
            // Instantiate создаем клоны powerupPrefab, в позиции координат new Vector3 используем метод рандомного спавна GenerateSpawnPosition(), 
            // powerupPrefab.transform.rotation оставляем по умолчанию.
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    // создаем метод SpawnEnemyWave() с параметром int enemiesTospawn, чтобы спавнить в нем нужное количество префабов Enemy в каждой волне.
    void SpawnEnemyWave(int enemiesTospawn)
    {
        // используем цикл for для появления нужного количества префабов Enemy в каждой волне, 
        // в нашем случае от одного в первой волне и увеличиваем на 1 в последующих волнах.
        for (int i = 0; i < enemiesTospawn; i++)
        {
            // Instantiate создаем клоны префабов enemy, в позиции координат new Vector3 используем метод рандомного спавна GenerateSpawnPosition(), 
            // enemyPrefab.transform.rotation оставляем по умолчанию.
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Используем метод GenerateSpawnPosition() для рандомного спавна мобов и усилений по оcям x,y,z. 
    private Vector3 GenerateSpawnPosition()
    {
        // переменная по оси х с рандомным спавном от -9 до 9
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        // переменная по оси z с рандомным спавном от -9 до 9
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        // создаем клоны префабов Enemy и Powerup, в позиции случайных координат по осям x,z и в позиции 0 по оси y.
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        // можем использовать случайную генерацию префабов Enemy и PowerUp в игре.
        return randomPos;
    }
}
