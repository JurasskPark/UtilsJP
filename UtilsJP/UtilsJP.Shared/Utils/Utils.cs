using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace UtilsJP
{
    /// <summary>
    /// The class provides helper methods for the module.
    /// <para>Класс, предоставляющий вспомогательные методы для модуля.</para>
    /// </summary>
    public static class Utils
    {
        #region The module code
        /// <summary>
        /// The module code.
        /// </summary>
        private static string moduleCode;
        public static string ModuleCode
        {
            get
            {
                return moduleCode;
            }
            set
            {
                moduleCode = value;
            }
        }

        #endregion The module code

        #region Get Bit Value From Integer
        /// <summary>
        /// Get Bit Value From Integer
        /// </summary>
        public static void GetBitValueFromInteger(int valueInput, int index, out int bit)
        {
            bit = 0;

            try
            {     
                BitArray arr = new BitArray(BitConverter.GetBytes(valueInput));
                bit = Convert.ToInt32(arr[index]);
            }
            catch { }
        }
        #endregion Get Bit Value From Integer

        #region Schedule
        /// <summary>
        /// Schedule
        /// </summary>
        public enum ScheduleMode { None = 0, Secondly = 1, Minutly = 2, Hourly = 3, Daily = 4 };

        public static DateTime CalculateTriggerTime(DateTime currentTime, TimeSpan processTime, ScheduleMode scheduleMode)
        {
            DateTime nextTime = new DateTime();
         
            nextTime = currentTime;

            switch (scheduleMode)
            {
                case ScheduleMode.Secondly:
                    {
                        nextTime = nextTime.Add(new TimeSpan(0, 0, 0, processTime.Seconds, nextTime.Millisecond));
                    }
                    break;
                case ScheduleMode.Minutly:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, 0, 0, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(0, 0, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;

                case ScheduleMode.Hourly:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, 0, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(0, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;

                case ScheduleMode.Daily:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, nextTime.Hour, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(processTime.Days, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;
            }
            return nextTime;
        }

        #endregion Schedule

        #region IsNullString

        public static string NullToString(object value)
        {
            try
            {
                // check for null
                if (value == null) return "";

                var type = value.GetType();

                // check for invalid values in date times.
                if (type == typeof(DateTime))
                {
                    if (((DateTime)value) == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    var date = (DateTime)value;

                    if (date.Millisecond > 0)
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    else
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                // use only the local name for qualified names.
                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)value).Name;
                }

                // use only the name for system types.
                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)value).FullName;
                }

                // treat byte arrays as a special case.
                if (type == typeof(byte[]))
                {
                    var bytes = (byte[])value;

                    var buffer = new StringBuilder(bytes.Length * 3);

                    foreach (var character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append(".");
                    }

                    return buffer.ToString();
                }

                // show the element type and length for arrays.
                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)value).Length}]{result}";
                }

                // instances of array are always treated as arrays of objects.
                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"Object[{((Array)value).Length}]{result}";
                }

                // default behavior.
                return value.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #endregion IsNullString

        #region UTC

        /// <summary>
        /// Converts the specified date and time to the local time.
        /// </summary>
        public static DateTime UtcToLocalTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }

        /// <summary>
        /// Converts local date and time to the univeral time.
        /// </summary>
        public static DateTime LocalToUtcTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }

        /// <summary>
        /// Converts double to the time.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static DateTime FromOADate(double unixTimeStamp)
        {
            return new DateTime(DoubleDateToTicks(unixTimeStamp), DateTimeKind.Unspecified);
        }

        static internal long DoubleDateToTicks(double value)
        {
            if (value >= 2958466.0 || value <= -657435.0)
                throw new ArgumentException("Not a valid value");
            long num1 = (long)(value * 86400000.0 + (value >= 0.0 ? 0.5 : -0.5));
            if (num1 < 0L)
                num1 -= num1 % 86400000L * 2L;
            long num2 = num1 + 59926435200000L;
            if (num2 < 0L || num2 >= 315537897600000L)
                throw new ArgumentException("Not a valid value");
            return num2 * 10000L;
        }

        #endregion UTC

        #region MD5

        public static string CalculateMD5Hash(string input)
        {
            MD5 mD = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            byte[] array = mD.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        #endregion MD5

        #region Exception Error
        public static string InfoError(Exception ex, bool showForm = false)
        {
            string errMsg = string.Empty;
            // Get stack trace for the exception with source file information
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();

            return errMsg = $"[ERROR] = " + ex.Message + Environment.NewLine +
                             "[ST] = " + NullToString(st) + Environment.NewLine +
                             "[FRAME] = " + NullToString(frame) + Environment.NewLine +
                             "[LINE] = " + NullToString(line) + Environment.NewLine;
        }

        #endregion Exception Error
    }
}
