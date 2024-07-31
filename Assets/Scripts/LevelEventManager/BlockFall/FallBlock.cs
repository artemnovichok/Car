using System.Collections;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    private GameObject _character;
    private GameObject _blockDroppping;

    private void Start()
    {
        Invoke("Init", 3f);
    }

    private void Init()
    {
        if (LevelEvent.Instance.Init() != EventType.FallingObjects)
        {
            return;
        }
        _blockDroppping = Resources.Load<GameObject>("DroppBlock");
        CarBlocksSettings[] carSet = GameObject.FindObjectsOfType<CarBlocksSettings>();
        foreach (CarBlocksSettings carBlock in carSet)
        {
            if (carBlock.blockSettings.id == 0)
            {
                _character = carBlock.gameObject;
                break;
            }
        }
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        while (true)
        {
            if (_character != null)
            {
                Vector3 position= _character.transform.position;
                Vector3 fixPosition = new Vector3(position.x + Random.Range(-1,10), position.y + 15, position.z);
                Instantiate(_blockDroppping, fixPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
    