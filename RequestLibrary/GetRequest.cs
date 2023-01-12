using System.Net;

namespace RequestLibrary
{
    public class GetRequest
    {
        private HttpWebRequest _request;
        private string _address;

        public Dictionary<string, string> Headers { get; set; }

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public WebProxy Proxy { get; set; }
        public GetRequest(string address)
        {
            _address = address;
            Headers = new Dictionary<string, string>();
        }

        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "GET";


            try
            {

                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream != null)
                    Response = new StreamReader(stream).ReadToEnd();

            }
            catch (Exception)
            {
            }

        }
        public void Run(ref CookieContainer CookiesContainer)
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "GET";
            _request.CookieContainer = CookiesContainer;
            _request.Accept = Accept;
            _request.Host = Host;
            _request.Proxy = Proxy;

            foreach (var Header in Headers)
            {
                _request.Headers.Add(Header.Key, Header.Value);
            }

            try
            {

                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream != null)
                    Response = new StreamReader(stream).ReadToEnd();

            }
            catch (Exception)
            {
            }

        }
    }
}