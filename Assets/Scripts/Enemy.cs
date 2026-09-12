using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;

    protected Transform player;

    [Header("State Machine")]
    [SerializeField] private float JarakDeteksi = 6f;
    [SerializeField] private float JarakSerang = 1.5f;
    [SerializeField] private float JedaSerang = 1f;

    // State Now
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();
        
        switch(state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

 
    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log(gameObject.name + " kena damage: " + jumlah + " | Sisa HP: " + hp);

        if (hp <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        Debug.Log(gameObject.name + " Mati!");

        
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.SendMessage("SaatZombieMati", this, SendMessageOptions.DontRequireReceiver);
        }

        // Hapus induk teratas (root) agar visual di parent ikut hilang dari layar
        Destroy(transform.root.gameObject);
    }
    // ------------------------------------------

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= JarakSerang)
        {
            state = StateZombie.ATTACK;
        }
        else if (jarak <= JarakDeteksi)
        {
            state = StateZombie.CHASE;
        }
        else
        {
            state = StateZombie.PATROL;
        }   
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy Serang");
    }

    void PerilakuIdle()
    {
        Debug.Log("Enemy sedang IDLE");
    }

    void PerilakuPatrol()
    {
        Debug.Log("Enemy sedang PATROL");
    }

    void PerilakuChase()
    {
        Kejar();
        Debug.Log("Enemy sedang CHASE");
    }

    void PerilakuAttack()
    {
        Debug.Log("Enemy sedang ATTACK");
    }
}