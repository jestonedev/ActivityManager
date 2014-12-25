using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
//using CommonData.Types;

namespace Declensions.Unicode
{
    /// <summary>
    /// Класс для преобразования фамилии, имени и отчества (ФИО), наименования должности или подразделения, 
    /// заданных в именительном падеже в форму любого другого падежа, а также для восстановления 
    /// именительного падежа для ФИО, записанного в произвольном падеже. Склонение ФИО выполняется по правилам 
    /// склонения имен собственных, принятых в русском языке. ФИО для склонения может быть задано одной или 
    /// тремя строками при склонении и одной строкой – при восстановлении именительного падежа. Наименование 
    /// должности или подразделения задаются одной строкой.
    /// </summary>
    /// <remarks>
    /// Класс является C# оберткой над библиотекой Плахова С.В. и Покаташкина Г.Л. Padeg.dll
    /// </remarks>
    public static class Declension
    {
        #region Public properties and functions

        private static int m_MaxResultStringBufSize = 500;

        /// <summary>
        /// Размер внутреннего буффера в символах для возвращаемых сторк. По-умолчанию 500.
        /// </summary>
        public static int MaxResultBufSize
        {
            get { return m_MaxResultStringBufSize; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("MaxResultBufSize",
                                                          "Размер внутреннего буфера не может быть меньше 1");
                }
                m_MaxResultStringBufSize = value;
            }
        }

        #region Declension functions

        /// <summary>
        /// Функция выполняет преобразование ФИО, заданного одной строкой и требует явного указания рода. 
        /// Порядок следования составляющих ФИО в строке параметра – фамилия, имя, отчество. Эта функция, 
        /// как и GetSNPDeclension, тоже допускает использование инициалов и может выполнять преобразование имен 
        /// типа китайских. Для корректной работы функции необходимо наличие трех компонент ФИО 
        /// (имена китайского типа допускается задавать двумя словами). В ряде случаев правильно обрабатываются 
        /// ФИО, записанные в формате "Фамилия Имя [Имя]".
        /// </summary>
        /// <param name="surnameNamePatronimic">ФИО0</param>
        /// <param name="gender">Пол</param>
        /// <param name="declensionCase">Падеж</param>
        /// <returns>Результат склонения</returns>
        public static string GetSNPDeclension(string surnameNamePatronimic, Gender gender,
                                              DeclensionCase declensionCase)
        {
            if (surnameNamePatronimic == null)
            {
                throw new ArgumentNullException("surnameNamePatronimic");
            }

            CheckGender(gender);
            CheckDeclensionCase(declensionCase);

            IntPtr[] ptrs = null;
            try
            {
                ptrs = StringsToIntPtrArray(surnameNamePatronimic);

                int resultLen = MaxResultBufSize;
                string s = GetExceptionsDictionaryFileName();
                int err = decGetFIOPadegFS(ptrs[0], (Int32)gender, (Int32)declensionCase,
                                           ptrs[1], ref resultLen);
                int i = s.Length;
                ThrowException(err);
                return Marshal.PtrToStringUni(ptrs[1]);
            }
            finally
            {
                FreeIntPtr(ptrs);
            }
        }

        /// <summary>
        /// Функция выполняет восстановление именительного падежа для ФИО, заданного в произвольном падеже в 
        /// формате "Фамилия Имя Отчество".
        /// </summary>
        /// <param name="surnameNamePatronimic">ФИО</param>
        /// <returns>ФИО в именительном падеже</returns>
        public static string GetNominativeDeclension(string surnameNamePatronimic)
        {
            if (surnameNamePatronimic == null)
            {
                throw new ArgumentNullException("surnameNamePatronimic");
            }

            IntPtr[] ptrs = null;
            try
            {
                ptrs = StringsToIntPtrArray(surnameNamePatronimic);

                int resultLen = MaxResultBufSize;
                int err = decGetNominativePadeg(ptrs[0], ptrs[1], ref resultLen);
                ThrowException(err);
                return Marshal.PtrToStringUni(ptrs[1]);
            }
            finally
            {
                FreeIntPtr(ptrs);
            }
        }

        /// <summary>
        /// Функция предназначена для склонения наименования должностей, записанных одной строкой. 
        /// Начиная с версии библиотеки 3.3.0.21 стала возможной обработка составных должностей. 
        /// Разделителем в этом случае должна быть цепочка символов: пробел, дефис, пробел (‘ - ’). 
        /// При этом, каждая из должностей в в своем составе может содержать дефис (инженер-конструктор).
        /// </summary>
        /// <param name="appointment">Название должности</param>
        /// <param name="declensionCase">Падеж</param>
        /// <returns>Результат склонения</returns>
        public static string GetAppointmentDeclension(string appointment, DeclensionCase declensionCase)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException("appointment");
            }

            CheckDeclensionCase(declensionCase);

            IntPtr[] ptrs = null;
            try
            {
                ptrs = StringsToIntPtrArray(appointment);

                int resultLen = MaxResultBufSize;
                int err = decGetAppointmentPadeg(ptrs[0], (Int32)declensionCase, ptrs[1], ref resultLen);
                ThrowException(err);
                return Marshal.PtrToStringUni(ptrs[1]);
            }
            finally
            {
                FreeIntPtr(ptrs);
            }
        }

        /// <summary>
        /// Функция предназначена для склонения наименования подразделений, записанных одной строкой. 
        /// Кроме подразделений функция также может выполнять склонение и наименований предприятий.
        /// </summary>
        /// <param name="office">Название подразделения</param>
        /// <param name="declensionCase">Падеж</param>
        /// <returns>Результат склонения</returns>
        public static string GetOfficeDeclension(string office, DeclensionCase declensionCase)
        {
            if (office == null)
            {
                throw new ArgumentNullException("office");
            }

            CheckDeclensionCase(declensionCase);

            IntPtr[] ptrs = null;
            try
            {
                ptrs = StringsToIntPtrArray(office);

                int resultLen = MaxResultBufSize;
                int err = decGetOfficePadeg(ptrs[0], (Int32)declensionCase, ptrs[1], ref resultLen);
                ThrowException(err);
                return Marshal.PtrToStringUni(ptrs[1]);
            }
            finally
            {
                FreeIntPtr(ptrs);
            }
        }

        #endregion Declension functions

        #region Service functions

        /// <summary>
        /// Позволяет определить род ФИО. Допускается параметром передавать не только отчество, но и ФИО 
        /// целиком. Главное, чтобы в передаваемой строке последним было отчество.
        /// </summary>
        /// <param name="patronimic">Отчество</param>
        /// <returns>Род</returns>
        public static Gender GetGender(string patronimic)
        {
            if (patronimic == null)
            {
                throw new ArgumentNullException("patronimic");
            }

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.StringToHGlobalUni(patronimic);
                return (Gender)decGetSex(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        #endregion Service functions

        #region Exceptions dictionary functions

        /// <summary>
        /// Устанавливает в качестве рабочего словарь fileName.
        /// Позволяет приложениям работать с различными словарями. Может быть полезной при разграничении прав 
        /// доступа пользователей. Осуществляет установку словаря в качестве рабочего и считывание из него 
        /// информации. 
        /// </summary>
        /// <param name="filename">Имя файла словаря</param>
        /// <returns>true - если словарь установлен и обновлен, иначе - false. </returns>
        public static bool SetExceptionsDictionaryFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            return Convert.ToBoolean(decSetDictionary(fileName));
        }

        /// <summary>
        /// Значение, возвращаемое этой функцией, содержит информацию о том, что словарь исключений найден и 
        /// программа с ним работает. 
        /// </summary>
        /// <returns>true - если словарь найден, иначе - false.</returns>
        public static bool UpdateExceptionsDictionary()
        {
            return Convert.ToBoolean(decUpdateExceptions());
        }

        /// <summary>
        /// Возвращает полное имя словаря исключений. Имя словаря исключений может потребоваться для модификации 
        /// словаря в процессе работы приложения, использующего функции DLL.
        /// </summary>
        /// <returns>Имя файла словаря</returns>
        public static string GetExceptionsDictionaryFileName()
        {
            StringBuilder sb = new StringBuilder(m_MaxResultStringBufSize);
            int tmp = m_MaxResultStringBufSize;
            int err = decGetExceptionsFileName(sb, ref tmp);
            ThrowException(err);
            return sb.ToString();
        }

        #endregion Exceptions dictionary functions

        #endregion Public properties and functions

        #region Private functions and fields

        private static readonly Encoding encoding = Encoding.GetEncoding(1251);

        private static void CheckGender(Gender gender)
        {
            if (!Enum.IsDefined(typeof(Gender), gender) || gender == Gender.NotDefind)
            {
                throw new ArgumentException("Недопустимое значение рода: " + gender, "gender");
            }
        }

        private static void CheckDeclensionCase(DeclensionCase declensionCase)
        {
            if (!Enum.IsDefined(typeof(DeclensionCase), declensionCase) ||
                declensionCase == DeclensionCase.NotDefind)
            {
                throw new ArgumentException("Недопустимое значение падежа: " + declensionCase, "declensionCase");
            }
        }

        private static void FreeIntPtr(IntPtr[] ptrs)
        {
            if (ptrs != null)
            {
                foreach (IntPtr ptr in ptrs)
                    Marshal.FreeHGlobal(ptr);
            }
        }

        private static IntPtr[] StringsToIntPtrArray(params string[] strs)
        {
            IntPtr[] ptrs = new IntPtr[strs.Length + 1];
            for (int i = 0; i < ptrs.Length - 1; i++)
                ptrs[i] = Marshal.StringToHGlobalUni(strs[i]);

            ptrs[ptrs.Length - 1] = Marshal.AllocHGlobal(m_MaxResultStringBufSize);

            return ptrs;
        }

        private static void ThrowException(int errorCode)
        {
            switch (errorCode)
            {
                case 0:
                    return;
                case -1:
                    throw new DeclensionException("Недопустимое значение падежа", errorCode);
                case -2:
                    throw new DeclensionException("Недопустимое значение рода", errorCode);
                case -3:
                    throw new DeclensionException(
                        "Размер буфера недостаточен для размещения результата преобразования ФИО",
                        errorCode);
                case -4:
                    throw new DeclensionException("Размер буфера surname(фамилия) недостаточен", errorCode);
                case -5:
                    throw new DeclensionException("Размер буфера name(имя) недостаточен", errorCode);
                case -6:
                    throw new DeclensionException("Размер буфера patronimic(отчество) недостаточен", errorCode);
            }
        }

        #endregion  Private functions and fields

        #region PadegUC.dll functions and structs

        [DllImport("PadegUC.dll", EntryPoint = "GetFIOPadegFS", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetFIOPadegFS([In] IntPtr fio, [In] Int32 sex, [In] Int32 padeg, [Out] IntPtr result, [In, Out] ref Int32 resultLength);

        [DllImport("PadegUC.dll", EntryPoint = "GetNominativePadeg", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetNominativePadeg([In] IntPtr surnameNamePatronimic, [Out] IntPtr result, [In, Out] ref Int32 resultLength);
        
        [DllImport("PadegUC.dll", EntryPoint = "GetAppointmentPadeg", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetAppointmentPadeg([In] IntPtr appointment, [In] Int32 padeg, [Out] IntPtr result, [In, Out] ref Int32 resultLength);

        [DllImport("PadegUC.dll", EntryPoint = "GetOfficePadeg", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetOfficePadeg([In] IntPtr office, [In] Int32 padeg, [Out] IntPtr result, [In, Out] ref Int32 resultLength);
        
        [DllImport("PadegUC.dll", EntryPoint = "GetSex", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetSex([In] IntPtr patronimic);

        [DllImport("PadegUC.dll", EntryPoint = "SetDictionary", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decSetDictionary([In] [MarshalAs(UnmanagedType.LPTStr)] string fileName);

        [DllImport("PadegUC.dll", EntryPoint = "UpdateExceptions", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decUpdateExceptions();

        [DllImport("PadegUC.dll", EntryPoint = "GetExceptionsFileName", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern Int32 decGetExceptionsFileName([Out] [MarshalAs(UnmanagedType.LPTStr)]StringBuilder result, [In, Out] ref Int32 resultLength);

        #endregion Padeg.dll functions and structs
    }

    /// <summary>
    /// Падежи русского языка
    /// </summary>
    public enum DeclensionCase
    {
        /// <summary>
        /// Падеж не определен
        /// </summary>
        NotDefind = 0,

        /// <summary>
        /// Именительный падеж (Кто? Что?)
        /// </summary>
        Imenit = 1,

        /// <summary>
        /// Родительный падеж (Кого? Чего?)
        /// </summary>
        Rodit = 2,

        /// <summary>
        /// Дательный падеж (Кому? Чему?)
        /// </summary>
        Datel = 3,

        /// <summary>
        /// Винительный падеж (Кого? Что?)
        /// </summary>
        Vinit = 4,

        /// <summary>
        /// Творительный падеж (Кем? Чем?)
        /// </summary>
        Tvorit = 5,

        /// <summary>
        /// Предложный падеж (О ком? О чём?)
        /// </summary>
        Predl = 6
    }

    /// <summary>
    /// Род
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Род неопределен
        /// </summary>
        NotDefind = -1,

        /// <summary>
        /// Мужской род
        /// </summary>
        MasculineGender = 1,

        /// <summary>
        /// Женский род
        /// </summary>
        FeminineGender = 0
    }

    /// <summary>
    /// Класс для исключений библиотеки
    /// </summary>
    [Serializable]
    public class DeclensionException : Exception, ISerializable
    {
        private int m_ErrorCode;

        public DeclensionException()
            : base()
        {
        }

        public DeclensionException(string message)
            : base(message)
        {
        }

        public DeclensionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DeclensionException(string message, int errorCode)
            : this(message)
        {
            m_ErrorCode = errorCode;
        }

        public DeclensionException(string message, int errorCode, Exception innerException)
            : this(message, innerException)
        {
            m_ErrorCode = errorCode;
        }

        private DeclensionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_ErrorCode = info.GetInt32("ErrorCode");
        }

        /// <summary>
        /// Код ошибки
        /// -1 - Недопустимое значение падежа
        /// -2 - Недопустимое значение рода
        /// -3 - Размер буфера недостаточен для размещения результата преобразования ФИО
        /// -4 - Размер буфера surname(фамилия) недостаточен
        /// -5 - Размер буфера name(имя) недостаточен"
        /// -6 - Размер буфера patronimic(отчество) недостаточен
        /// </summary>
        public int ErrorCode
        {
            get { return m_ErrorCode; }
            set { m_ErrorCode = value; }
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ErrorCode", m_ErrorCode);

            base.GetObjectData(info, context);
        }

        #endregion
    }
}