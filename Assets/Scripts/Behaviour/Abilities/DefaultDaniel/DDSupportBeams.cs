using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDSupportBeams : MonoBehaviour
{
    private float damage;
    CombatHandler combatHandler;
    public PolygonCollider2D polygonCollider;
    public SpriteRenderer beamRenderer;
    private bool nowUsable = false;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        if (nowUsable)
        {
            polygonCollider.enabled = true;
            beamRenderer.enabled = true;
        }
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
        if (nowUsable)
        {
            polygonCollider.enabled = false;
            beamRenderer.enabled = false;
        }
    }
    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        beamRenderer = gameObject.GetComponent<SpriteRenderer>();
        polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        gameObject.SetActive(false);
        polygonCollider.enabled = false;
        beamRenderer.enabled = false;
        nowUsable = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        combatHandler.HandleDamage(damage, collision.gameObject,CombatHandler.DamageType.Light);
    }
    public void UpdateInfo(float passedDamage)
    {
        damage = passedDamage;
    }
}
