﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extentions
{
    /// <summary>
    /// Same as LINQ Min but returns element with min value itself
    /// </summary>
    public static TSource MinElement<TSource>(this IEnumerable<TSource> source, Func<TSource, float> comparator)
    {
        TSource[] array = source.ToArray();
        TSource result = default(TSource);
        float min = Single.MaxValue;
        foreach (TSource element in array)
        {
            float comparedValue = comparator(element);
            if (comparedValue > min)
                continue;
            result = element;
            min = comparedValue;
        }

        return result;
    }

    public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);
}