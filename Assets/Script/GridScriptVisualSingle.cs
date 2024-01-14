using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScriptVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_MeshRenderer;

    public void Show() {
        m_MeshRenderer.enabled = true;
    }

    public void Hide() { 
        m_MeshRenderer.enabled = false;
    }
}
