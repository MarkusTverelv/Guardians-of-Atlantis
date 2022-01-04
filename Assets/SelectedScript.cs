using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SelectedScript : MonoBehaviour
{
    public UnityEvent OnEnter = new UnityEvent();
    TMP_Text textMesh;
    Color baseColor;
    bool _selected;
    int framecounter = 0;
    public bool selected
    {
        get
        {
            return _selected;
        }
        set
        {
            if (value)
            {
                textMesh.fontSize = 40;
                textMesh.color = Color.white;
            }
            else
            {
                textMesh.fontSize = 30;
                textMesh.color = baseColor;
            }
            _selected = value;
        }
    }
    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        baseColor = textMesh.color;
        
        
    }
    private void Update()
    {
        framecounter++;
        textMesh.ForceMeshUpdate();
        Vector3[] vertices = textMesh.mesh.vertices;
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            
            Vector3 offset = Wobble(i + framecounter/100f);
            for(int j = 0; j<4; j++)
            {
                vertices[index + j] += offset; 
            }  
        }
        textMesh.mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(textMesh.mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * 1.1f));
    }

}
