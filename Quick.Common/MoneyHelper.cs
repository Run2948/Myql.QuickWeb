/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：MoneyHelper
* 创 建 者：Qing
* 创建时间：2019-01-01 13:52:36
* CLR 版本：4.0.30319.42000
* 保存的文件名：MoneyHelper
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public static class MoneyHelper
    {
        /// <summary>
        /// 金额转换成中文大写金额
        /// </summary>
        /// <param name="lowerMoney">eg:10.74</param>
        /// <returns>拾元柒角肆分</returns>
        public static string MoneyToUpper(this string lowerMoney)
        {
            string functionReturnValue = null;
            bool isNegative = false; // 是否是负数
            if (lowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                lowerMoney = lowerMoney.Trim().Remove(0, 1);
                isNegative = true;
            }
            string strLower = null;
            string strUpstart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            lowerMoney = Math.Round(double.Parse(lowerMoney), 2).ToString(CultureInfo.InvariantCulture);
            if (lowerMoney.IndexOf(".", StringComparison.Ordinal) > 0)
            {
                if (lowerMoney.IndexOf(".", StringComparison.Ordinal) == lowerMoney.Length - 2)
                {
                    lowerMoney = lowerMoney + "0";
                }
            }
            else
            {
                lowerMoney = lowerMoney + ".00";
            }
            strLower = lowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                if (strLower.Substring(strLower.Length - iTemp, 1) == ".")
                    strUpstart = "圆";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "0")
                    strUpstart = "零";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "1")
                    strUpstart = "壹";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "2")
                    strUpstart = "贰";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "3")
                    strUpstart = "叁";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "4")
                    strUpstart = "肆";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "5")
                    strUpstart = "伍";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "6")
                    strUpstart = "陆";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "7")
                    strUpstart = "柒";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "8")
                    strUpstart = "捌";
                else if (strLower.Substring(strLower.Length - iTemp, 1) == "9")
                    strUpstart = "玖";

                switch (iTemp)
                {
                    case 1:
                        strUpstart = strUpstart + "分";
                        break;
                    case 2:
                        strUpstart = strUpstart + "角";
                        break;
                    case 3:
                        strUpstart = strUpstart + "";
                        break;
                    case 4:
                        strUpstart = strUpstart + "";
                        break;
                    case 5:
                        strUpstart = strUpstart + "拾";
                        break;
                    case 6:
                        strUpstart = strUpstart + "佰";
                        break;
                    case 7:
                        strUpstart = strUpstart + "仟";
                        break;
                    case 8:
                        strUpstart = strUpstart + "万";
                        break;
                    case 9:
                        strUpstart = strUpstart + "拾";
                        break;
                    case 10:
                        strUpstart = strUpstart + "佰";
                        break;
                    case 11:
                        strUpstart = strUpstart + "仟";
                        break;
                    case 12:
                        strUpstart = strUpstart + "亿";
                        break;
                    case 13:
                        strUpstart = strUpstart + "拾";
                        break;
                    case 14:
                        strUpstart = strUpstart + "佰";
                        break;
                    case 15:
                        strUpstart = strUpstart + "仟";
                        break;
                    case 16:
                        strUpstart = strUpstart + "万";
                        break;
                    default:
                        strUpstart = strUpstart + "";
                        break;
                }

                strUpper = strUpstart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            return isNegative ? "负" + functionReturnValue : functionReturnValue;
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns>二十二</returns>
        public static string NumberToChinese(this int number)
        {
            string res;
            string str = number.ToString();
            string scar = str.Substring(0, 1);
            switch (scar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "零";
                    break;
            }
            if (str.Length <= 1) return res;
            switch (str.Length)
            {
                case 2:
                case 6:
                    res += "十";
                    break;
                case 3:
                case 7:
                    res += "百";
                    break;
                case 4:
                    res += "千";
                    break;
                case 5:
                    res += "万";
                    break;
                default:
                    res += "";
                    break;
            }
            res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            return res;
        }

        /// <summary>
        /// 数字转带圈数字
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns>㉒</returns>
        public static string NumberToCircle(this int number)
        {
            const string allChar = "①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳㉑㉒㉓㉔㉕㉖㉗㉘㉙㉚㉛㉜㉝㉞㉟㊱㊲㊳㊴㊵㊶㊷㊸㊹㊺㊻㊼㊽㊾㊿";
            var charArray = allChar.ToCharArray();
            if (number > charArray.Length)
                return number + "";
            return charArray[number - 1] + "";
        }

        /// <summary>
        /// 数字转Excel中的列字母(从‘0-A’开始)
        /// </summary>
        /// <param name="number">eg:1</param>
        /// <returns>B</returns>
        public static string NumberToExcelColumn(this int number)
        {
            if (number < 0) { throw new Exception("invalid parameter"); }
            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0) number--;
                chars.Insert(0, ((char)(number % 26 + (int)'A')).ToString());
                number = (int)((number - number % 26) / 26);
            } while (number > 0);
 
            return string.Join(string.Empty, chars.ToArray());
        }
    }
}
