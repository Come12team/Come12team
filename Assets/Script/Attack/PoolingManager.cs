using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager m_instance;
    public static PoolingManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PoolingManager>();
                DontDestroyOnLoad(instance);
                
            }

            return m_instance;
        }
    }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        MakePoolingList();
    }
    
    [Header("Pooling Setting")]
    [SerializeField] private Projectile projectilePrefab; // 발사체 프리팹
    [SerializeField] private GameObject projectileParent; // 발사체 부모 오브젝트
    [SerializeField] private int projectileCount; // 발사체 개수(풀링할)
    private List<Projectile> ProjectileList = new List<Projectile>(); // 발사체 List


    private void MakePoolingList()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileParent.transform);
            projectile.gameObject.SetActive(false);
            ProjectileList.Add(projectile);
        }
    }

    public void CreateProjectile(Vector3 Pos, GameObject tr, int damage)
    {
        foreach (var Projectile in ProjectileList)
        {
            if (!Projectile.gameObject.activeSelf)
            {
                Projectile.Initialize(Pos, tr, damage);
                break;
            }
        }
    }
}
