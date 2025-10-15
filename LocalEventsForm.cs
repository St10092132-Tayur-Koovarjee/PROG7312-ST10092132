using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        private SortedDictionary<DateTime, EventInfo> eventDict = new SortedDictionary<DateTime, EventInfo>();
        private HashSet<string> categories = new HashSet<string>();
        private Dictionary<string, List<EventInfo>> categoryEvents = new Dictionary<string, List<EventInfo>>();
        private string lastSearch = "";

        public LocalEventsForm()
        {
            InitializeComponent();
            LoadSampleEvents();
        }

        private void InitializeComponent()
        {
            this.Text = "Local Events & Announcements";
            this.Width = 600;
            this.Height = 450;

            Label lblSearch = new Label() { Text = "Search Category:", Left = 20, Top = 20 };
            TextBox txtSearch = new TextBox() { Left = 140, Top = 18, Width = 250 };
            Button btnSearch = new Button() { Text = "Search", Left = 400, Top = 17, Width = 80 };

            ListBox lstEvents = new ListBox() { Left = 20, Top = 60, Width = 540, Height = 250 };
            Label lblRecommendation = new Label() { Left = 20, Top = 330, Width = 540, Height = 50, Text = "" };

            Button btnBack = new Button() { Text = "Back", Left = 20, Top = 390, Width = 80 };

            btnSearch.Click += (s, e) =>
            {
                string category = txtSearch.Text.Trim();
                lstEvents.Items.Clear();

                if (categoryEvents.ContainsKey(category))
                {
                    foreach (var ev in categoryEvents[category])
                    {
                        lstEvents.Items.Add($"{ev.Date.ToShortDateString()} - {ev.Title}");
                    }

                    lastSearch = category;
                    lblRecommendation.Text = GetRecommendations(category);
                }
                else
                {
                    lstEvents.Items.Add("No events found for this category.");
                    lblRecommendation.Text = "";
                }
            };

            btnBack.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch, lstEvents, lblRecommendation, btnBack });
        }

        private void LoadSampleEvents()
        {
            AddEvent(new EventInfo("Water Maintenance", "Utilities", DateTime.Now.AddDays(2)));
            AddEvent(new EventInfo("Community Clean-up", "Community", DateTime.Now.AddDays(4)));
            AddEvent(new EventInfo("Sports Day", "Sports", DateTime.Now.AddDays(6)));
            AddEvent(new EventInfo("Road Repair Notice", "Infrastructure", DateTime.Now.AddDays(3)));
        }

        private void AddEvent(EventInfo ev)
        {
            eventDict[ev.Date] = ev;
            categories.Add(ev.Category);

            if (!categoryEvents.ContainsKey(ev.Category))
                categoryEvents[ev.Category] = new List<EventInfo>();

            categoryEvents[ev.Category].Add(ev);
        }

        private string GetRecommendations(string category)
        {
            if (string.IsNullOrEmpty(category) || !categoryEvents.ContainsKey(category)) return "";

            var similar = categoryEvents[category].Take(2).Select(e => e.Title).ToList();
            return $"You might also be interested in: {string.Join(", ", similar)}";
        }
    }

    public class EventInfo
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public EventInfo(string title, string category, DateTime date)
        {
            Title = title;
            Category = category;
            Date = date;
        }
    }
}
