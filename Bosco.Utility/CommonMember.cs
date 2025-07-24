/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Bosco.Utility.CommonMemberSet;
using System.Web;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using System.Xml;


namespace Bosco.Utility
{
    public class CommonMember
    {
        private NumberSetMember numberSet = null;
        private DateSetMember dateSet = null;
        private ArraySetMember arraySet = null;
        private StringSetMember stringSet = null;
        private ListSetMember listSet = null;
        private ComboSetMember comboSet = null;
        private FileSetMember fileSet = null;
        private EnumSetMember enumSet = null;

        public static string SELECT = "-Select-";
        public static string UPLOAD_PATH = "~/Module/Software/Uploads/";
        public static string DOWNLOAD_PATH = "~/Module/Software/Uploads/";
        public static string DOWNLOAD_FOLDER = "Uploads/";
        public static string DATEFORMAT = "dd/MM/yyyy";
        public static string DATEFORMAT_TIME = "dd/MM//yyyy hh:mm";
        public static string DefaultCountry = "India";
        public static string DefaultCountryId = "94";
        public static string DefaultState = "Tamil Nadu";
        public static string DefaultStateId = "61";
        public static string CountryCode = "91";
        public static string dllname = "Acme.erpLicense.acp";
        public static string dllnameLC = "Acme.erpLC.acp";
        public static int FixedDepositGroup = 14;

        #region EncryptDecrypt
        /// <summary>
        /// accepts a plain text string and returns the string encrypted
        ///</summary>
        ///<param name="strValue"></param>
        ///<returns></returns>
        ///<remarks></remarks>
        public static string EncryptValue(string strValue)
        {
            string strReturn = "";

            if (!String.IsNullOrEmpty(strValue))
            {
                SimpleEncrypt.SimpleEncDec se = new SimpleEncrypt.SimpleEncDec();
                strReturn = se.EncryptString(strValue);
            }

            return strReturn;
        }

        /// <summary>
        /// accepts an encrypted string and returns the string as plain text
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DecryptValue(string strValue)
        {
            string strReturn = "";

            if (!String.IsNullOrEmpty(strValue))
            {
                try
                {
                    SimpleEncrypt.SimpleEncDec se = new SimpleEncrypt.SimpleEncDec();
                    strReturn = se.DecryptString(strValue);
                }
                catch
                {
                    strReturn = strValue;
                }
            }

            return strReturn;
        }
        #endregion

        #region License DLL Creation

        public static string GetXMLAsString()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PagePath.LicenseKeyFileName);

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);

            string str = sw.ToString();// 
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ResultArgs GetLicenceDetails()
        {
            string licencedetails = string.Empty;
            ResultArgs resultarg = new ResultArgs();
            try
            {
                Assembly acperp = Assembly.LoadFrom(dllname);
                if (acperp != null)
                {
                    object acperpproperties = acperp.CreateInstance(dllname);
                    Type type = acperpproperties.GetType();
                    FieldInfo variable = type.GetField("xcontrolaxes");

                    if (variable != null)
                    {
                        licencedetails = variable.GetValue(acperpproperties).ToString();
                        licencedetails = CommonMember.DecryptValue(licencedetails);
                        licencedetails = CommonMember.DecryptValue(licencedetails);
                    }

                    acperp = null;
                }
                resultarg.Success = true;
                resultarg.ReturnValue = licencedetails;
            }
            catch (Exception err)
            {
                resultarg.Message = err.Message;
                resultarg.ReturnValue = String.Empty;
                resultarg.Success = false;
            }
            return resultarg;
        }

        /// <summary>
        /// This Function is used to generate licence file with licence details in dll format
        /// </summary>
        /// <param name="keycontent"></param>
        /// <returns></returns>
        public static ResultArgs CreateLicenceKey(string keycontent)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<String, String> { { "CompilerVersion", "v4.0" } });
                System.CodeDom.Compiler.CompilerParameters cparameters = new CompilerParameters();
                cparameters.GenerateExecutable = false;
                //cparameters.CompilerOptions= " /out:D:\\Temp\\" + dllname;
                cparameters.CompilerOptions = " /out:" + PagePath.AppFilePath + dllname;
                cparameters.OutputAssembly = dllname;
                keycontent = CommonMember.EncryptValue(keycontent);
                keycontent = CommonMember.EncryptValue(keycontent);

                // provider is an instance of CodeDomProvider
                //cparameters.ReferencedAssemblies.Add("System.Xml.dll");
                //if (provider.Supports(GeneratorSupport.Resources))
                //{
                //    cparameters.EmbeddedResources.Add("c:\\t.txt");
                //}

                cparameters.TempFiles.Delete();
                string keyClass = string.Empty;
                StringBuilder strbuilder = new StringBuilder();
                //strbuilder.AppendLine("using System.Xml;");
                strbuilder.AppendLine("namespace Acme.erpLicense");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public class acp");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public static string xcontrolaxes=\"" + keycontent + "\";");
                strbuilder.AppendLine("}");
                strbuilder.AppendLine("}");

                keyClass = strbuilder.ToString();
                CompilerResults compilerResult = provider.CompileAssemblyFromSource(cparameters, keyClass);
                if (compilerResult.Errors.Count > 0)
                {
                    resultarg.Message = "Acme.erp licence not generated";
                    resultarg.ReturnValue = false;
                    resultarg.Success = false;
                    //foreach (CompilerError ce in compilerResult.Errors)
                    // {
                    //    MessageBox.Show("Acme.erp licence not generated " + ce.ToString());
                    //}
                }
                else
                {
                    resultarg.Message = "Acme.erp licence generated sucessfully";
                    resultarg.Success = true;
                    resultarg.ReturnValue = true;
                }


            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.ReturnValue = false;
                resultarg.Message = ex.Message;
            }

            return resultarg;
        }


        /// <summary>
        /// This Function is used to assing local community client system information in dll format
        /// </summary>
        /// <param name="licensecode"></param>
        /// <param name="hocode"></param>
        /// <param name="bocode"></param>
        /// <param name="clientip"></param>
        /// <param name="clientmacaddress"></param>
        /// <returns></returns>
        public static ResultArgs CreateLocalCommunityEnableModuleKey(string licenserequestcode, string licensekey, string hocode, string bocode, string location,
                    string clientip, string clientmacaddress)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                //string dllkeypath = Path.Combine(PagePath.MultilicensekeySettingFileName, hocode + "_" + bocode + "_" + dllnameLC);

                string dllkeypath = Path.Combine(PagePath.MultilicensekeySettingFileName, licenserequestcode + "_" + hocode + "_" + bocode + "_" + dllnameLC);

                CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<String, String> { { "CompilerVersion", "v4.0" } });
                System.CodeDom.Compiler.CompilerParameters cparameters = new CompilerParameters();
                cparameters.GenerateExecutable = false;
                //cparameters.CompilerOptions= " /out:D:\\Temp\\" + dllname;
                //cparameters.CompilerOptions = " /out:" + PagePath.MultilicensekeySettingFileName + @"\LC\" + hocode + "_" + bocode + "_" + dllnameLC;
                cparameters.CompilerOptions = " /out:" + dllkeypath;
                cparameters.OutputAssembly = dllname;
                string Licensecode = CommonMember.EncryptValue(CommonMember.EncryptValue(licenserequestcode));
                string Licensekey = CommonMember.EncryptValue(CommonMember.EncryptValue(licensekey));
                string Hocode = CommonMember.EncryptValue(CommonMember.EncryptValue(hocode));
                string Bocode = CommonMember.EncryptValue(CommonMember.EncryptValue(bocode));
                string Location = CommonMember.EncryptValue(CommonMember.EncryptValue(location));
                string Clientip = CommonMember.EncryptValue(CommonMember.EncryptValue(clientip));
                string Clientmacaddress = CommonMember.EncryptValue(CommonMember.EncryptValue(clientmacaddress));

                // provider is an instance of CodeDomProvider
                //cparameters.ReferencedAssemblies.Add("System.Xml.dll");
                //if (provider.Supports(GeneratorSupport.Resources))
                //{
                //    cparameters.EmbeddedResources.Add("c:\\t.txt");
                //}

                cparameters.TempFiles.Delete();
                string keyClass = string.Empty;
                StringBuilder strbuilder = new StringBuilder();
                //strbuilder.AppendLine("using System.Xml;");
                strbuilder.AppendLine("namespace Acme.erpLC");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public class AcmeerpLC");
                strbuilder.AppendLine("{");
                strbuilder.AppendLine("public static string ref1=\"" + Licensecode + "\";");
                strbuilder.AppendLine("public static string ref2=\"" + Licensekey + "\";");
                strbuilder.AppendLine("public static string ref3=\"" + Hocode + "\";");
                strbuilder.AppendLine("public static string ref4=\"" + Bocode + "\";");
                strbuilder.AppendLine("public static string ref5=\"" + Location + "\";");
                strbuilder.AppendLine("public static string ref6=\"" + Clientip + "\";");
                strbuilder.AppendLine("public static string ref7=\"" + Clientmacaddress + "\";");
                strbuilder.AppendLine("}");
                strbuilder.AppendLine("}");

                keyClass = strbuilder.ToString();
                CompilerResults compilerResult = provider.CompileAssemblyFromSource(cparameters, keyClass);
                if (compilerResult.Errors.Count > 0)
                {
                    resultarg.Message = "Not generated";
                    resultarg.ReturnValue = false;
                    resultarg.Success = false;
                }
                else
                {
                    resultarg.Message = "generated";
                    resultarg.Success = true;
                    resultarg.ReturnValue = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.ReturnValue = false;
                resultarg.Message = ex.Message;
            }

            return resultarg;
        }
        #endregion

        #region Member Group

        public NumberSetMember NumberSet
        {
            get { if (numberSet == null) numberSet = new NumberSetMember(); return numberSet; }
        }

        public DateSetMember DateSet
        {
            get { if (dateSet == null) dateSet = new DateSetMember(); return dateSet; }
        }

        public ArraySetMember ArraySet
        {
            get { if (arraySet == null) arraySet = new ArraySetMember(); return arraySet; }
        }

        public StringSetMember StringSet
        {
            get { if (stringSet == null) stringSet = new StringSetMember(); return stringSet; }
        }

        public ListSetMember ListSet
        {
            get { if (listSet == null) listSet = new ListSetMember(); return listSet; }
        }

        public ComboSetMember ComboSet
        {
            get { if (comboSet == null) comboSet = new ComboSetMember(); return comboSet; }
        }

        public FileSetMember FileSet
        {
            get { if (fileSet == null) fileSet = new FileSetMember(); return fileSet; }
        }

        public EnumSetMember EnumSet
        {
            get { if (enumSet == null) enumSet = new EnumSetMember(); return enumSet; }
        }

        #endregion

        public object GetDynamicInstance(string instanceType, object[] args)
        {

            Type type = Type.GetType(instanceType, false, true);
            object instance = null;

            if (type != null)
            {
                try
                {
                    if (args != null)
                    {
                        instance = System.Activator.CreateInstance(type, args);
                    }
                    else
                    {
                        instance = System.Activator.CreateInstance(type);
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionHandler(e, true);
                }
            }

            return instance;
        }

        public static string ApplicationVirtualPath
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                       HttpContext.Current.Request.ApplicationPath;
            }
        }
    }
}
