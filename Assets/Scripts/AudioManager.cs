using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_Source;
    [SerializeField] List<AudioClip> m_ClipList = new List<AudioClip>();
    [SerializeField] internal int m_Index = 0;

    private void Start()
    {
        ChangeSong(0);
    }

    internal void Previous()
    {
        if (m_Index <= 0)
        {
            m_Index = m_ClipList.Count;
        }
        else
        {
            m_Index--;
        }

        ChangeSong(m_Index);
    }

    internal void Next()
    {
        if (m_Index >= m_ClipList.Count - 1)
        {
            m_Index = 0;
        }
        else
        {
            m_Index++;
        }
        ChangeSong(m_Index);

    }

    private void ChangeSong(int index)
    {
        m_Source.clip = m_ClipList[index];
        m_Source.Play();
    }
}
