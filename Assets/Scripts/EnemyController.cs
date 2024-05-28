using UnityEngine;
using Photon.Pun;

public class EnemyController : MonoBehaviour
{
    private GameObject[] players;
    private GameObject targetPlayer;
    private bool reachedPlayer = false;
    PhotonView photonView;
    Rigidbody2D rb;
    public float speed = 2.0f;
    TimeoutManager timeoutManager;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        timeoutManager = gameObject.AddComponent<TimeoutManager>();
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        targetPlayer = GetClosestPlayer();
        MoveTowardsPlayer();
    }

    GameObject GetClosestPlayer()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }

    void MoveTowardsPlayer()
    {
        if (targetPlayer != null && !reachedPlayer)
            rb.velocity = (targetPlayer.transform.position - transform.position).normalized * speed;
        else
            rb.velocity = Vector2.zero;
    }

    public void TakeDamage()
    {
        RequestDestroyEnemy(photonView);
        GameObject scoreValue = GameObject.Find("ScoreValue");
        scoreValue.GetComponent<Score>().IncreaseScore();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            reachedPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeoutManager.SetTimeout(() => reachedPlayer = false, 1f);
        }
    }

    void RequestDestroyEnemy(PhotonView enemyPhotonView)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("DestroyNetworkObject", RpcTarget.MasterClient, enemyPhotonView.ViewID);
        }
        else
        {
            PhotonNetwork.Destroy(enemyPhotonView.gameObject);
        }
    }
}
