using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float range = 100f;

    [Header("UI")]
    [SerializeField] private HitMarkerUI hitMarker;
    [Header("Recoil")]
    [SerializeField] private float recoilKick = 2f;
    [SerializeField] private float recoilReturnSpeed = 20f;


    private float nextFireTime;
    private float currentRecoil;
    private float targetRecoil;


    private AudioSource audioSource;

    private Camera playerCamera;

    public float CurrentRecoil => currentRecoil;


    private void Awake()
    {
        // Get camera from player hierarchy
        playerCamera = GetComponentInChildren<Camera>();
        // Audio
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
        targetRecoil = Mathf.Lerp(targetRecoil, 0f, recoilReturnSpeed * Time.deltaTime);
        currentRecoil = Mathf.Lerp(currentRecoil, targetRecoil, recoilReturnSpeed * Time.deltaTime);

    }

    private void Shoot()
    {
        audioSource.PlayOneShot(audioSource.clip);

        Debug.Log("BANG " + Time.time);

        targetRecoil += recoilKick;


        if (playerCamera == null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, range))
            return;

        // Ignore hitting yourself
        if (hit.transform.root == transform)
            return;

        Debug.Log("Hit: " + hit.transform.name);

        // Target logic
        Target target = hit.transform.GetComponent<Target>();
        if (target != null)
        {
            target.OnHit();

            if (hitMarker != null)
            {
                hitMarker.Show();
            }
        }
    }
}



