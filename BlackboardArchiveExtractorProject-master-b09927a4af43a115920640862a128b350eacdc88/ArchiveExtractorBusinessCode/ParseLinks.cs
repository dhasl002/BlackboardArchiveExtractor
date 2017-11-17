using System;
using System.Net;

namespace ArchiveExtractorBusinessCode
{
    public class ParseLinks
    {
        public string Uri { get; set; }                 // initial Uri passed to this class to start request
        public string FinalUri { get; private set; }    // final Uri that the response returns

        public ParseLinks(string Uri)
        {
            this.Uri = Uri;
            this.FinalUri = "";
        }

        /// <summary>
        /// HTTP GET request function for Uri. 
        /// Returns true if resp code is 200 otherwise considered dead.
        /// Some things it should be equipped to handle is Monitor Connectivity.
        /// </summary>
        /// <returns>Boolean of request if alive or not</returns>
        public bool RequestUrl()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozila/5.0";
                request.Timeout = 10000;    //10s
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                FinalUri = resp.ResponseUri.ToString();
                
                int statusCode = (int)resp.StatusCode;

                if (IsAlive(statusCode))
                {
                    //Console.WriteLine($"Response code: {statusCode}");
                    return true;
                }
                return false;
            }
            catch (WebException e)
            {
                //Console.WriteLine("ERR: " + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {   

                    HttpWebResponse response = e.Response as HttpWebResponse;
                    if (response != null)
                    {
                        int statusCode = (int)response.StatusCode;
                        //Console.WriteLine("HTTP Status Code: " + statusCode);
                        return IsAlive(statusCode);
                    }
                    return false;
                    // no http status code available
                }
                return false;
                // no http status code available
            }
        }

        /// <summary>
        /// Helper to check if Uri is an absolute path
        /// </summary>
        /// <param name="Uri">Uri to be checked if it is an absolute Uri</param>
        /// <returns>Boolean of whether it is a an absolute Uri or not</returns>
        public bool IsAbsoluteUri()
        {
            Uri result;
            return System.Uri.TryCreate(Uri, UriKind.Absolute, out result);
        }

        private bool IsAlive(int code)
        {
            if(code == 200)
            {
                return true;
            }
            return false;
        }

    }

}
