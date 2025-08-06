using Donate;
using LicenseaAgreement;
using Splash;
using System.ComponentModel;
using System.Reflection;
using UtilsJP.WinForm;

namespace UtilsJP.WinForm
{
    public partial class FrmDebug : System.Windows.Forms.Form
    {
        public FrmDebug()
        {
            InitializeComponent();
        }

        private void btnSplash_Click(object sender, EventArgs e)
        {
            SplashScreen.ShowSplashScreen();
            SplashScreen.SetTitle("Иницилизация...");
            SplashScreen.SetStatus("Статус 1");
            Thread.Sleep(1000);
            SplashScreen.SetStatus("Статус 2");
            Thread.Sleep(1000);
            SplashScreen.SetStatus("Статус 3");
            Thread.Sleep(1000);
            SplashScreen.CloseSplashScreen();
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            DonateScreen.ShowDonateScreen();
            DonateScreen.TextRU = "Русский текст";
            DonateScreen.SetText(DonateScreen.TextRU);
            Thread.Sleep(2000);
            DonateScreen.TextEN = "English text";
            DonateScreen.SetText(DonateScreen.TextEN);
            Thread.Sleep(2000);
            DonateScreen.CloseDonateScreen();
        }

        private void btnInfoError_Click(object sender, EventArgs e)
        {
            InfoError.FrmInfo frmInfo = new InfoError.FrmInfo();
            frmInfo.SetInfo("error", "stack trace", "frame", "line");
            frmInfo.ShowDialog();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            FrmLicenseaAgreement frmLicenseaAgreement = new FrmLicenseaAgreement();
            frmLicenseaAgreement.SetInfo("Лицензия");
            frmLicenseaAgreement.ShowDialog();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            try
            {
                About.FrmAbout frmAbout = new About.FrmAbout();
                frmAbout.AppTitle = "Utils.Code";
                frmAbout.AppDescription = GetAutoDescription();
                frmAbout.AppVersion = GetAutoVersion();
                frmAbout.AppCopyright = GetAutoCopyright();
                frmAbout.AppBuildDate = GetBuildDateTime();
                frmAbout.AppInfoMore = "Utils.Description";
                //frmAbout.AppLicense = "license";

                string[] linkInfo = new string[6];
                linkInfo[0] = "mailto:jurasskpark@yandex.ru";
                linkInfo[1] = "https://github.com/JurasskPark";
                linkInfo[2] = "https://www.youtube.com/@JurasskPark";
                linkInfo[3] = "https://jurasskpark.ru";
                frmAbout.AppLinkInfo = linkInfo;

                frmAbout.ShowDialog();
            }
            catch (Exception ex) { UtilsJP.Utils.InfoError(ex, true); }
        }

        #region About
        public static string GetAutoCopyright()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
            }
            catch { return string.Empty; }
        }

        public static string GetAutoDescription()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return ((AssemblyDescriptionAttribute)customAttributes[0]).Description.ToString();
            }
            catch { return string.Empty; }
        }

        public static string GetAutoTitle()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute attribute = (AssemblyTitleAttribute)customAttributes[0];
                    if (attribute.Title != "")
                    {
                        return attribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
            catch (Exception) { return string.Empty; }
        }

        public static string GetAutoVersion()
        {
            try
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
            catch { return string.Empty; }
        }

        public static string GetAutoCompany()
        {
            try
            {
                return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
            }
            catch { return string.Empty; }
        }

        public static string GetBuildDateTime()
        {
            try
            {
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                return buildDate.ToString();
            }
            catch { return string.Empty; }
        }

        #endregion About
    }
}
