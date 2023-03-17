using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#pragma warning disable CA1050
public static class StringExtensions
{
    public static string ReplaceIc(this string current, string oldValue, string newValue)
    {
        return current.Replace(oldValue, newValue, StringComparison.InvariantCulture);
    }
    public static bool ContainsIc(this string current, string value)
    {
        return current.Contains(value, StringComparison.InvariantCulture);
    }
    public static bool StartsWithIc(this string current, string value)
    {
        return current.StartsWith(value, StringComparison.InvariantCulture);
    }
    public static int IndexOfIc(this string current, string value)
    {
        return current.IndexOf(value, StringComparison.InvariantCulture);
    }
    public static int LastIndexOfIc(this string current, string value)
    {
        return current.LastIndexOf(value, StringComparison.InvariantCulture);
    }
    public static bool EqualsIc(this string current, string value)
    {
        return current.Equals(value, StringComparison.InvariantCulture);
    }

    /// <summary>
    /// Bir string listesindeki değerlerin arasına virgül katıp geriye string dönderen metod.
    /// </summary>
    /// <param name="current">String listesi</param>
    /// <returns>Aralarına virgül katılıp dönderilen değer</returns>
    public static string AddComma(this List<string> current)
    {
        if (current == null || current.Count == 0)
            return string.Empty;
        string rtn = string.Empty;
        foreach (var str in current)
        {
            rtn += string.Concat(str, ",");
        }

        rtn = rtn.Substring(0, length: rtn.Length - 1);
        return rtn;
    }
    /// <summary>
    /// String değer içindeki first parametresi ile second sonlandırıcı parametreleri arasındaki değerleri listeleyip dönderir.
    /// </summary>
    /// <param name="st">İki değer arasındaki değerlerin alınacağı string değer</param>
    /// <param name="first">Bu parametre ile başlayan değerler</param>
    /// <param name="second">Sonlandırıcı parametreler</param>
    /// <returns>İki string arasında kalan değerler listesi</returns>
    public static List<string> BetweensTwoString(this string st, string first, params string[] second)
    {
        List<string> result = new List<string>();
        if (string.IsNullOrEmpty(st))
            return result;
        var sp = st.Split(first);
        for (int i = 1; i < sp.Length; i++)
        {
            var s = sp[i];//.Split(second[0])[0];
            //if (second.Length > 1)
            //{
                foreach (var str in second)
                {
                    s = s.Split(str)[0];
                }
            //}
            result.Add(s);
        }

        return result;
    }
    /// <summary>
    /// String bir değer içerisinde olmasını istemediğin değeri boşaltır
    /// </summary>
    /// <param name="st"></param>
    /// <param name="killed">Boşaltılacak içerik</param>
    /// <returns>İstenmeyen değerlerin silindiği string değer.</returns>
    public static string Kill(this string st, string killed)
    {
        var firstPie = st.Substring(0, st.IndexOfIc(killed));
        var secondPie = st.Substring(st.LastIndexOfIc(killed), st.Length - killed.Length - firstPie.Length);

        return string.Concat(firstPie, secondPie);
    }
    public static T ToObject<T>(this string str)
    {
       return JsonConvert.DeserializeObject<T>(str);
    }
    public static object  ToObject(this string str,Type type)
    {
        return JsonConvert.DeserializeObject(str,type);
    }
    public static T ToObject2<T>(this string str)
    {
        return JsonConvert.DeserializeObject<T>(str);
    }
}


