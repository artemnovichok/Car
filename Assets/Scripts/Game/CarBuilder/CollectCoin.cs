using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Coin"))
        {
            Destroy(coll.gameObject);
            CollectedCoin collCoin = GameObject.FindFirstObjectByType<CollectedCoin>();
            collCoin.collectedCoinOnLevel += coll.gameObject.GetComponent<Coin>().GetCoinValue();
            EventManager.Instance.UpdateCoinTextOnLevel();
        }
    }
}
