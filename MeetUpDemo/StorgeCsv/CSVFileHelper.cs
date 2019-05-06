using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Data;
using System.Xml.Linq;
using System.Xml;
using MeetUpDemo.Model;
using System.Reflection;

namespace MeetUpDemo.StorgeCsv
{
    class CSVFileHelper
    {
        //private static EventItem eventItem = new EventItem();
        //private static group group = new group();
        //private static venue venue = new venue();
        //private static fee fee = new fee();
        //private static PropertyInfo[] eventItemproperties = eventItem.GetType().GetProperties();
        private static PropertyInfo[] eventItemproperties = typeof(EventItem).GetProperties();
        //private static PropertyInfo[] groupItemproperties = group.GetType().GetProperties();
        private static PropertyInfo[] groupItemproperties = typeof(group).GetProperties();
        //private static PropertyInfo[] venueItemproperties = venue.GetType().GetProperties();
        private static PropertyInfo[] venueItemproperties = typeof(venue).GetProperties();
        //private static PropertyInfo[] feeItemproperties = fee.GetType().GetProperties();
        private static PropertyInfo[] feeItemproperties = typeof(fee).GetProperties();

        /// <summary>
        /// Get all the titles of the CSV file
        /// </summary>
        /// <returns></returns>
        static string GetDataTitle()
        {
            var dataTitle = new StringBuilder();
            foreach (var eventItemproperty in eventItemproperties)
            {
                if (eventItemproperty.PropertyType == typeof(string)) //General attribute
                    dataTitle.Append(eventItemproperty.Name + ",");
                else if (eventItemproperty.PropertyType == typeof(group))//Group attribute
                {
                    foreach (var groupItemproperty in groupItemproperties)
                    {
                        dataTitle.Append($"group_{groupItemproperty.Name},");
                    }
                }
                else if (eventItemproperty.PropertyType == typeof(venue))//Venue attribute
                {
                    foreach (var venueItemproperty in venueItemproperties)
                    {
                        dataTitle.Append($"venue_{venueItemproperty.Name},");
                    }
                }
                else if (eventItemproperty.PropertyType == typeof(fee))//Fee attribute
                {
                    foreach (var feeItemproperty in feeItemproperties)
                    {
                        dataTitle.Append($"fee_{feeItemproperty.Name},");
                    }
                }
            }



            return dataTitle.ToString();
        }
        /// <summary>
        /// Get all the data content
        /// </summary>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        static string GetDataContent(string xmlText)
        {
            XDocument xDocument = XDocument.Parse(xmlText);
            var dataContent = new StringBuilder();
            foreach (var eventitem in xDocument.Descendants("item"))//Each Event Item is a row
            {
                foreach (var property in eventItemproperties)
                {
                    var propertyName = property.Name;

                    if (property.PropertyType == typeof(string)) //General attribute
                    {
                        if (eventitem.Element(propertyName) is null) //If the value of this property is null, add a space 
                        {
                            dataContent.Append(" " + ",");
                            continue;
                        }
                        var propertyValue = eventitem.Element(propertyName).Value;
                        if (propertyValue.Contains("\""))
                        {
                            propertyValue = propertyValue.Replace("\"", "\"\""); //Eliminate the effect of quotation marks
                                                                                 //Replacing single quotation marks with double quotation marks
                        }
                        //property.SetValue(eventItem, propertyValue);
                        dataContent.Append($"\"{propertyValue}\",");

                    }
                    else if (property.PropertyType == typeof(Model.group))//Group attribute
                    {

                        var groupXml = eventitem.Descendants(propertyName).First();
                        foreach (var prop in groupItemproperties)
                        {
                            string proName = prop.Name;

                            if (groupXml.Element(proName) is null)
                            {
                                dataContent.Append(" " + ",");
                                continue;
                            }
                            var proValue = groupXml.Element(proName).Value;
                            //prop.SetValue(group, proValue);
                            dataContent.Append($"\"{proValue}\",");
                        }
                        //property.SetValue(eventItem, group);
                        //group = new group();
                    }
                    else if ((property.PropertyType == typeof(Model.venue)))//Venue attribute
                    {
                        if (eventitem.Element(propertyName) is null)
                        {
                            for (int i = 0; i < venueItemproperties.Length; i++)
                            {
                                dataContent.Append(" " + ",");      //if the value of  VenueAttribute is null,Add the corresponding number of spaces
                            }
                            continue;
                        }
                        var venueXml = eventitem.Descendants(propertyName).First();
                        foreach (var prop in venueItemproperties)
                        {
                            string proName = prop.Name;
                            if (venueXml.Element(proName) is null)
                            {
                                dataContent.Append(" " + ",");
                                continue;
                            }
                            var proValue = venueXml.Element(proName).Value;
                            //prop.SetValue(venue, proValue);
                            dataContent.Append($"\"{proValue}\",");
                        }
                        //property.SetValue(eventItem, venue);
                        //venue = new venue();
                    }
                    else if ((property.PropertyType == typeof(Model.fee)))//Fee attribute
                    {

                        if (eventitem.Element(propertyName) is null)
                        {
                            for (int i = 0; i < feeItemproperties.Length; i++)
                            {
                                dataContent.Append(" " + ",");  //if the value of  FeeAttribute is null,Add the corresponding number of spaces
                            }
                            continue;
                        }
                        var venueXml = eventitem.Descendants(propertyName).First();
                        foreach (var prop in feeItemproperties)
                        {
                            string proName = prop.Name;
                            if (venueXml.Element(proName) is null)
                            {
                                dataContent.Append(" " + ",");
                                continue;
                            }
                            var proValue = venueXml.Element(proName).Value;
                            //prop.SetValue(fee, proValue);
                            dataContent.Append($"\"{proValue}\",");
                        }
                        //property.SetValue(eventItem, fee);
                        //fee = new fee();
                    }
                }
                dataContent.AppendLine();
                //eventItem = new EventItem();
            }
            return dataContent.ToString();

        }
        /// <summary>
        /// Write data in XML format into CSV file
        /// </summary>
        /// <param name="xmlText"></param>
        public static void SaveToCsv(string xmlText)
        {
            var inputFolder = ConfigurationManager.AppSettings["CSVFolder"];
            if (!Directory.Exists(inputFolder))
            {
                Directory.CreateDirectory(inputFolder);
            }
            var storageFile = inputFolder + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
            string title = GetDataTitle();
            string content = GetDataContent(xmlText);
            File.WriteAllText(storageFile, title + "\n" + content);
        }

    }
}
