using System.Net;
using System.Text;

namespace RequestLibrary
{
    public class PostRequest
    {
        private HttpWebRequest _request;
        private string _address;

        public Dictionary<string, string> Headers { get; set; }

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
        public WebProxy Proxy { get; set; }
        public PostRequest(string address)
        {
            _address = address;
            Headers = new Dictionary<string, string>();
        }

        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "Post";


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
            _request.Method = "Post";
            _request.CookieContainer = CookiesContainer;
            _request.Accept = Accept;
            _request.Host = Host;
            _request.Proxy = Proxy;
            _request.ContentType = ContentType;

            byte[] SendData = Encoding.UTF8.GetBytes(Data);

            _request.ContentLength = SendData.Length;
            Stream SendStream = _request.GetRequestStream();
            SendStream.Write(SendData, 0 , SendData.Length);
            SendStream.Flush();
            SendStream.Close();

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