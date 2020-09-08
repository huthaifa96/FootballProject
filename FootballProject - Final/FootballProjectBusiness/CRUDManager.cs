using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FootballProject;
using Microsoft.EntityFrameworkCore;

namespace FootballProjectBusiness
{
    public class CRUDManager
    {
        public Teams SelectedTeams { get; set ; }
        public Players SelectedPlayers { get; set; }
        public List<Teams> RetrieveAll()
        {
            using (var db = new FootballProjectContext())
            {
                return db.Teams.Include(t => t.Players).ToList();
            }
        }

        public List<Players> RetrieveAllPlayers()
        {
            using (var db = new FootballProjectContext())
            {
                return db.Players.ToList();
            }    
        }

        public void SetSelectedTeams(object selectedItem)
        {
            SelectedTeams = (Teams)selectedItem;
        }

        public void SetSelectedPlayers(object selectedItem)
        {
            SelectedPlayers = (Players)selectedItem;
        }

        static void Main(string[] args)
        {
            using (var db = new FootballProjectContext())
            {
                                
            }

        }

        public void CreateTeam(string name, string description, int leagueTrophiesWon, int europeanTrophiesWon)
        {
            using (var db = new FootballProjectContext())
            {

                var newTeam = new Teams()
                {
                    TeamName = name.Trim(),
                    TeamDescription = description.Trim(),
                    LeagueTrophies = leagueTrophiesWon,
                    EuropeanTrophies = europeanTrophiesWon                   

                };

                db.Teams.Add(newTeam);
                db.SaveChanges();
            }
        }

        public List<Players> RetrieveTeamPlayers()
        {
            using (var db = new FootballProjectContext())
            {
                var TeamPlayers = new List<Players>();
                var AllPlayers = db.Players.ToList();
                foreach (var player in AllPlayers)
                {
                    if (player.TeamId == SelectedTeams.TeamId)
                    {
                        TeamPlayers.Add(player);
                    }
                }

                return TeamPlayers;
            }
        }


        //Create Player
        public void CreatePlayer(string name, int age, string nationality, int team, int height, string strongestFoot, string position)
        {
            using (var db = new FootballProjectContext())
            {

                var newPlayer = new Players()
                {
                    Name = name,
                    Age = age,
                    Nationality = nationality,
                    TeamId = team,
                    HeightInches = height,
                    StrongestFoot = strongestFoot,
                    Position = position,

                };

                db.Players.Add(newPlayer);
                db.SaveChanges();
            }
        }




        //Update team
        public void Update(int teamId, string teamName, string teamDescription, int leagueTrophies, int europeanTrophies/*, string players*/)
        {
            using (var db = new FootballProjectContext())
            {
                SelectedTeams = db.Teams.Where(t => t.TeamId == teamId).FirstOrDefault();
                SelectedTeams.TeamName = teamName;
                SelectedTeams.TeamDescription = teamDescription;
                SelectedTeams.LeagueTrophies = leagueTrophies;
                SelectedTeams.EuropeanTrophies = europeanTrophies;
                               
                db.SaveChanges();
            }
        }

        public void UpdatePlayer(int playerId, string name, int age, string nationality, int height, string strongestFoot, string position)
        {
            using (var db = new FootballProjectContext())
            {
                SelectedPlayers = db.Players.Where(p => p.PlayerId == playerId).FirstOrDefault();
                SelectedPlayers.Name = name;
                SelectedPlayers.Age = age;
                SelectedPlayers.Nationality = nationality;
                SelectedPlayers.HeightInches = height;
                SelectedPlayers.StrongestFoot = strongestFoot;
                SelectedPlayers.Position = position;
                /* SelectedPlayers.Name = players;*//*db.Players.Include(t => db.Teams).Where(p => p.TeamId == players && t.TeamID == players);*/
                //from p in db.Players
                //join t in db.Teams on p.TeamId equals t.TeamId
                //where p.TeamId == t.TeamId
                //select { players };

                // write changes to database
                db.SaveChanges();
            }
        }

        //Update team's league trophies
        public void UpdateTeamLeagueTrophies(string teamName, int leagueTrophies)
        {
            using (var db = new FootballProjectContext())
            {
                // obtain selected player
                var selectedTeam =
                        from t in db.Teams
                        where t.TeamName == teamName
                        select t;
                // now update the Team
                foreach (var item in selectedTeam)
                {
                    item.LeagueTrophies = leagueTrophies;
                }
                // save back to database
                db.SaveChanges();

            }
        }


        //Update Player's team
        public void UpdatePlayerTeam(string playerName, int team)
        {
            using (var db = new FootballProjectContext())
            {
                // obtain selected player
                var selectedPlayer =
                        from p in db.Players
                        where p.Name == playerName
                        select p;
                // now update the Team
                foreach (var item in selectedPlayer)
                {
                    item.TeamId = team;
                }
                // save back to database
                db.SaveChanges();

            }

        }

        //Create player stats
        public void AddPlayerStats(int goalsScored, int goalsAssisted, int leagueTrophiesWon, int europeanTrophiesWon)
        {
            using (var db = new FootballProjectContext())
            {

                var newPlayerStats = new PlayerStats()
                {
                    GoalsScored = goalsScored,
                    GoalsAssisted = goalsAssisted,
                    LeagueTrophiesWon = leagueTrophiesWon,
                    EuropeanTrophiesWon = europeanTrophiesWon
                };

                db.PlayerStats.Add(newPlayerStats);
                db.SaveChanges();
            }
        }

        public void DeleteTeam(int teamid)
        {

            using (var db = new FootballProjectContext())
            {
                var selectedTeam =
            from t in db.Teams
            where t.TeamId == teamid
            select t;
                foreach (var item in selectedTeam)
                {
                    db.Teams.Remove(item);
                }

                db.SaveChanges();
            }
        }



        public void DeletePlayer(int playerid)
        {

            using (var db = new FootballProjectContext())
            {
                var selectedPlayer =
            from p in db.Players
            where p.PlayerId == playerid
            select p;
                foreach (var item in selectedPlayer)
                {
                    db.Players.Remove(item);
                }

                db.SaveChanges();
            }
        }




    }

}


