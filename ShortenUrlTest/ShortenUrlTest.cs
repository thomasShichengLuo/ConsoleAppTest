using ConsoleAppTest;

namespace ShortenUrlTest
{
    public class ShortenUrlTest
    {
        [Fact]
        public void TestShortenAndRetrieveUrl()
        {
            var service = ShortenerService.Instance;
            var originalUrl = "https://google.com/verylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylong/url";
            var shortUrl = service.ShortenUrl(originalUrl);
            var retrievedUrl = service.RetrieveUrl(shortUrl);
            Assert.Equal(originalUrl, retrievedUrl);
        }

        [Fact]
        public void TestHttpShortenAndRetrieveUrl()
        {
            var service = ShortenerService.Instance;
            var originalUrl = "http://google.com/verylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylong/url";
            var shortUrl = service.ShortenUrl(originalUrl);
            var retrievedUrl = service.RetrieveUrl(shortUrl);
            Assert.Equal(originalUrl, retrievedUrl);
        }

        [Fact]
        public void TestShortenUrlDuplicateOriginalUrl()
        {
            var service = ShortenerService.Instance;
            var originalUrl = "https://www.test.com/Duplicateurl";
            var shortUrl = service.ShortenUrl(originalUrl);
            var duplicateShortUrl = service.ShortenUrl(originalUrl);
            var retrievedUrl = service.RetrieveUrl(shortUrl);
            var retrievedDuplicateUrl = service.RetrieveUrl(duplicateShortUrl);
            Assert.Equal(shortUrl, duplicateShortUrl);
            Assert.Equal(retrievedUrl, retrievedDuplicateUrl);
        }

        [Fact]
        public void TestRetrieveNonexistentUrl()
        {
            var service = ShortenerService.Instance;
            Assert.Throws<KeyNotFoundException>(() => service.RetrieveUrl("https://aa.com/TestUrlAddress"));
        }

        [Fact]
        public void TestShortenEmptyUrl()
        {
            var service = ShortenerService.Instance;
            var originalUrl = "";
            var shortUrl = service.ShortenUrl(originalUrl);
            var retrievedUrl = service.RetrieveUrl(shortUrl);
            Assert.Equal(originalUrl, retrievedUrl);

        }

        [Fact]
        public void TestRetrieveEmptyUrl()
        {
            var service = ShortenerService.Instance;
            Assert.Throws<KeyNotFoundException>(() => service.RetrieveUrl(""));
        }
    }
}