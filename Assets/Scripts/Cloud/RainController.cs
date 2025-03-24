using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] private CloudController cloudController;

    [SerializeField] private Transform cloud;
    private ParticleSystem rain;
    private bool isRaining = false;

    private void Start()
    {
        rain = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.position = cloud.position;
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRaining)
                StopRain();
            else
                StartRain();
        }

        if (isRaining)
        {
            cloudController.ReduceWater(Time.deltaTime * cloudController.WaterConsumptionRate);

            if (cloudController.WaterSupply <= 0)
            {
                StopRain();
            }
        }


    }

    public void StartRain()
    {
        if (cloudController.WaterSupply <= 0)
        {
            Debug.Log("Нет воды в облаке!");
            return;
        }
        rain.Play();
        isRaining = true;
    }

    public void StopRain()
    {
        rain.Stop();
        isRaining = false;
    }
}