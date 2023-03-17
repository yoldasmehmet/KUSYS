﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Hierarchy
{
    public static IEnumerable<TSource> FromHierarchies<TSource>(
this TSource source,
Func<TSource, TSource> nextItem,
Func<TSource, bool> canContinue)
    {
        for (var current = source; canContinue(current); current = nextItem(current))
        {
            yield return current;
        }
    }

    public static IEnumerable<TSource> FromHierarchies<TSource>(
        this TSource source,
        Func<TSource, TSource> nextItem)
        where TSource : class
    {
        return FromHierarchies(source, nextItem, s => s != null);
    }
}

