using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : class ,IComparable<T>
{
    public bool IsEmpty
    {
        get
        {
            return queue.Count > 0;
        }
    }

    private List<T> queue;

    public PriorityQueue()
    {
        queue = new List<T>();
    }

    public void Enque(T element)
    {
        if(queue.Count > 0)
        {
            int count = queue.Count;
            for(int i = 0; i < count; i++)
            {
                if(element.CompareTo(queue[i]) == -1)
                {
                    queue.Insert(i, element);
                    return;
                }
            }
        }

        queue.Add(element);
    }

    public T Deque()
    {
        if(queue.Count > 0)
        {
            T element = queue[0];
            queue.RemoveAt(0);
            return element;
        }

        return null;
    }

    public void RemoveElement(T element)
    {
        queue.Remove(element);
    }
}
