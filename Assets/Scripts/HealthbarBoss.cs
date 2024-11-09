// Credit to Tarodev https://www.youtube.com/watch?v=6U_OZkFtyxY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBoss: MonoBehaviour {
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _reduceSpeed = 2;
    private float _target = 1;
    private Camera _cam;
    private BossAI _bossAI;

    void Start() {
        _cam = Camera.main;
        _bossAI = GetComponentInParent<BossAI>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        _target = currentHealth / maxHealth;
    }

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);

        if (_bossAI != null) {
            UpdateHealthBar(1000f, _bossAI.health); // Corrected to use the actual health value as current health
        }
    }
}
