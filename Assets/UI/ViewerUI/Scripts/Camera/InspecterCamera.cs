using com.bricsys.tune.TreeNode;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class InspecterCamera : MonoBehaviour
{
    static public Action OnCameraPanStarted;
    static public Action OnCameraPanEnded;
    static public InspecterCamera Active { get; private set; }
    [SerializeField] [Range(0.1f, 4f)] private float m_panSpeed = 1f;
    private Coroutine m_currentTransition;
    private bool m_interrupted = false;

    /// <summary>
    /// Returns false if the camera doesn't need to pan to the given TreeNode.
    /// </summary>
    public bool Inspect(ITreeNode treeNode)
    {
        Vector3 targetPos = treeNode.Position;
        Quaternion targetRot = Quaternion.identity;
        if (treeNode.Rotation != null)
            targetRot = (Quaternion)treeNode.Rotation;
        if (transform.position == targetPos && transform.rotation == targetRot)
            return false;

        if (m_currentTransition != null)
        {
            StopCoroutine(m_currentTransition);
            m_interrupted = true;
        }
        m_currentTransition = StartCoroutine(Transition(targetPos, targetRot));
        return true;
    }

    private IEnumerator Transition(Vector3 targetPos, Quaternion targetRot)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float transition = 0;
        float distanceMultiplier = m_panSpeed / Mathf.Abs(Vector3.Distance(startPos, targetPos)); // This equals to panSpeed (m/s) / meters to cross
        distanceMultiplier = Mathf.Clamp(distanceMultiplier, 0.1f, 4f);
        Debug.Log("DistanceMultiplier = " + distanceMultiplier);

        //Proposal: change movepseed in "tiers" depending on how far needs to be travelled. The closer the target, the faster the movement speed goes...

        if (OnCameraPanStarted != null && !m_interrupted)
            OnCameraPanStarted();

        m_interrupted = false;
        while (transition < 1)
        {
            transition = Mathf.Clamp01(transition += Time.deltaTime * distanceMultiplier);
            transform.position = Vector3.Lerp(startPos, targetPos, transition);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, transition);
            yield return null;
        }
        if (OnCameraPanEnded != null)
            OnCameraPanEnded();

        m_currentTransition = null;
    }

    private void Awake()
    {
        if (Active != null && Active != this)
            Destroy(this);

        Active = this;
    }
}
