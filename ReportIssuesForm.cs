using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        private List<Issue> issuesList = new List<Issue>();

        public ReportIssuesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Report Municipal Issues";
            this.Width = 500;
            this.Height = 400;

            Label lblLocation = new Label() { Text = "Location:", Left = 20, Top = 20 };
            TextBox txtLocation = new TextBox() { Left = 120, Top = 20, Width = 300 };

            Label lblCategory = new Label() { Text = "Category:", Left = 20, Top = 60 };
            ComboBox cmbCategory = new ComboBox() { Left = 120, Top = 60, Width = 300 };
            cmbCategory.Items.AddRange(new string[] { "Roads", "Water", "Electricity", "Sanitation", "Other" });

            Label lblDescription = new Label() { Text = "Description:", Left = 20, Top = 100 };
            RichTextBox rtbDescription = new RichTextBox() { Left = 120, Top = 100, Width = 300, Height = 80 };

            Button btnAttach = new Button() { Text = "Attach File", Left = 120, Top = 200, Width = 100 };
            Label lblFile = new Label() { Text = "No file selected", Left = 230, Top = 205, Width = 200 };

            Button btnSubmit = new Button() { Text = "Submit", Left = 120, Top = 250, Width = 100 };
            Button btnBack = new Button() { Text = "Back", Left = 240, Top = 250, Width = 100 };

            ProgressBar progressBar = new ProgressBar() { Left = 120, Top = 300, Width = 300, Visible = false };

            btnAttach.Click += (s, e) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    lblFile.Text = open.FileName;
                }
            };

            btnSubmit.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtLocation.Text) || cmbCategory.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill in all required fields.", "Error");
                    return;
                }

                Issue issue = new Issue()
                {
                    Location = txtLocation.Text,
                    Category = cmbCategory.SelectedItem.ToString(),
                    Description = rtbDescription.Text,
                    AttachmentPath = lblFile.Text
                };

                issuesList.Add(issue);
                progressBar.Visible = true;
                progressBar.Value = 100;

                MessageBox.Show("Issue successfully reported! Thank you for helping your community.");
                txtLocation.Clear();
                rtbDescription.Clear();
                cmbCategory.SelectedIndex = -1;
                lblFile.Text = "No file selected";
                progressBar.Visible = false;
            };

            btnBack.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] {
                lblLocation, txtLocation,
                lblCategory, cmbCategory,
                lblDescription, rtbDescription,
                btnAttach, lblFile,
                btnSubmit, btnBack,
                progressBar
            });
        }
    }

    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
    }
}
