using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float timeBetweenShips = 3.0f;
    [SerializeField] Transform top;
    [SerializeField] Transform bot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShips);
            Vector3 position =
                Vector3.Lerp(top.position, bot.position, Random.Range(0.0f, 1.0f));
            Instantiate(prefabEnemy, position, Quaternion.identity);
        }
    }
    
}
