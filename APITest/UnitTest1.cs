using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Script.Serialization;
using ASPDotNET;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APITest
{
    [TestClass]
    public class UnitTest1
    {
        private static HttpSelfHostServer _server;

        /// <summary>
        /// 執行每個測試前先把伺服器架起來
        /// https://stackoverflow.com/questions/2382552/is-it-possible-to-execute-code-once-before-all-tests-run
        /// </summary>
        /// <param name="context"></param>
        [TestInitialize]
        public void RunServer()
        {
            _server = new HttpSelfHostServer(MySetting.getServerConfig());
            _server.OpenAsync().Wait();
        }


        [TestMethod]
        public void TestOnlyId()
        {
            //建立一個client
            //https://dotblogs.com.tw/yc421206/2013/11/11/127593
            var client = new HttpClient();
            client.BaseAddress = new Uri(MySetting.GetUrl());
            //在Header加上Accept:application.json，對Laravel伺服器有奇效
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //發一個request給伺服器，方法為post
            var response = client.PostAsync("api/products", GetContent<Product>((new Product() { Id = 1234 }))).Result;
            //只塞id一定不給過
            Assert.IsTrue(!response.IsSuccessStatusCode);
            //每次測試後一定要關
            _server.CloseAsync().Wait();
        }

        private HttpContent GetContent<Class>(Class obj1)
        {
            var serializer = new JavaScriptSerializer();
            var jsonText = serializer.Serialize(obj1);
            StringContent content = new StringContent(jsonText, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
