using FootballProjectBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FootballGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CRUDManager _crudManager = new CRUDManager();
        public MainWindow()
        {
            InitializeComponent();
            PopulateListBox();
        }
        private void PopulateListBox()
        {
            ListBoxTeams.ItemsSource = _crudManager.RetrieveAll();
        }

        private void PopulateTeamsFields()
        {
            if (_crudManager.SelectedTeams != null)
            {
                TextTeamID.Text = (_crudManager.SelectedTeams.TeamId).ToString();
                TextTeamName.Text = _crudManager.SelectedTeams.TeamName;
                TextTeamDescription.Text = _crudManager.SelectedTeams.TeamDescription;
                TextLeagueTrophies.Text = (_crudManager.SelectedTeams.LeagueTrophies).ToString();
                TextEuropeanTrophies.Text = (_crudManager.SelectedTeams.EuropeanTrophies).ToString();
                
            }
        }

        private void ListBoxTeams_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListBoxTeams.SelectedItem != null)
            {
                _crudManager.SetSelectedTeams(ListBoxTeams.SelectedItem);
                ListBoxPlayers.ItemsSource = _crudManager.RetrieveTeamPlayers(); /* = _crudManager.RetrieveTeamPlayers();*/
                PopulateTeamsFields();
            }
        }

        private void ListBoxPlayers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {

            _crudManager.Update(Int32.Parse(TextTeamID.Text), TextTeamName.Text, TextTeamDescription.Text, Int32.Parse(TextLeagueTrophies.Text), Int32.Parse(TextEuropeanTrophies.Text));
            ListBoxTeams.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            ListBoxTeams.SelectedItem = _crudManager.SelectedTeams;
            PopulateTeamsFields();
        }

        private void TextPlayers_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonCreateATeam_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.CreateTeam(TextTeamName.Text, TextTeamDescription.Text, Int32.Parse(TextLeagueTrophies.Text), Int32.Parse(TextEuropeanTrophies.Text));
            ListBoxTeams.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            ListBoxTeams.SelectedItem = _crudManager.SelectedTeams;
            PopulateTeamsFields();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.DeleteTeam(Int32.Parse(TextTeamID.Text));
            ListBoxTeams.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            ListBoxTeams.SelectedItem = _crudManager.SelectedTeams;
            PopulateTeamsFields();
        }

        private void ButtonViewPlayers_Click(object sender, RoutedEventArgs e)
        {
            PlayersPage page = new PlayersPage();
            page.Show();
            this.Close();
            
        }
    }
}
