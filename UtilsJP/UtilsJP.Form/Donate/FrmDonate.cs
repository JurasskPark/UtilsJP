using System.Collections;
using System.Xml;

namespace Donate
{
    public partial class DonateScreen : Form
    {
        #region Member Variables

        // Threading
        private static DonateScreen ms_frmDonate = null;
        private static Thread ms_oThread = null;

        // Fade in and fade out. Each time the timer runs, the fade in and fade out increases or decreases continuously.
        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .08;
        private const int m_TimerInterval = 50;

        // Status
        private string m_sText;
        private string m_sTimeRemaining;
        private double m_dblCompletionFraction = 0.0;

        // Progress smoothing
        private double m_dblLastCompletionFraction = 0.0;
        private double m_dblPBIncrementPerTimerInterval = .015;

        // Self-calibration support
        private int m_iIndex = 1;
        private int m_iActualTicks = 0;
        private ArrayList m_alPreviousCompletionFraction;
        private ArrayList m_alActualTimes = new ArrayList();
        private DateTime m_dtStart;
        private bool m_bFirstLaunch = false;
        private bool m_bDTSet = false;

        #endregion Member Variables

        #region Public Member Text
        public static string TextRU = "Здравствуйте, уважаемый пользователь!\r\nЯ обращаюсь к вам с просьбой поддержать приложение, внеся свой вклад в его развитие. Сейчас приложение находится на начальном этапе и ему очень нужна ваша помощь. С вашей помощью приложение сможет и дальше развиваться, обретая новые и улучшая существующие функции.\r\nДля того чтобы помочь, вы можете пожертвовать деньги на лицензию - тем самым поддержав проект. Для связи используйте почтовый адрес или другую информацию, указанную в приложении.";
        public static string TextEN = "Hello, dear user!\r\nI am asking you to support the application by contributing to its development. Now the application is at the initial stage and it really needs your help. With your help, the application will be able to develop further, acquiring new and improving existing functions.\r\nIn order to help, you can donate money for a license - thereby supporting the project. For communication, use the postal address or other information specified in the application.";
        #endregion Public Member Text

        /// <summary>
        /// Base constructor
        /// </summary>
        public DonateScreen()
        {
            InitializeComponent();

            // Initialize transparency to fully transparent
            this.Opacity = 0.0;

            // Initialize transparency to fully transparent
            tmrTimer.Interval = m_TimerInterval;

            // Start Timer
            tmrTimer.Start();
        }

        #region Public Static Methods
        /// <summary>
        /// Display the startup form. If the form does not exist, it will be displayed after creation.
        /// </summary>
        static public void ShowDonateScreen()
        {
            // Make sure startup creation is only loaded once
            if (ms_frmDonate != null)
                return;



            // Create a thread to update status
            ms_oThread = new Thread(() =>
            {
                ms_frmDonate = new DonateScreen();
                Application.Run(ms_frmDonate);
            })
            {
                IsBackground = true
            };

            // Set thread unit status
            ms_oThread.SetApartmentState(ApartmentState.STA);

            // Start thread
            ms_oThread.Start();

            // Wait for the startup form to be created
            while (ms_frmDonate == null || ms_frmDonate.IsHandleCreated == false)
            {
                Thread.Sleep(m_TimerInterval);
            }
        }

        // Close the form without setting the parent.
        static public void CloseDonateScreen()
        {
            if (ms_frmDonate != null && ms_frmDonate.IsDisposed == false)
            {
                // Make it start going away.
                ms_frmDonate.m_dblOpacityIncrement = -ms_frmDonate.m_dblOpacityDecrement;
            }
            ms_oThread = null;  // we don't need these any more.
            ms_frmDonate = null;
        }

        // A static method to set the title and update the reference.
        static public void SetText(string newText)
        {
            if (ms_frmDonate == null)
                return;

            ms_frmDonate.m_sText = newText;

            ms_frmDonate.SetReferenceInternal();
        }

        // Static method called from the initializing application to 
        // give the splash screen reference points.  Not needed if
        // you are using a lot of status strings.
        static public void SetReferencePoint()
        {
            if (ms_frmDonate == null)
                return;
            ms_frmDonate.SetReferenceInternal();

        }
        #endregion Public Static Methods

        #region Private Methods

        // Internal method for setting reference points.
        private void SetReferenceInternal()
        {
            if (m_bDTSet == false)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }
            double dblMilliseconds = ElapsedMilliSeconds();
            m_alActualTimes.Add(dblMilliseconds);
        }

        // Utility function to return elapsed Milliseconds since the 
        // SplashScreen was launched.
        private double ElapsedMilliSeconds()
        {
            TimeSpan ts = DateTime.Now - m_dtStart;
            return ts.TotalMilliseconds;
        }

        // Function to read the checkpoint intervals from the previous invocation of the
        // splashscreen from the XML file.
        private void ReadIncrements()
        {
            string sPBIncrementPerTimerInterval = DonateXMLStorage.Interval;
            double dblResult;

            if (Double.TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblResult) == true)
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = .0015;

            string sPBPreviousPctComplete = DonateXMLStorage.Percents;

            if (sPBPreviousPctComplete != "")
            {
                string[] aTimes = sPBPreviousPctComplete.Split(null);
                m_alPreviousCompletionFraction = new ArrayList();

                for (int i = 0; i < aTimes.Length; i++)
                {
                    double dblVal;
                    if (Double.TryParse(aTimes[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblVal) == true)
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
            }
        }

        // Method to store the intervals (in percent complete) from the current invocation of
        // the splash screen to XML storage.
        private void StoreIncrements()
        {
            string sPercent = "";
            double dblElapsedMilliseconds = ElapsedMilliSeconds();
            for (int i = 0; i < m_alActualTimes.Count; i++)
            {
                sPercent += ((double)m_alActualTimes[i] / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";
            }

            DonateXMLStorage.Percents = sPercent;

            m_dblPBIncrementPerTimerInterval = 1.0 / (double)m_iActualTicks;

            DonateXMLStorage.Interval = m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public static DonateScreen GetSplashScreen()
        {
            return ms_frmDonate;
        }

        #endregion Private Methods

        #region Event Handlers
        // Tick Event handler for the Timer control.  Handle fade in and fade out and paint progress bar. 
        private void tmrTimer_Tick(object sender, System.EventArgs e)
        {
            this.txtText.Text = this.m_sText;

            // Calculate opacity
            if (m_dblOpacityIncrement > 0)      // Starting up splash screen
            {
                m_iActualTicks++;
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else // Closing down splash screen
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                {
                    StoreIncrements();
                    tmrTimer.Stop();
                    this.Close();
                }
            }

            // Paint progress bar
            if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
            {
                m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
            }

            TimeSpan timeSpan = DateTime.Now - this.m_dtStart;
            m_sTimeRemaining = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
            lblTime.Text = m_sTimeRemaining;
        }

        private void txtText_MouseClick(object sender, MouseEventArgs e)
        {
            picLoad.Focus();
        }

        private void txtText_MouseDown(object sender, MouseEventArgs e)
        {
            picLoad.Focus();
        }

        // Close the form if they double click on it.
        private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
        {
            CloseDonateScreen();
        }
        #endregion Event Handlers

    }

    #region Auxiliary Classes 
    /// <summary>
    /// A specialized class for managing XML storage for the splash screen.
    /// </summary>
    internal class DonateXMLStorage
    {
        private static string ms_StoredValues = "Donate.xml";
        private static string ms_DefaultPercents = "";
        private static string ms_DefaultIncrement = ".015";


        // Get or set the string storing the percentage complete at each checkpoint.
        static public string Percents
        {
            get { return GetValue("Percents", ms_DefaultPercents); }
            set { SetValue("Percents", value); }
        }
        // Get or set how much time passes between updates.
        static public string Interval
        {
            get { return GetValue("Interval", ms_DefaultIncrement); }
            set { SetValue("Interval", value); }
        }

        // Store the file in a location where it can be written with only User rights. (Don't use install directory).
        static private string StoragePath
        {
            get { return Path.Combine(Application.UserAppDataPath, ms_StoredValues); }
        }

        // Helper method for getting inner text of named element.
        static private string GetValue(string name, string defaultValue)
        {
            if (!File.Exists(StoragePath))
                return defaultValue;

            try
            {
                XmlDocument docXML = new XmlDocument();
                docXML.Load(StoragePath);
                XmlElement elValue = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
                return (elValue == null) ? defaultValue : elValue.InnerText;
            }
            catch
            {
                return defaultValue;
            }
        }

        // Helper method for setting inner text of named element.  Creates document if it doesn't exist.
        static public void SetValue(string name,
             string stringValue)
        {
            XmlDocument docXML = new XmlDocument();
            XmlElement elRoot = null;
            if (!File.Exists(StoragePath))
            {
                elRoot = docXML.CreateElement("root");
                docXML.AppendChild(elRoot);
            }
            else
            {
                docXML.Load(StoragePath);
                elRoot = docXML.DocumentElement;
            }
            XmlElement value = docXML.DocumentElement.SelectSingleNode(name) as XmlElement;
            if (value == null)
            {
                value = docXML.CreateElement(name);
                elRoot.AppendChild(value);
            }
            value.InnerText = stringValue;
            docXML.Save(StoragePath);
        }
    }
    #endregion Auxiliary Classes
}
