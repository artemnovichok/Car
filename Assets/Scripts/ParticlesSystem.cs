using UnityEngine;

public class ParticlesSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnEnable()
    {
        EventManager.Instance.startMoveCar.AddListener(startParticle);
        EventManager.Instance.stopMoveCar.AddListener(stopParticle);
    }

    private void OnDisable()
    {
        EventManager.Instance.startMoveCar.RemoveListener(startParticle);
        EventManager.Instance.stopMoveCar.RemoveListener(stopParticle);
    }

    private void Start()
    {
        _particle.Stop();
    }

    private void startParticle()
    {
        _particle.Play();
    }

    private void stopParticle()
    {
        _particle.Stop();
    }
}
