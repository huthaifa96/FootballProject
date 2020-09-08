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
    /// Interaction logic for PlayersPage.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Window
    {
        private CRUDManager _crudManager = new CRUDManager();
        public PlayersPage()
        {
            InitializeComponent();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            ListBoxPlayers.ItemsSource = _crudManager.RetrieveAllPlayers();
        }

        private void PopulatePlayersFields()
        {
            if (_crudManager.SelectedPlayers != null)
            {
                TextPlayerID.Text = (_crudManager.SelectedPlayers.PlayerId).ToString();
                TextName.Text = _crudManager.SelectedPlayers.Name;
                TextAge.Text = (_crudManager.SelectedPlayers.Age).ToString();
                TextNationality.Text = _crudManager.SelectedPlayers.Nationality;
                TextTeamID.Text = (_crudManager.SelectedPlayers.TeamId).ToString();
                TextHeight.Text = (_crudManager.SelectedPlayers.HeightInches).ToString();
                TextStrongestFoot.Text = _crudManager.SelectedPlayers.StrongestFoot;
                TextPosition.Text = _crudManager.SelectedPlayers.Position;
            }
        }

        private void ListBoxPlayers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListBoxPlayers.SelectedItem != null)
            {
                _crudManager.SetSelectedPlayers(ListBoxPlayers.SelectedItem);
                ListBoxPlayers.ItemsSource = _crudManager.RetrieveAllPlayers();
                PopulatePlayersFields();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {

            _crudManager.UpdatePlayer(Int32.Parse(TextPlayerID.Text), TextName.Text, Int32.Parse(TextAge.Text), TextNationality.Text, Int32.Parse(TextHeight.Text), TextStrongestFoot.Text, TextPosition.Text);
            ListBoxPlayers.ItemsSource = null;
            // put back the screen data
            PopulatePlayersFields();
        }

        private void TextPlayers_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonCreatePlayer_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.CreatePlayer(TextName.Text, Int32.Parse(TextAge.Text), TextNationality.Text, Int32.Parse(TextTeamID.Text), Int32.Parse(TextHeight.Text), TextStrongestFoot.Text, TextPosition.Text);
            ListBoxPlayers.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            ListBoxPlayers.SelectedItem = _crudManager.SelectedPlayers;
            PopulatePlayersFields();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _crudManager.DeletePlayer (Int32.Parse(TextPlayerID.Text));
            ListBoxPlayers.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            ListBoxPlayers.SelectedItem = _crudManager.SelectedPlayers;
            PopulatePlayersFields();
        }

        private void ButtonViewTeams_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
