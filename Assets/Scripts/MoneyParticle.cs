using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticle : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private double vSpd = -500;
    public double hSpd = 0;

    private bool fall = false;

    /// <summary>
    /// Список партиклов, см <see cref="ClickerGame.particles">ClickerGame</see>
    /// </summary>
    [NonSerialized]
    public List<GameObject> particles;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {

        float dt = Time.deltaTime;

        Vector2 pos = m_RectTransform.position;
        pos.x += (float)hSpd * dt;
        pos.y -= (float)vSpd * dt;
        m_RectTransform.position = pos;
        m_RectTransform.Rotate((float)vSpd * dt, 0, 0, Space.Self);

        if (fall)
            vSpd += 80 * dt;
        else
            vSpd += 20 * dt;

        if (!fall && pos.y > Screen.safeArea.height - 300)
        {
            vSpd = -20;
            fall = true;
        }

        if (pos.y < -60)
        {
            particles?.Remove(gameObject);
            Destroy(gameObject);
        }    
    }
}
