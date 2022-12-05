using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float maxForce = 16;
    private float minForce = 12;
    private float torque = 10;
    private float xRange = 4;
    private float ySpawn = -3;
    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(),ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);

        if(gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(-1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.UpdateLives(-1); //Reduces 1 life
        }
        Destroy(gameObject);
    }

    // Generating random force
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    // Generating fandom torque
    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    // Generating random position for objects to spawn
    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawn);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position,
            explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}
