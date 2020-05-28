using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace ASPDotNET
{
    /// <summary>
    /// 為了給測試專案故抽離出來
    /// </summary>
    public class MySetting
    {
        /// <summary>
        /// 使用函式是為了日後好抽換邏輯(ex:讀檔)
        /// 此處請根據自己需求更換網址
        /// </summary>
        /// <returns></returns>
        static public string GetUrl()
        {
            return "http://localhost:41333";
        }

        /// <summary>
        /// 伺服器設定也抽離出來
        /// </summary>
        /// <returns></returns>
        static public HttpSelfHostConfiguration getServerConfig()
        {
            //{controller}的字根會自動加上controller (ex: api/product => ProductController)來執行
            //{id}則是在相對應的函式變數中名為id的變數
            var config = new HttpSelfHostConfiguration(MySetting.GetUrl());
            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }
}
