using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleField : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    [SerializeField] private float _cellSize;
    [SerializeField] private int _textureResolution;
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    
    private Texture2D[,] _textures;
    private Color[,][] _colors;
    private float[,] _waterVolume;

    private int _paddedColumns;
    private int _paddedRows;
    
    private int _textureRows;
    private int _textureColumns;

    private void RecalculateMetrics()
    {
        _textureRows = Mathf.CeilToInt((float)_rows / _textureResolution);
        _textureColumns = Mathf.CeilToInt((float)_columns / _textureResolution);
        
        _paddedRows = _textureRows * _textureResolution;
        _paddedColumns = _textureColumns * _textureResolution;
    }

    private void CreateRenderers()
    {
        for (int tY = 0; tY < _textureRows; tY++)
        {
            for (int tX = 0; tX < _textureColumns; tX++)
            {
                GameObject rendererObject = new GameObject();
                rendererObject.transform.SetParent(transform);
                rendererObject.transform.localPosition = new Vector3(tX * _cellSize * _textureResolution, tY * _cellSize * _textureResolution, 0);
                rendererObject.transform.localScale = Vector3.one * _cellSize * _textureResolution;
                rendererObject.transform.localRotation = Quaternion.identity;
                
                MeshFilter meshFilter = rendererObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = _mesh;
                
                MeshRenderer meshRenderer = rendererObject.AddComponent<MeshRenderer>();
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                meshRenderer.sharedMaterial = new Material(_material);
                meshRenderer.sharedMaterial.SetTexture(MainTex, _textures[tY, tX]);
            }
        }
    }

    private void Start()
    {
        RecalculateMetrics();
        
        _waterVolume = new float[_paddedColumns, _paddedRows];

        for (int y = 0; y < _paddedRows; y++)
        {
            for (int x = 0; x < _paddedColumns; x++)
            {
                _waterVolume[x, y] = Random.value;
            }
        }
        
        _textures = new Texture2D[_textureRows, _textureColumns];
        
        _colors = new Color[_textureRows, _textureColumns][];

        for (int y = 0; y < _textureRows; y++)
        {
            for (int x = 0; x < _textureColumns; x++)
            {
                _textures[x,y] = new Texture2D(_textureResolution, _textureResolution, TextureFormat.R8, false);
                _colors[x,y] = _textures[x,y].GetPixels();
            }
        }

        CreateRenderers();
    }

    private void TransferVolumeToColors()
    {
        int pYStart = 0;
        
        for (int tY = 0; tY < _textureRows; tY++)
        {
            int pXStart = 0;
            
            for (int tX = 0; tX < _textureColumns; tX++)
            {
                int p = 0;

                for (int pY = pYStart; pY < pYStart + _textureResolution; pY++)
                {
                    for (int pX = pXStart; pX < pXStart + _textureResolution; pX++)
                    {
                        _colors[tX, tY][p].r = _waterVolume[pX, pY];
                        p++;
                    }
                }
                pXStart += _textureResolution;
            }
            pYStart += _textureResolution;
        }
    }

    private void TransferColorsToTextures()
    {
        for (int tY = 0; tY < _textureRows; tY++)
        {
            for (int tX = 0; tX < _textureColumns; tX++)
            {
                _textures[tX, tY].SetPixels(_colors[tX, tY]);
                _textures[tX, tY].Apply();
            }
        }
    }

    private void RunSimulation()
    {
        
    }

    private void FixedUpdate()
    {
        TransferVolumeToColors();
        TransferColorsToTextures();
    }

    private void OnDrawGizmosSelected()
    {
        RecalculateMetrics();
        
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;

        float xSize = _paddedColumns * _cellSize;
        float ySize = _paddedRows * _cellSize;
        
        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(xSize, 0, 0));
        Gizmos.DrawLine(new Vector3(xSize, 0, 0), new Vector3(xSize, ySize, 0));
        Gizmos.DrawLine(new Vector3(xSize, ySize, 0), new Vector3(0, ySize, 0));
        Gizmos.DrawLine(new Vector3(0, ySize, 0), new Vector3(0, 0, 0));
    }
}
