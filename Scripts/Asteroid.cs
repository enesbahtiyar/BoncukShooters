using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private int _Lives = 5;
    [SerializeField]
    private GameObject[] powerups;

    private Player _player;
    private Uımanager _uimanager;
    private GameManager _gamemanager;
    private Animator _anim;
    [SerializeField]
    private float _speed = 1.3f;
    [SerializeField]
    private AudioClip _explosionsound;

    private AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();       
        _uimanager = GameObject.Find("Canvas").GetComponent<Uımanager>();
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _anim = GetComponent<Animator>();
        _audiosource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);      
        if (transform.position.y < -9f)
        {    
            Destroy(this.gameObject);
        }
    }

    public void asteroiddamage()
    {
        _Lives -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            asteroiddamage();
            if (_Lives == 0)
            {               
                asteroiddestroyed();
            }
        }

        if (other.tag == "Player")
        {
            Destroy(_player.gameObject);
            playerdestroyed();
        }

        if (other.tag == "Enemy")
        {
            //Do nothing
        }
    }

    public void asteroiddestroyed()
    {
        _audiosource.Play();
        _player.AddScore(20);
        _anim.SetTrigger("Asteroid_Destroyed");
        _speed = 0;
        Destroy(this.gameObject, 2.3f);        
        Vector3 postoSpawn = GameObject.Find("Asteroid(Clone)").transform.position;
        int randomPowerup = Random.Range(0, 3);
        GameObject newEnemy = Instantiate(powerups[randomPowerup], postoSpawn, Quaternion.identity);
        
    }

    public void playerdestroyed()
    {     
        _uimanager.gameoversequence();
        _gamemanager.GameOver();
        
    }
}
