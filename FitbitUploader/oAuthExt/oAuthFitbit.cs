using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

public class oAuthFitbit : oAuthBase2
{
    #region Method enum

    public enum Method
    {
        GET,
        POST,
        PUT,
        DELETE
    } ;

    #endregion

    public const string RequestToken = "https://api.fitbit.com/oauth/request_token";
    public const string Authorize = "https://www.fitbit.com/oauth/authorize";
    public const string AccessToken = "https://api.fitbit.com/oauth/access_token";

    private string _consumerKey = "3e3a29f35d064a3e996ccbe8eb1f47a4";
    private string _consumerSecret = "b1ba6cc29e0a44768b4969ae393e241b";

    public oAuthFitbit()
    {
        TokenSecret = "";
        Token = "";
        CallbackConfirmed = false;
    }

    #region Properties

    public string ConsumerKey
    {
        get
        {
            if (_consumerKey.Length == 0)
            {
                _consumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            }
            return _consumerKey;
        }
        set { _consumerKey = value; }
    }

    public string ConsumerSecret
    {
        get
        {
            if (_consumerSecret.Length == 0)
            {
                _consumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            }
            return _consumerSecret;
        }
        set { _consumerSecret = value; }
    }

    public string Token { get; set; }

    public string TokenSecret { get; set; }

    public bool CallbackConfirmed { get; set; }

    #endregion

    /// <summary>
    /// Get the link to Twitter's authorization page for this application.
    /// </summary>
    /// <returns>The url with a valid request token, or a null string.</returns>
    public string AuthorizationLinkGet()
    {
        string ret = null;

        var response = OAuthWebRequest(Method.POST, RequestToken, String.Empty);
        if (response.Length > 0)
        {
            //response contains token and token secret.  We only need the token.
            //oauth_token=36d1871d-5315-499f-a256-7231fdb6a1e0&oauth_token_secret=34a6cb8e-4279-4a0b-b840-085234678ab4&oauth_callback_confirmed=true
            var qs = HttpUtility.ParseQueryString(response);
            if (qs["oauth_token"] != null)
            {
                Token = qs["oauth_token"];
                TokenSecret = qs["oauth_token_secret"];
                CallbackConfirmed = qs["oauth_callback_confirmed"].CompareTo("true") == 0;
                ret = Authorize + "?oauth_token=" + Token;
            }
        }
        return ret;
    }

    /// <summary>
    /// Exchange the request token for an access token.
    /// </summary>
    /// <param name="authToken">The oauth_token is supplied by Twitter's authorization page following the callback.</param>
    public bool AccessTokenGet(string authToken)
    {
        Token = authToken;

        var response = OAuthWebRequest(Method.POST, AccessToken, string.Empty);

        if (response.Length <= 0) return false;
        //Store the Token and Token Secret
        var qs = HttpUtility.ParseQueryString(response);
        if (qs["oauth_token"] != null)
        {
            Token = qs["oauth_token"];
        }
        else return false;
        if (qs["oauth_token_secret"] != null)
        {
            TokenSecret = qs["oauth_token_secret"];
        }
        else return false;

        return true;
    }

    /// <summary>
    /// Submit a web request using oAuth.
    /// </summary>
    /// <param name="method">GET or POST</param>
    /// <param name="url">The full url, including the querystring.</param>
    /// <param name="postData">Data to post (querystring format)</param>
    /// <returns>The web server response.</returns>
    public string OAuthWebRequest(Method method, string url, string postData)
    {
        string outUrl;
        string querystring;
        var ret = "";

        //Setup postData for signing.
        //Add the postData to the querystring.
        if (method == Method.POST)
        {
            if (postData.Length > 0)
            {
                //Decode the parameters and re-encode using the oAuth UrlEncode method.
                var qs = HttpUtility.ParseQueryString(postData);
                postData = "";
                foreach (var key in qs.AllKeys)
                {
                    if (postData.Length > 0)
                    {
                        postData += "&";
                    }
                    qs[key] = HttpUtility.UrlDecode(qs[key]);
                    qs[key] = UrlEncode(qs[key]);
                    postData += key + "=" + qs[key];
                }
                if (url.IndexOf("?") > 0)
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }
                url += postData;
            }
        }

        var uri = new Uri(url);

        var nonce = GenerateNonce();
        var timeStamp = GenerateTimeStamp();

        //Generate Signature
        var sig = GenerateSignature(uri,
                                       ConsumerKey,
                                       ConsumerSecret,
                                       Token,
                                       TokenSecret,
                                       method.ToString(),
                                       timeStamp,
                                       nonce,
                                       out outUrl,
                                       out querystring);


        querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

        //Convert the querystring to postData
        if (method == Method.POST)
        {
            postData = querystring;
            querystring = "";
        }

        if (querystring.Length > 0)
        {
            outUrl += "?";
        }

        if (method == Method.POST || method == Method.GET)
            ret = WebRequest(method, outUrl + querystring, postData);
        //else if (method == Method.PUT)
        //ret = WebRequestWithPut(outUrl + querystring, postData);
        return ret;
    }


    /// <summary>
    /// WebRequestWithPut
    /// </summary>
    /// <param name="method">WebRequestWithPut</param>
    /// <param name="url"></param>
    /// <param name="postData"></param>
    /// <returns></returns>
    public string APIWebRequest(string method, string url, string postData)
    {
        var uri = new Uri(url);
        var nonce = GenerateNonce();
        var timeStamp = GenerateTimeStamp();

        string outUrl, querystring;

        //Generate Signature
        var sig = GenerateSignature( uri,
                                     ConsumerKey,
                                     ConsumerSecret,
                                     Token,
                                     TokenSecret,
                                     method,
                                     timeStamp,
                                     nonce,
                                     out outUrl,
                                     out querystring );

        //querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);
        //NameValueCollection qs = HttpUtility.ParseQueryString(querystring);

        //string finalGetUrl = outUrl + "?" + querystring;

        //webRequest = System.Net.WebRequest.Create(finalGetUrl) as HttpWebRequest;
        var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
        //webRequest.ContentType = "text/xml";
        if (webRequest == null) return "";
        webRequest.Method = method;
        webRequest.Credentials = CredentialCache.DefaultCredentials;
        webRequest.AllowWriteStreamBuffering = true;

        webRequest.PreAuthenticate = true;
        webRequest.ServicePoint.Expect100Continue = false;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        webRequest.Headers.Add("Authorization",
                               "OAuth realm=\"" + "https://api.fibit.com/" + "\",oauth_consumer_key=\"" + ConsumerKey +
                               "\",oauth_token=\"" + Token +
                               "\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"" + HttpUtility.UrlEncode(sig) +
                               "\",oauth_timestamp=\"" + timeStamp + "\",oauth_nonce=\"" + nonce +
                               "\",oauth_verifier=\"" + Verifier + "\", oauth_version=\"1.0\"");

        /*webRequest.Headers.Add( "Authorization",
                       @"OAuth realm=""http://api.fibit.com/"", " +
                       @"oauth_consumer_key=""" + ConsumerKey + @""", " +
                       @"oauth_token=""" + Token + @""", " +
                       @"oauth_signature_method=""HMAC-SHA1"", " +
                       @"oauth_signature=""" + HttpUtility.UrlEncode(sig) + @""", " +
                       @"oauth_timestamp=""" + timeStamp + @""", " +
                       @"oauth_nonce=""" + nonce + @""", " +
                       @"oauth_verifier=""" + Verifier + @""", " +
                       @"oauth_version=""1.0""" );*/
        

        //webRequest.Headers.Add("Authorization", "OAuth oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timeStamp + "\", oauth_consumer_key=\"" + this.ConsumerKey + "\", oauth_token=\"" + this.Token + "\", oauth_signature=\"" + HttpUtility.UrlEncode(sig) + "\", oauth_version=\"1.0\"");
        if (postData != null)
        {
            var fileToSend = Encoding.UTF8.GetBytes(postData);
            webRequest.ContentLength = fileToSend.Length;

            var reqStream = webRequest.GetRequestStream();

            reqStream.Write(fileToSend, 0, fileToSend.Length);
            reqStream.Close();
        }

        var returned = WebResponseGet(webRequest);

        return returned;
    }


    /// <summary>
    /// Web Request Wrapper
    /// </summary>
    /// <param name="method">Http Method</param>
    /// <param name="url">Full url to the web resource</param>
    /// <param name="postData">Data to post in querystring format</param>
    /// <returns>The web server response.</returns>
    public string WebRequest(Method method, string url, string postData)
    {
        StreamWriter requestWriter;

        var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
        if (webRequest == null) return "";
        webRequest.Method = method.ToString();
        webRequest.ServicePoint.Expect100Continue = false;
        //webRequest.UserAgent  = "Identify your application please.";
        //webRequest.Timeout = 20000;

        if (method == Method.POST)
        {
            webRequest.ContentType = "application/x-www-form-urlencoded";

            //POST the data.
            requestWriter = new StreamWriter(webRequest.GetRequestStream());
            try
            {
                requestWriter.Write(postData);
            }
            finally
            {
                requestWriter.Close();
            }
        }

        var responseData = WebResponseGet(webRequest);

        return responseData;
    }

    /// <summary>
    /// Process the web response.
    /// </summary>
    /// <param name="webRequest">The request object.</param>
    /// <returns>The response data.</returns>
    public string WebResponseGet(HttpWebRequest webRequest)
    {
        WebResponse response = null;
        StreamReader responseReader = null;
        string responseData = null;

        try
        {
            response = webRequest.GetResponse();
            responseReader = new StreamReader(response.GetResponseStream());
            responseData = responseReader.ReadToEnd();
        }
        catch(WebException e)
        {
            MessageBox.Show(e.Message);
            response = e.Response;
            responseReader = new StreamReader(response.GetResponseStream());
            responseData = responseReader.ReadToEnd();
        }
        finally
        {
            if (response != null) response.Close();
            //if (responseData != null) webRequest.GetResponse().GetResponseStream().Close();
            if (responseReader != null) responseReader.Close();
        }

        return responseData;
    }
}