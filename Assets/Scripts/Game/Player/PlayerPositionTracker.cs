using UnityEngine;
using UnityEngine.UI;  

public class PlayerPositionTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider positionSlider; 
    private Transform playerTransform;      
    private Transform finishTransform;       
    private float maxDistance;     

    void Start()
    {
        Invoke("FindPlayerAndFinish", 0.2f);
    }

    void FindPlayerAndFinish()
    {
        GameObject player = GameObject.FindGameObjectWithTag("CameraTarget");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
        GameObject finish = GameObject.FindGameObjectWithTag("Finish");
        if (finish != null)
        {
            finishTransform = finish.transform;
            maxDistance = finishTransform.position.x;
        }
        else
        {
            Debug.LogWarning("Finish not found!");
        }
    }

    void Update()
    {
        if (playerTransform != null && positionSlider != null)
        {
            float playerX = playerTransform.position.x;
            if (maxDistance > 0)
            {
                positionSlider.value = Mathf.Clamp01(playerX / maxDistance);
            }
        }
    }
}
