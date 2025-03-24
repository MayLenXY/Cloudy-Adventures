using UnityEngine;
using UnityEngine.UI;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minX = -5f, maxX = 5f;
    [SerializeField] private float minZ = -5f, maxZ = 5f;
    [SerializeField] private float cloudHeight = 5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float maxWaterSupply = 100f;
    private float waterSupply;
    [SerializeField] private float waterConsumptionRate = 5f;
    [SerializeField] private float waterRechargeRate = 2f;

    [SerializeField] private Slider waterBar;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        waterSupply = maxWaterSupply;
        targetPosition = transform.position;
        UpdateWaterBar();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                Vector3 newTarget = new Vector3(hit.point.x, cloudHeight, hit.point.z);
                if (Physics.Linecast(transform.position, newTarget, out RaycastHit wallHit, obstacleLayer))
                {
                    if (wallHit.collider.CompareTag("Obstacle"))
                    {
                        newTarget = new Vector3(wallHit.point.x, cloudHeight, wallHit.point.z);
                    }
                }

                newTarget.x = Mathf.Clamp(newTarget.x, minX, maxX);
                newTarget.z = Mathf.Clamp(newTarget.z, minZ, maxZ);

                targetPosition = newTarget;
                isMoving = true;
            }
        }

        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }

        RechargeWater();
    }

    // Уменьшает запас воды
    public void ReduceWater(float amount)
    {
        waterSupply = Mathf.Max(0, waterSupply - amount);
        UpdateWaterBar();
    }

    // Пополняет запас воды
    public void RechargeWater()
    {
        waterSupply = Mathf.Min(maxWaterSupply, waterSupply + waterRechargeRate * Time.deltaTime);
        UpdateWaterBar();
    }

    // Геттер для получения текущего запаса воды
    public float WaterSupply => waterSupply;

    // Геттер для скорости расхода воды
    public float WaterConsumptionRate => waterConsumptionRate;

    void UpdateWaterBar()
    {
        if (waterBar != null)
        {
            waterBar.value = waterSupply / maxWaterSupply;
        }
    }
}
