using System.Collections;
using PeanutDashboard._06_RobotRampage;
using UnityEngine;

public class SwordixAttackTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private Animator _shadowAnimator;
    
    [SerializeField]
    private float _cooldown;

    [SerializeField]
    private float _damage;
    
    [SerializeField]
    private float _timeToAttack;

    [SerializeField]
    private bool _playerInCollision;

    [SerializeField]
    private RobotRampagePlayerHealth _playerHealth;
    
    public void Initialise(float cooldown, float damage)
    {
        _timeToAttack = cooldown;
        _cooldown = cooldown;
        _damage = damage;
    }

    private void Update()
    {
        _timeToAttack -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth = other.GetComponent<RobotRampagePlayerHealth>();
            _playerInCollision = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_timeToAttack < 0 && other.CompareTag("Player"))
        {
            _timeToAttack = _cooldown;
            _animator.SetTrigger("Attack");
            _shadowAnimator.SetTrigger("Attack");
            StartCoroutine(DamagePlayerAfter());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInCollision = false;
        }
    }

    private IEnumerator DamagePlayerAfter()
    {
        yield return new WaitForSeconds(0.3f);
        if (_playerInCollision)
        {
            _playerHealth.DealDamage(_damage);
        }
    }
}
