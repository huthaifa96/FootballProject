using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FootballProjectBusiness;
using FootballProject;
using NUnit.Framework;

namespace FootballProjectTests
{
    public class Tests
    {


        CRUDManager _crudManager = new CRUDManager();

        [SetUp]

        public void Setup()
        {
            using (var db = new FootballProjectContext())
            {

                var selectedTeam =
                from t in db.Teams
                where t.TeamName == "Test Team"
                select t;


                foreach (var c in selectedTeam)
                {
                    db.Teams.Remove(c);
                }

                db.SaveChanges();
            }
        }

        [TearDown]

        public void TearDown()
        {
            using (var db = new FootballProjectContext())
            {

                var selectedTeams =
                from t in db.Teams
                where t.TeamName == "Test Team"
                select t;


                foreach (var item in selectedTeams)
                {
                    db.Teams.Remove(item);
                }

                db.SaveChanges();
            }
        }


        [Test]
        public void TeamAddedTest()
        {
            using (var db = new FootballProjectContext())
            {

                var numberOfTeamsBefore = db.Teams.ToList().Count();
                _crudManager.CreateTeam("Test Team", "Boing boing Baggies!", 1, 1);
                var numberOfTeamsAfter = db.Teams.ToList().Count();

                Assert.AreEqual(numberOfTeamsBefore + 1, numberOfTeamsAfter);

            }
        }


        [Test]
        public void TeamAddedDetailsCorrectTest()
        {
            using (var db = new FootballProjectContext())
            {

                var numberOfTeamsBefore = db.Teams.ToList().Count();
                _crudManager.CreateTeam("Test Team", "Boing boing Baggies!", 1, 1);
                var numberOfTeamsAfter = db.Teams.ToList().Count();

                Assert.AreEqual(numberOfTeamsBefore + 1, numberOfTeamsAfter);

                var createdTeam =
                    from t in db.Teams
                    where t.TeamName == "Test Team"
                    select t;

                foreach (var item in createdTeam)
                {
                    Assert.AreEqual("Test Team", item.TeamName);
                    Assert.AreEqual("Boing boing Baggies!", item.TeamDescription);
                    Assert.AreEqual(1, item.LeagueTrophies);
                    Assert.AreEqual(1, item.EuropeanTrophies);
                }

            }
        }


        [Test]
        public void UpdateTest()
        {
            using (var db = new FootballProjectContext())
            {

                var newTeam = new Teams()
                {
                    TeamName = "West Brom",
                    TeamDescription = "Boing boing Baggies!",
                    LeagueTrophies = 1,
                    EuropeanTrophies = 1
                };

                db.Teams.Add(newTeam);

                db.SaveChanges();

                _crudManager.UpdateTeamLeagueTrophies("West Brom", 2);

                var updatedTeam =
                            from t in db.Teams
                            where t.TeamName == "West Brom"
                            select t;

                foreach (var item in updatedTeam)
                {
                    item.LeagueTrophies = 2;
                }
                // save back to database
                db.SaveChanges();
            }
        }

       [Test]
        public void RemoveTest()
        {
            using (var db = new FootballProjectContext())
            {

                var newTeam = new Teams()
                {
                    TeamName = "Test Team",
                    TeamDescription = "Boing boing Baggies!",
                    LeagueTrophies = 1,
                    EuropeanTrophies = 1
                };

                db.Teams.Add(newTeam);

                db.SaveChanges();

                var newTeamID = newTeam.TeamId;

                var numberOfTeamsBefore = db.Teams.ToList().Count();

                _crudManager.DeleteTeam(newTeamID);

                var numberOfTeamsAfter = db.Teams.ToList().Count();

                Assert.AreEqual(numberOfTeamsBefore - 1, numberOfTeamsAfter);


            }
        }

    }
}    
