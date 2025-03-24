using UnityEngine;

namespace MyGame.Environment
{
    public class PlantController : MonoBehaviour
    {
        [Tooltip("Скорость роста растения")]
        [SerializeField] private float growthRate = 0.1f;

        [Tooltip("Время роста растения (в секундах)")]
        [SerializeField] private float growthTime = 2f;

        private bool isGrowing = false;
        private float growthTimer = 0f;
        private Vector3 initialScale;

        private void Start()
        {
            initialScale = transform.localScale;
        }

        private void Update()
        {
            if (!isGrowing) return;

            growthTimer += Time.deltaTime;
            transform.localScale += Vector3.one * (growthRate * Time.deltaTime);

            if (growthTimer >= growthTime)
            {
                isGrowing = false;
                growthTimer = 0f;
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!other.CompareTag("RainDrop") || isGrowing) return;

            isGrowing = true;
            ParticleSystem ps = GetComponent<ParticleSystem>();

            if (ps == null) return;

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
            int numParticles = ps.GetParticles(particles);

            for (int i = 0; i < numParticles; i++)
            {
                particles[i].remainingLifetime = 0;
            }

            ps.SetParticles(particles, numParticles);
        }

        public void ResetScale()
        {
            transform.localScale = initialScale;
        }
    }
}
