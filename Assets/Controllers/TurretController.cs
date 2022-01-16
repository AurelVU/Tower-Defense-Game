using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
	public Sprite icon;
	private Transform target;
	private EnemyController targetEnemy;

	[Header("General")]
	public float damage = 0.1f;
	public float range = 15f;
	public int cost = 700;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<EnemyController>();
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
        
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();
		} else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}

	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser ()
	{
		targetEnemy.hit(damageOverTime * Time.deltaTime);
		// targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	
    public void onHit(Transform target, EnemyController targetEnemyController) {
		targetEnemyController.hit(damage);
    }
	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		BulletController bullet = bulletGO.GetComponent<BulletController>();

		if (bullet != null)
		{
			bullet.Seek(target, targetEnemy);
			bullet.onHitEnemy += onHit;
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public void sale() {
		GameObject shop = GameObject.Find("shop");
        ShopController shopController = shop.GetComponent<ShopController>();
		Debug.Log(transform.name.Replace("(Clone)", "").Trim());
		shopController.towerSale(transform.name.Replace("(Clone)", "").Trim());
	}

    private void OnMouseUp() {
        Debug.Log("Clicked on TURRET!!!");
    }
}
