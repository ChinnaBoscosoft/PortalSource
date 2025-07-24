using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bosco.Utility
{
    public static class ConvertRuppessInWord
    {
        // Function for conversion of a Indian Rupees into words
        //   Parameter - accept a Currency
        //   Returns the number in words format
        //====================================================
        private static object objLockRs = new Object();

        public static string GetRupeesToWord(string RupeesValue)
        {
            lock (objLockRs)
            {
                return ConvertRupeesToWord(RupeesValue);
            }
        }

        private static string ConvertRupeesToWord(string RupeesValue)
        {
            double AmtValue = 0;
            int DecimalPlace = 0;
            int FractionPart = 0;
            int iCount = 0;

            string AmtNumber = "";
            string TempWord = "";
            string Paisa = "";
            string Hundreds = "";
            string Words = "";
            string[] place = new string[11];

            place[0] = " Thousand ";
            place[2] = " Lakh ";
            place[4] = " Crore ";
            place[6] = " Arab ";
            place[8] = " Kharab ";
            place[10] = " ";

            //INSTANT C# TODO TASK: The 'On Error Resume Next' statement is not converted by Instant C#:
            //On Error Resume Next
            // Convert AmtNumber to a string, trimming extra spaces.

            double.TryParse((string.IsNullOrEmpty(RupeesValue) ? RupeesValue : RupeesValue.Trim()), out AmtValue);
            AmtNumber = AmtValue.ToString("#0.00");

            if (AmtValue == 0) return "Zero Only";

            // Find decimal place.
            DecimalPlace = AmtNumber.IndexOf(".");

            // If we find decimal place...
            if (DecimalPlace >= 0)
            {
                // Convert Paisa
                TempWord = AmtNumber.Substring(DecimalPlace + 1);
                int.TryParse(TempWord, out FractionPart);

                if (FractionPart > 0)
                {
                    Paisa = " and " + ConvertTens(TempWord) + " Paisa";
                }

                // Strip off paisa from remainder to convert.
                AmtNumber = AmtNumber.Substring(0, DecimalPlace);
            }

            //===============================================================
            string TM = ""; // If AmtNumber between Rs.1 To 99 Only.
            TM = AmtNumber;

            if (AmtNumber.Length >= 2)
            {
                TM = AmtNumber.Substring(AmtNumber.Length - 2);
            }

            if (AmtNumber.Length > 0 & AmtNumber.Length <= 2)
            {
                if (TM.Length == 1)
                {
                    Words = ConvertDigit(TM);
                    return " " + Words + Paisa + " Only";
                }
                else if (TM.Length == 2)
                {
                    Words = ConvertTens(TM);
                    return " " + Words + Paisa + " Only";
                }
            }
            //===============================================================


            // Convert last 3 digits of AmtNumber to ruppees in word.
            Hundreds = ConvertHundreds(AmtNumber.Substring(AmtNumber.Length - 3));

            // Strip off last three digits
            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 3);

            iCount = 0;
            while (AmtNumber != "")
            {
                //Strip last two digits
                TempWord = AmtNumber;

                if (AmtNumber.Length >= 2)
                {
                    TempWord = AmtNumber.Substring(AmtNumber.Length - 2);
                }

                if (AmtNumber.Length == 1)
                {
                    if (Words.Trim() == "Thousand" || Words.Trim() == "Lakh  Thousand"
                        || Words.Trim() == "Lakh" || Words.Trim() == "Crore"
                        || Words.Trim() == "Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab  Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab" || Words.Trim() == "Kharab  Arab  Crore  Lakh  Thousand"
                        || Words.Trim() == "Kharab")
                    {
                        Words = ConvertDigit(TempWord) + place[iCount];
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 1);
                    }
                    else
                    {
                        Words = ConvertDigit(TempWord) + place[iCount] + Words;
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 1);
                    }
                }
                else
                {
                    if (Words.Trim() == "Thousand" || Words.Trim() == "Lakh  Thousand"
                        || Words.Trim() == "Lakh" || Words.Trim() == "Crore"
                        || Words.Trim() == "Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab  Crore  Lakh  Thousand" || Words.Trim() == "Arab")
                    {
                        Words = ConvertTens(TempWord) + place[iCount];
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                    }
                    else
                    {
                        //=================================================================
                        // if only Lakh, Crore, Arab, Kharab
                        string wrd = (ConvertTens(TempWord) + place[iCount]);

                        if (wrd.Trim() == "Lakh" || wrd.Trim() == "Crore" || wrd.Trim() == "Arab")
                        {
                            //Words = Words;
                            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                        }
                        else
                        {
                            Words = wrd + Words;
                            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                        }
                    }
                }

                iCount += 2;
            }

            return "" + Words + Hundreds + Paisa + " Only";
        }

        // Conversion for hundreds
        //*****************************************
        private static string ConvertHundreds(string AmtNumber)
        {
            string Result = "";
            double amt = 0;
            double.TryParse(AmtNumber, out amt);

            // Exit if there is nothing to convert.
            if (amt == 0)
            {
                return "";
            }

            // Append leading zeros to number.
            AmtNumber = ("000" + AmtNumber).Substring(("000" + AmtNumber).Length - 3);

            // Do we have a hundreds place digit to convert?
            if (AmtNumber.ToString().Substring(0, 1) != "0")
            {
                Result = ConvertDigit(AmtNumber.Substring(0, 1)) + " Hundred ";
            }

            // Do we have a tens place digit to convert?
            if (AmtNumber.Substring(1, 1) != "0")
            {
                Result = Result + ConvertTens(AmtNumber.Substring(1));
            }
            else
            {
                // If not, then convert the ones place digit.
                Result = Result + ConvertDigit(AmtNumber.Substring(2));
            }

            return Result.Trim();
        }

        // Conversion for tens
        //*****************************************
        private static string ConvertTens(string MyTens)
        {
            string Result = "";

            // Is value between 10 and 19?
            if (MyTens.Substring(0, 1) == "1")
            {
                switch (MyTens)
                {
                    case "10":
                        Result = "Ten";
                        break;
                    case "11":
                        Result = "Eleven";
                        break;
                    case "12":
                        Result = "Twelve";
                        break;
                    case "13":
                        Result = "Thirteen";
                        break;
                    case "14":
                        Result = "Fourteen";
                        break;
                    case "15":
                        Result = "Fifteen";
                        break;
                    case "16":
                        Result = "Sixteen";
                        break;
                    case "17":
                        Result = "Seventeen";
                        break;
                    case "18":
                        Result = "Eighteen";
                        break;
                    case "19":
                        Result = "Nineteen";
                        break;
                }
            }
            else
            {
                // .. otherwise it's between 20 and 99.
                switch (MyTens.Substring(0, 1))
                {
                    case "2":
                        Result = "Twenty ";
                        break;
                    case "3":
                        Result = "Thirty ";
                        break;
                    case "4":
                        Result = "Forty ";
                        break;
                    case "5":
                        Result = "Fifty ";
                        break;
                    case "6":
                        Result = "Sixty ";
                        break;
                    case "7":
                        Result = "Seventy ";
                        break;
                    case "8":
                        Result = "Eighty ";
                        break;
                    case "9":
                        Result = "Ninety ";
                        break;
                    default:
                        break;
                }

                // Convert ones place digit.
                Result = Result + ConvertDigit(MyTens.Substring(MyTens.Length - 1));
            }

            return Result;
        }

        private static string ConvertDigit(string MyDigit)
        {
            string tempConvertDigit = "";

            switch (MyDigit)
            {
                case "1":
                    tempConvertDigit = "One";
                    break;
                case "2":
                    tempConvertDigit = "Two";
                    break;
                case "3":
                    tempConvertDigit = "Three";
                    break;
                case "4":
                    tempConvertDigit = "Four";
                    break;
                case "5":
                    tempConvertDigit = "Five";
                    break;
                case "6":
                    tempConvertDigit = "Six";
                    break;
                case "7":
                    tempConvertDigit = "Seven";
                    break;
                case "8":
                    tempConvertDigit = "Eight";
                    break;
                case "9":
                    tempConvertDigit = "Nine";
                    break;
            }
            return tempConvertDigit;
        }


        //Function for conversion of Numbers into Readable words like first, second, third

        public static string GetRupeesToReadableWord(string RupeesValue)
        {

            string amountInWords = GetRupeesToWord(RupeesValue);
            amountInWords = amountInWords.Replace("Only", "");
            amountInWords = amountInWords.Replace("Rupees", "");
            amountInWords = amountInWords.Trim();

            if (amountInWords == "One")
                amountInWords = "First ";
            else if (amountInWords == "Two")
                amountInWords = "Second ";
            else if (amountInWords == "Three")
                amountInWords = "Third ";
            else if (amountInWords == "Five")
                amountInWords = "Fifth ";
            else
                amountInWords = amountInWords + "th ";

            return amountInWords;
        }
    }

}

