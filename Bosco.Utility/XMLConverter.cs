using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Reflection;

namespace Bosco.Utility
{
    public static class XMLConverter
    {
        public static string ReadFromXMLFile(string xmlFilename)
        {
            ResultArgs resultArgs = new ResultArgs();
            string xml = "";

            try
            {
                // Load an XML file into the XmlDocument object.
                // Create an XmlDocument object.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilename);
                xml = xmlDoc.OuterXml;
                resultArgs.Success = true;
            }
            catch (Exception err)
            {
                resultArgs.Success = false;
                resultArgs.Exception = err;
            }

            return xml;
        }

        public static string ReadFromXMLFile(string xmlFilename, bool isEncrypted)
        {
            ResultArgs resultArgs = new ResultArgs();
            string xml = "";

            try
            {
                if (isEncrypted)
                {
                    //XmlDocument xmlDoc = XMLBuilder.DecryptXML(xmlFilename);
                    //xml = xmlDoc.OuterXml;
                }
                else
                {
                    xml = ReadFromXMLFile(xmlFilename);
                }
                resultArgs.Success = true;
            }
            catch (Exception err)
            {
                resultArgs.Success = false;
                resultArgs.Exception = err;
            }

            return xml;
        }

        public static void ReadFromXMLString(string xml, DataSet dtSource)
        {
            System.IO.TextReader stringReader = new System.IO.StringReader(xml);

            if (xml != "")
            {
                dtSource.ReadXml(stringReader);
            }
        }

        public static string WriteToXMLString(DataSet dtSource)
        {
            TextWriter stringWriter = new StringWriter();
            dtSource.WriteXml(stringWriter);
            return stringWriter.ToString();
        }

        public static ResultArgs WriteToXMLFile(DataSet dtSource, string xmlFileName)
        {
            ResultArgs resultArgs = new ResultArgs();

            try
            {
                dtSource.WriteXml(xmlFileName, XmlWriteMode.WriteSchema);
                resultArgs.Success = true;
            }
            catch (Exception ex)
            {
                resultArgs.Exception = ex;
                resultArgs.Success = false;
            }

            return resultArgs;
        }

        public static ResultArgs WriteToXMLFile(DataSet dtSource, string xmlFileName, string elementToEncrypt)
        {
            ResultArgs resultArgs = WriteToXMLFile(dtSource, xmlFileName);

            if (elementToEncrypt != "" && resultArgs.Success)
            {
                //resultArgs = XMLBuilder.EncryptXML(xmlFileName, elementToEncrypt);
            }
            return resultArgs;
        }

        public static ResultArgs WriteToXMLFile(string xml, string xmlFileName)
        {
            ResultArgs resultArgs = new ResultArgs();

            // Load an XML file into the XmlDocument object.
            try
            {
                // Create an XmlDocument object.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlDoc.Save(xmlFileName);
                resultArgs.Success = true;
            }
            catch (Exception err)
            {
                resultArgs.Success = false;
                resultArgs.Exception = err;
            }

            return resultArgs;
        }

        public static XmlReader GetXMLReader(string xml)
        {
            TextReader tr = new StringReader(xml);
            XmlReader xr = XmlReader.Create(tr);
            return xr;
        }

        public static ResultArgs DeleteXMLFile(string xml)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = false;

            try
            {
                if (File.Exists(xml))
                {
                    File.Delete(xml);
                    resultArgs.Success = true;
                }
            }
            catch { }

            return resultArgs;
        }

        /// <summary>
        /// Convert XML file into DataSet 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataSet ConvertXMLToDataSet(string fileName)
        {
            DataSet dsReadXML = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                {
                    dsReadXML.ReadXml(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dsReadXML;
        }

    }
}
