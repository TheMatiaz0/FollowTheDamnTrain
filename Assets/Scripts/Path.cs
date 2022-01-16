using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Path : MonoBehaviour
{
    public static Path Instance { get; private set; } = null;

    /// <summary>
    /// Super private, don't use this in this script
    /// </summary>
    [SerializeField]
    private Transform[] nodes = null;

    private Transform[] reverseNodes = null;

    private Transform[] currentSelectedNodeArray = null;

    protected void Awake()
    {
        Instance = this;
        reverseNodes = nodes;
        Array.Reverse((Transform[])reverseNodes.Clone());

        ReverseNodes(false);
    }

    public void ReverseNodes(bool reverse)
    {
        currentSelectedNodeArray = (reverse) ? reverseNodes : nodes;
    }

    public Vector3 GetLinearPositionFromNode(int nodeIndex, float ratio)
    {
        Vector3 firstPoint = currentSelectedNodeArray[nodeIndex].position;
        Vector3 secondPoint;

        if (nodeIndex + 1 >= GetNodesCount())
        {
            secondPoint = firstPoint;
        }

        else
        {
            secondPoint = currentSelectedNodeArray[nodeIndex + 1].position;
        }

        return Vector3.Lerp(firstPoint, secondPoint, ratio);
    }

    public Transform GetTransformDestination(int nodeIndex)
    {
        Transform firstPoint = currentSelectedNodeArray[nodeIndex];
        Transform secondPoint;

        if (nodeIndex + 1 >= GetNodesCount())
        {
            secondPoint = firstPoint;
        }

        else
        {
            secondPoint = currentSelectedNodeArray[nodeIndex + 1];
        }

        return secondPoint;
    }

    public int GetNodesCount()
    {
        return currentSelectedNodeArray.Length;
    }

    public Vector3 GetPositionFromNode(int nodeIndex)
    {
        return currentSelectedNodeArray[nodeIndex].position;
    }

    /// <summary>
    /// Editor utility, visible lines in the Editor menu
    /// </summary>
    protected void OnDrawGizmos()
    {
        for (int i = 0; i < GetNodesCount() - 1; i++)
        {
            Debug.DrawLine(currentSelectedNodeArray[i].position, currentSelectedNodeArray[i + 1].position, Color.red);
        }
    }
}
