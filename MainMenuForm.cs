using System;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Municipal Services - Main Menu";
            this.Width = 400;
            this.Height = 300;

            Button btnReportIssues = new Button() { Text = "Report Issues", Width = 200, Top = 50, Left = 100 };
            Button btnLocalEvents = new Button() { Text = "Local Events & Announcements", Width = 200, Top = 100, Left = 100 };
            Button btnServiceStatus = new Button() { Text = "Service Request Status (Coming Soon)", Width = 200, Top = 150, Left = 100, Enabled = false };

            btnReportIssues.Click += (s, e) => { new ReportIssuesForm().ShowDialog(); };
            btnLocalEvents.Click += (s, e) => { new LocalEventsForm().ShowDialog(); };

            Controls.Add(btnReportIssues);
            Controls.Add(btnLocalEvents);
            Controls.Add(btnServiceStatus);
        }
    }
}
