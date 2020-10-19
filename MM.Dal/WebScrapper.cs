using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using MM.Common;
using MM.Common.Models;
using MM.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MM.Dal
{

    internal class WebScrapper
    {

        private static ILogger Logger { get; set; }


        static WebScrapper()
        {
            Logger = LogManager.CreateLogger<WebScrapper>();
        }



        /// <summary>
        /// read celebs from web page using HtmlAgilityPack package
        /// </summary>
        /// <returns>data or null on error</returns>
        internal IEnumerable<Celeb> Get_Celebs_From_WebSite()
        {
                 string celebsUrl                    = "https://www.imdb.com/list/ls052283250/";
                var celebs                          = new List<Celeb>();


                HtmlWeb website                     = new HtmlWeb();
                website.AutoDetectEncoding          = false;
                website.OverrideEncoding            = Encoding.Default;


                HtmlDocument Doc                    = website.Load(celebsUrl);

                var celebItemNodes                  = Doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("mode-detail")).ToList();
                var celebIndex                      = 1;
                foreach (var celebItemNode in celebItemNodes)
                {
                    //BL: find nodes
                    var celeb_index_node            = celebItemNode.Descendants().Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Equals("lister-item-index unbold text-primary")).FirstOrDefault();

                    var celeb_text_node             = celebItemNode.Descendants().Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Equals("text-muted text-small")).FirstOrDefault();

                    var celeb_image_node            = celebItemNode.Descendants().Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lister-item-image")).FirstOrDefault();
                    var celeb_img_node              = celeb_image_node.Descendants("a").FirstOrDefault().Descendants("img").FirstOrDefault();
                    var celeb_anchore_node          = celeb_image_node.Descendants("a").FirstOrDefault();

                    
                    var celeb_detailsUrl            = celeb_anchore_node.GetAttributeValue("href", null);
                    string celev_biography_url      = celeb_detailsUrl.Remove(celeb_detailsUrl.IndexOf("?")) + "bio?ref_=nm_ov_bio_sm"; //faster than details
                    HtmlDocument celebBiographyDoc  = website.Load("https://www.imdb.com/" + celev_biography_url);
                    var celeb_birthDate_node        = celebBiographyDoc.DocumentNode.Descendants("time").FirstOrDefault();
                    
              


                    //BL: Parse nodes
                var role                = celeb_text_node.InnerText.Trim().Split('|')[0].Trim();
                    var name                = celeb_img_node.GetAttributeValue("alt", string.Empty);
                    string gender           = null;
                    switch (role)
                    {
                        case    "Actress":
                        {
                            gender = "F";
                            break;
                        }
                        case "Actor":
                        {
                            gender = "M";
                            break;
                        }
                    }
                   var imapgeUri            = celeb_img_node.GetAttributeValue("src", string.Empty);
                    var birthDateAsString   = celeb_birthDate_node.Attributes["datetime"].Value;
                    var birthYear           = int.Parse(birthDateAsString.Split('-')[0]);
                    var birthMonth          = int.Parse(birthDateAsString.Split('-')[1]);
                    var birthDay            = int.Parse(birthDateAsString.Split('-')[2]);
                    var birthDate           = new DateTime(birthYear, birthMonth, birthDay);                  
                    //gender

                    //BL: add item to to the list
                    var Celeb = new Celeb(celebIndex, name, role, birthDate, gender, imapgeUri);
                    celebs.Add(Celeb);
                    celebIndex++;
                    if ((celebIndex % 10) == 0)
                    {
                    Logger.LogInformation("Scrapper ...  built " + celebIndex + " objects.");
                    }

                }


                return celebs;
            }
        

    }
}
