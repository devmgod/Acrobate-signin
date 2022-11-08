using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace Demo
{
    class AdobeSignAuthPostRequest
    {
        private string authUrl = "https://secure.na4.adobesign.com/oauth/v2/token";

        static void Main(string[] args)
        {
            AdobeSignAuthPostRequest adobe = new AdobeSignAuthPostRequest();
            adobe.postAuth();
        }

        public void postAuth()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("code", "CBNCKBAAHBCAABAAGRPnbCgj0zuHj0BYK44VF_CfW_mrFzz7");
            dict.Add("client_id", "CBJCHBCAABAA088HhHpSbMGdjXhAupcswWgoWT0l4R8r");
            dict.Add("client_secret", "Mi_vfh31SXcUghHxiZdSPc-dAoj7nq1d");
            dict.Add("grant_type", "authorization_code");
            dict.Add("redirect_uri", "https://oauth.pstmn.io/v1/callback");

            using (HttpClient client = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(dict))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = client.PostAsync(authUrl, content).Result;
                    var token = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(token);
                    Root root = JsonConvert.DeserializeObject<Root>(token);
                    Console.WriteLine("access_token", root.access_token);
                    Console.WriteLine("refresh_token", root.refresh_token);
                    Console.WriteLine("api_access_point", root.api_access_point);
                    Console.WriteLine("web_access_point", root.web_access_point);
                    Console.WriteLine("token_type", root.token_type);
                    Console.WriteLine("expires_in", root.expires_in);
                    Console.ReadLine();
                }
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = client.PostAsync(authUrl, new FormUrlEncodedContent(dict)).Result;
            //    var token = response.Content.ReadAsStringAsync().Result;
            //    Console.WriteLine(token);
            //    Root root = JsonConvert.DeserializeObject<Root>(token);
            //    Console.WriteLine(root.access_token);
            //    Console.WriteLine(root.refresh_token);
            //    Console.WriteLine(root.api_access_point);
            //    Console.WriteLine(root.web_access_point);
            //    Console.WriteLine(root.token_type);
            //    Console.WriteLine(root.expires_in);
            //    Console.ReadLine();
            //}
        }
    }
    class Root
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string api_access_point { get; set; }
        public string web_access_point { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
