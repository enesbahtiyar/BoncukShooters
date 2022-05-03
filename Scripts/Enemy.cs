using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

   
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _anim;
    [SerializeField]
    private AudioClip _explosionsound;

    private AudioSource _audiosource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audiosource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("the player is null");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Anim is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float RandomRange = Random.Range(-9, 9);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);      
        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(RandomRange, 11f, 0);
        }       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(_player != null)
            {
                _player.Damage(1);
            }
            _audiosource.Play();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.3f);
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _audiosource.Play();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.3f);
        }
    }
}
