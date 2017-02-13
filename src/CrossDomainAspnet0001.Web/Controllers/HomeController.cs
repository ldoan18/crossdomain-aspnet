using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrossDomainAspnet0001.Web.Controllers {

public class HomeController : Controller
{
    /// <summary>GET /Home</summary>
    public ActionResult Index()
    {
        System.Text.StringBuilder sbDebug = new System.Text.StringBuilder();
        string dtUnixTimeSeconds = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        string qsCustomTokenId = Request.QueryString["CustomTokenId"];

        if(!String.IsNullOrEmpty(qsCustomTokenId))
        {
            Session["TokenId"] = qsCustomTokenId;
        }

        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Unix Time Seconds : {0}", dtUnixTimeSeconds);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Request QueryString CustomTokenId : {0}", qsCustomTokenId ?? "null");
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("ASP.NET Session Id : {0}", Session.SessionID);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("ASP.NET Session Storage - TokenId : {0}", Session["TokenId"]);
        sbDebug.AppendFormat("\n<br />\n");

        ViewBag.Title = GetAssemblyAttribute<System.Reflection.AssemblyTitleAttribute>().Title.Split('.').FirstOrDefault();
        ViewBag.Message = sbDebug.ToString();
        ViewBag.UnixTimeSeconds = dtUnixTimeSeconds;
        ViewBag.CustomTokenId = qsCustomTokenId;

        return View();
    }

    /// <summary>GET /Home/Sync</summary>
    public void Sync()
    {
        Session["TokenId"] = Request.QueryString["TokenId"] ?? String.Empty;
    }

    /// <summary>Get this assembly attribute from Properties/AssemblyInfo.cs file.</summary>
    public static T GetAssemblyAttribute<T>() where T : Attribute {
        object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), true);
        if(attributes == null || attributes.Length == 0) return null;
        return (T)attributes[0];
    }
}

}
