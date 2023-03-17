using Library.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CA1050
public static class StringExtensions
{
    public static string ReplaceWithInvariantCulture(this string current, string oldValue, string newValue)
    {
        return current.Replace(oldValue, newValue, StringComparison.InvariantCulture);
    }
    public static bool ContainsWithInvariantCulture(this string current, string value)
    {
        return current.Contains(value, StringComparison.InvariantCulture);
    }
    public static bool StartsWithWithInvariantCulture(this string current, string value)
    {
        return current.StartsWith(value, StringComparison.InvariantCulture);
    }
    public static int IndexOfWithInvariantCulture(this string current, string value)
    {
        return current.IndexOf(value, StringComparison.InvariantCulture);
    }
    public static int LastIndexOfWithInvariantCulture(this string current, string value)
    {
        return current.LastIndexOf(value, StringComparison.InvariantCulture);
    }
    public static bool EqualsWithInvariantCulture(this string current, string value)
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
        var firstPie = st.Substring(0, st.IndexOfWithInvariantCulture(killed));
        var secondPie = st.Substring(st.LastIndexOfWithInvariantCulture(killed), st.Length - killed.Length - firstPie.Length);

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
    public static string ToBase64Encode(this string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string ToBase64Decode(this string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

    }
    static Cryptography cryptography;
    public static string Encrypt(this string plainText, string key)
    {
        CreateCryptography(key);
        return cryptography.Encrypt(plainText);
    }

    private static void CreateCryptography(string key)
    {
        if (cryptography == null)
        {
            cryptography = new Cryptography(key);
        }
    }
    public static string ToMd5(this string plainText)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes); 
        }
    }
    public static string Decrypt(this string plainText, string key)
    {
        CreateCryptography(key);
        return cryptography.Decrypt(plainText);
    }

public static Dictionary<string,double> ParseAsNumerics(this string plainText, List<FID> findedItemInfos)
    {
        return new Parser().ParseNumerics(plainText, findedItemInfos);
    }
    public static Dictionary<string, string> ParseAsStrings(this string plainText, List<FID> findedItemInfos)
    {
        findedItemInfos = findedItemInfos.OrderBy(x => x.WhiceIterator).ToList();
        return new Parser().Parse(plainText, findedItemInfos);
    }
}


