using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Tooltip("Место старта")][SerializeField] private GameObject _posToStart;
    [Tooltip("Время до старта торнадо")][SerializeField] private float _timeToStartTornado = 5f;
    [Tooltip("Скорость торнадо")][SerializeField] private float _tornadoSpeed = 1f;
    private bool _isStart = false;
    private GameObject _tornadoPrefab;
    private GameObject _tornado;

    private void Start()
    {
        if(LevelEvent.Instance.Init() == EventType.Tornado)
        {
            _tornadoPrefab = Resources.Load<GameObject>("Tornado_obj");
            Invoke(nameof(startTornado), _timeToStartTornado);
        }
    }

    private void Update()
    {
        if (!_isStart)
        {
            return;
        }

        float speed = _tornadoSpeed * Time.deltaTime;
        _tornado.transform.position = new Vector3(_tornado.transform.position.x + speed, _posToStart.transform.position.y, _posToStart.transform.position.z);
    }

    private void startTornado()
    {
        _isStart = true;
        _tornado = Instantiate(_tornadoPrefab, _posToStart.transform.position, Quaternion.identity);
    }
}
