using UnityEngine;

public class CarBlocksSettings : MonoBehaviour
{
    public CarBlock blockSettings = new CarBlock();

    private void OnEnable() => EventManager.Instance.updateBlocksInGrid.AddListener(UpdateJoint);

    private void OnDisable() => EventManager.Instance.updateBlocksInGrid.RemoveListener(UpdateJoint);

    private float koeff = 2f;


    private void UpdateJoint()
    {
        if (blockSettings.weConnectUp)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.up, koeff, LayerMask.GetMask("CarBlock"));
            bool stop = false;
            foreach (RaycastHit ray in hit)
            {
                if (ray.collider.gameObject != gameObject && stop == false)
                {
                    GameObject go = ray.collider.gameObject;
                    FixedJoint fixJ = gameObject.AddComponent<FixedJoint>();
                    fixJ.connectedBody = go.GetComponent<Rigidbody>();
                    stop = true;
                }
            }
        }
        if (blockSettings.weConnectDown)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.down, koeff, LayerMask.GetMask("CarBlock"));
            bool stop = false;
            foreach (RaycastHit ray in hit)
            {
                if (ray.collider.gameObject != gameObject && stop == false)
                {
                    GameObject go = ray.collider.gameObject;
                    FixedJoint fixJ = gameObject.AddComponent<FixedJoint>();
                    fixJ.connectedBody = go.GetComponent<Rigidbody>();
                    stop = true;
                }
            }
        }
        if (blockSettings.weConnectLeft)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.left, koeff, LayerMask.GetMask("CarBlock"));
            bool stop = false;
            foreach (RaycastHit ray in hit)
            {
                if (ray.collider.gameObject != gameObject && stop == false)
                {
                    GameObject go = ray.collider.gameObject;
                    FixedJoint fixJ = gameObject.AddComponent<FixedJoint>();
                    fixJ.connectedBody = go.GetComponent<Rigidbody>();
                    stop = true;
                }
            }
        }
        if (blockSettings.weConnectRight)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.right, koeff, LayerMask.GetMask("CarBlock"));
            bool stop = false;
            foreach (RaycastHit ray in hit)
            {
                if (ray.collider.gameObject != gameObject && stop == false)
                {
                    GameObject go = ray.collider.gameObject;
                    FixedJoint fixJ = gameObject.AddComponent<FixedJoint>();
                    fixJ.connectedBody = go.GetComponent<Rigidbody>();
                    stop = true;
                }
            }
        }
    }
}
