using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private TimeoutManager timeoutManager;

    private void Start()
    {
        SpawnPlayer();
        timeoutManager = gameObject.AddComponent<TimeoutManager>();
    }

    private void Update()
    {
        GameObject playersStillAlive = GameObject.FindWithTag("Player");
        if (playersStillAlive == null)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("GameOver");
            return;
        }
    }

    private void SpawnPlayer(bool updateHealthText = false)
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        if (updateHealthText)
        {
            GameObject healthValue = GameObject.Find("HealthValue");
            healthValue.GetComponent<Health>().GetCorrespondingPlayer();
        }

    }

    public void SpawnPlayerAfterDeath()
    {
        timeoutManager.SetTimeout(() => SpawnPlayer(true), 10f);
    }
}
