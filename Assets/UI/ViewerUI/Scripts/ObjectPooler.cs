using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject NextObject {
        get {
            if (m_pool.Count != 0)
            {
                for(int i = 0; i < m_pool.Count; i++)
                {
                    if(m_pool[i] != null)
                    {
                        GameObject go = m_pool[i];
                        m_pool.RemoveAt(i);
                        return go;
                    }
                }
            }
            return (Instantiate(m_prefab));
        } }
    private List<GameObject> m_pool;
    [SerializeField][Range(1, 400)] private int m_poolSize = 100;
    [SerializeField] private GameObject m_prefab;

	private void Awake ()
    {
        m_pool = new List<GameObject>(m_poolSize);
        StartCoroutine(FillPool());
	}

    private IEnumerator FillPool()
    {
        int amountToAdd = m_poolSize - m_pool.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            m_pool.Add(Instantiate(m_prefab, transform));
            m_pool[m_pool.Count - 1].SetActive(false);
            yield return null;
        }
        yield return null;
    }
}
