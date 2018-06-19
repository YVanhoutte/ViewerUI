using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class VRVignetteController : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float m_fadeStrength = 0.5f;
    [SerializeField] [Range(0.5f, 4f)] private float m_fadeSpeed = 2f;
    private Vector3 m_previousPos;
    private Quaternion m_previousRot;
    private Rigidbody m_rb;
    private Vignette m_vignette;
    private float m_velocity;
    private Coroutine m_fadeRoutine = null;

    private void Awake()
    {
        m_rb = GetComponentInParent<Rigidbody>();
        var pv = GetComponent<PostProcessVolume>();
        pv.profile.TryGetSettings(out m_vignette);
    }

    void FixedUpdate ()
    {
        float velocity;
        if(m_rb == null)
        {
            Vector3 currentPos = transform.position;
            Quaternion currentRot = transform.rotation;
            velocity = (currentPos - m_previousPos).sqrMagnitude + Quaternion.Angle(currentRot, m_previousRot);
            m_previousPos = currentPos;
            m_previousRot = currentRot;
        }
        else
        {
            velocity = m_rb.velocity.sqrMagnitude + m_rb.angularVelocity.sqrMagnitude;
        }

        if(m_velocity == 0 && velocity != 0)
        {
            if(m_fadeRoutine != null)
                StopCoroutine(m_fadeRoutine);
            m_fadeRoutine = StartCoroutine(FadeIn(m_fadeStrength, m_fadeSpeed));
        }
        else if (m_velocity != 0 && velocity == 0)
        {
            if (m_fadeRoutine != null)
                StopCoroutine(m_fadeRoutine);
            m_fadeRoutine = StartCoroutine(FadeOut(m_fadeSpeed));
        }

        m_velocity = velocity;
    }

    private IEnumerator FadeOut(float speed)
    {
        while(m_vignette.intensity.value > 0)
        {
            m_vignette.intensity.value -= speed * Time.deltaTime;
            yield return null;
        }
        m_vignette.intensity.value = 0;
        m_fadeRoutine = null;
    }

    private IEnumerator FadeIn(float limit, float speed)
    {
        while (m_vignette.intensity.value < limit)
        {
            m_vignette.intensity.value += speed * Time.deltaTime;
            yield return null;
        }
        m_vignette.intensity.value = limit;
        m_fadeRoutine = null;
    }
}
