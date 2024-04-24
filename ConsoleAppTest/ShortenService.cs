using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace ConsoleAppTest
{
    public class ShortenerService
    {
        private static readonly ShortenerService _instance = new ShortenerService();
        public static ShortenerService Instance => _instance;

        private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();
        private const string BaseUrlSecure = "https://st.st/";
        private const string BaseUrl = "http://st.st/";

        private ShortenerService()
        {
        }

        public string ShortenUrl(string longUrl)
        {
            var baseUrl = BaseUrlSecure;
            if (longUrl.ToLower().StartsWith("http:"))
            {
                baseUrl = BaseUrl;
            }
            var shortKey = GenerateKey(longUrl);
            var shortUrl = baseUrl + shortKey;

            if (urlMap.ContainsKey(shortKey))
            {
                var existingShortUrl = FindExistingLongUrl(longUrl); 
                if (existingShortUrl != null) 
                {
                    return baseUrl + existingShortUrl;
                }
                throw new InvalidOperationException("Key already exists.");
            }

            urlMap.Add(shortKey, longUrl);
            return shortUrl;
        }

        public string RetrieveUrl(string shortUrl)
        {
            var key = shortUrl.Replace(BaseUrlSecure, "");
            if (shortUrl.ToLower().StartsWith("http:"))
            {
                key = shortUrl.Replace(BaseUrl, "");
            }

            if (!urlMap.ContainsKey(key))
            {
                throw new KeyNotFoundException("Short URL does not exist.");
            }

            return urlMap[key];
        }

        private string GenerateKey(string longUrl)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
                return BitConverter.ToString(hash).Replace("-", "").Substring(0, 5);
            }
        }

        private string? FindExistingLongUrl(string longUrl)
        {
            if (urlMap.ContainsValue(longUrl))
            {
                var myKey = urlMap.FirstOrDefault(x => x.Value == longUrl).Key;
                return myKey; 
            }
            return null;
        }
    }
}
