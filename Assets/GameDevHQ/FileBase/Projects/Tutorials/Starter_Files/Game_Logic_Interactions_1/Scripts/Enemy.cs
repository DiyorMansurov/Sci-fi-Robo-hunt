using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private enum States
    {
        Running,
        Hiding,
        Death
    }

    [SerializeField] private States _currentState = States.Running;
    [SerializeField] private AudioSource _death_SFX;

    
    [SerializeField] private GameObject _redLight;

    private NavMeshAgent _agent;
    private EnemyPool _pool;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Vector3 _coverPoint;
    private Animator _animator;
    private Transform _lookTarget;
    private float _waitingTime;
    private bool _hiddenAlready = false;
    [SerializeField] private bool _isDead = false;


    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _lookTarget = GameObject.Find("Player").GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        // if (_agent.remainingDistance < 1f)
        // {
        //     _currentState = States.Death; 
        // }
        if (_isDead) return; 

        switch (_currentState)
        {
            case States.Running: RunningBehaviour(); break;
            case States.Hiding: HidingBehaviour(); break;
            case States.Death: DeathBehaviour(); break;

            default: break;
        }

        CoverSpotDetection();
    }

    public void Init(Vector3 start, EnemyPool pool, Vector3 end)
    {
        _startPoint = start;
        _pool = pool;
        _endPoint = end;
        _coverPoint = CoverManager.Instance.GetRandomCoverPoint();

        gameObject.SetActive(true);
        _redLight.SetActive(true);
        _agent.enabled = true;
        _isDead = false;
        _hiddenAlready = false;
        _currentState = States.Running;
        gameObject.tag = "Enemy";
        _animator.SetFloat("Speed", _agent.speed);
        transform.position = _startPoint;
        _agent.destination = _endPoint; 
        _agent.speed = SpawnManager.Instance.RandomSpeed();
        _waitingTime = Random.Range(3f, 6f);
    }

    private void RunningBehaviour()
    {
        if (_isDead || !_agent.enabled) return;

        _agent.updateRotation = true;
        _agent.destination = _endPoint;
    }

    private void CoverSpotDetection()
    {
        if (_isDead || !_agent.enabled) return; 

        float distance = Vector3.Distance(this.transform.position, _coverPoint);

        if (distance < 6f && !_hiddenAlready)
        {
            _agent.destination = _coverPoint;
            _currentState = States.Hiding;
        }
    }
    private void HidingBehaviour()
    {
        if (_isDead || !_agent.enabled) return; 
        
        if (_waitingTime > 0f)
        {
            _waitingTime -= Time.deltaTime;

            _agent.updateRotation = false;
            _animator.SetBool("Hiding", true);
            _agent.destination = _coverPoint; 
            RotateTowardsTarget();   
        }else if(_waitingTime <= 0f)
        {
            _currentState = States.Running;
            _animator.SetBool("Hiding", false);
            _hiddenAlready = true;
        }
        
    }
    private void DeathBehaviour()
    {
        if (_isDead) return;

        _isDead = true;

        _death_SFX.pitch = Random.Range(0.75f, 1.75f);
        _death_SFX.Play();

        _agent.isStopped = true;
        _agent.enabled = false;

        _animator.SetBool("Hiding", false);
        _animator.SetTrigger("Death");

        _redLight.SetActive(false);

        StartCoroutine(WaitandReturn());
    }

    IEnumerator WaitandReturn()
    {
        yield return new WaitForSeconds(6f);
        ReturnToPool();
        _hiddenAlready = true;
    }

    public void EnemyHit()
    {
        if (_isDead) return;
        _currentState = States.Death;
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = _lookTarget.position - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        10f * Time.deltaTime
        );
    }

    private void ReturnToPool()
    {
        
        gameObject.SetActive(false);
        _pool.ReturnEnemyToPool(this);
        _agent.enabled = false;
    }

    public void Die()
    {
        ReturnToPool();
    }
}
