using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();
    public void Add(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (heap[i].CompareTo(heap[parent]) >= 0) 
                break;
            (heap[i], heap[parent]) = (heap[parent], heap[i]);
            i = parent;
        }
    }

    public T Pop()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Heap is empty");

        T root = heap[0];
        T last = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        if (heap.Count > 0)
        {
            heap[0] = last;
            Heapify(0);
        }

        return root;
    }

    private void Heapify(int i)
    {
        int smallest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < heap.Count && heap[left].CompareTo(heap[smallest]) < 0) smallest = left;
        if (right < heap.Count && heap[right].CompareTo(heap[smallest]) < 0) smallest = right;

        if (smallest != i)
        {
            (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
            Heapify(smallest);
        }
    }

    public int Count => heap.Count;
}