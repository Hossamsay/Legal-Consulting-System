using LegalConsulting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Type = LegalConsulting.Models.Type;



namespace LegalConsulting.DAL
{
    public class ConsultingInitializer : DropCreateDatabaseIfModelChanges<ConsultingContext>
    {
     
        protected override void Seed(ConsultingContext context)
        {
            
            var lawyers = new List<Lawyer>
            {
                new Lawyer{FirstName="Ahmed",LastName="Sayed",HiringDate=DateTime.Parse("2010-09-01"),PricePerHour=150},
                new Lawyer{FirstName="Kamel",LastName="Ibrahim",HiringDate=DateTime.Parse("2012-11-08"),PricePerHour=120},
                new Lawyer{FirstName="Sara",LastName="Ahmed",HiringDate=DateTime.Parse("2015-01-09"),PricePerHour=50},
                new Lawyer{FirstName="Metwly",LastName="Elkady",HiringDate=DateTime.Parse("2016-06-11"),PricePerHour=250},
                new Lawyer{FirstName="Salma",LastName="Mohamed",HiringDate=DateTime.Parse("2013-05-01"),PricePerHour=167},
                new Lawyer{FirstName="Hossam",LastName="Abdallah",HiringDate=DateTime.Parse("2017-03-11"),PricePerHour=450},
                new Lawyer{FirstName="Tamer",LastName="Elfayek",HiringDate=DateTime.Parse("2019-02-12"),PricePerHour=170}
            };
            lawyers.ForEach(l => context.Lawyers.AddOrUpdate(p => p.LastName, l));
            context.SaveChanges();
            var clients = new List<Client>
            {
                new Client{FirstName="Fares",LastName="Elsayed",JobTitle="Architect"},
                new Client{FirstName="Jasmine",LastName="Kaml",JobTitle="Marketer"},
                new Client{FirstName="Samia",LastName="Ayoub",JobTitle="Teacher"},
                new Client{FirstName="Eslam",LastName="Mahmoud",JobTitle="HR"},
                new Client{FirstName="Ahmed",LastName="Monir",JobTitle="Project Manager"},
                new Client{FirstName="Dina",LastName="Ahmed",JobTitle="Actress"},
            };

            clients.ForEach(c => context.Clients.AddOrUpdate(p => p.LastName, c));
            context.SaveChanges();


            var cases = new List<Case>
            {
                new Case{CaseID=101,ClientID=1,Type=Type.Criminal,Description="Hard Case",CaseName="Mr.Fares case",StartDate=DateTime.Parse("2018-09-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=102,ClientID=2,Type=Type.General,Description="Hard Case with limted resources",CaseName="Limited case",StartDate=DateTime.Parse("2019-02-11"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=103,ClientID=3,Type=Type.Marriage,Description="easy case",CaseName="cairo case",StartDate=DateTime.Parse("2020-02-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=104,ClientID=2,Type=Type.robbery,Description="werd case",CaseName="werd case",StartDate=DateTime.Parse("2016-09-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=105,ClientID=4,Type=Type.General,Description="this case is not in EGYPT",CaseName="Outsource case",StartDate=DateTime.Parse("2015-12-12"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=106,ClientID=1,Type=Type.Criminal,Description="the criminal is not found till now",CaseName="strange case",StartDate=DateTime.Parse("2018-09-09"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=107,ClientID=3,Type=Type.Marriage,Description="the client want to divorce",CaseName="Family case",StartDate=DateTime.Parse("2017-11-05"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=108,ClientID=5,Type=Type.General,Description="Hard Case",CaseName="Alex case",StartDate=DateTime.Parse("2012-07-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=109,ClientID=6,Type=Type.Criminal,Description="this case take much time",CaseName="Long case",StartDate=DateTime.Parse("2019-09-10"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=110,ClientID=5,Type=Type.Marriage,Description="this case take many lawyers",CaseName="Caffe Case",StartDate=DateTime.Parse("2018-10-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=111,ClientID=4,Type=Type.robbery,Description="easy Case",CaseName="GYM case",StartDate=DateTime.Parse("2017-07-09"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=112,ClientID=5,Type=Type.Criminal,Description="Hard Case",CaseName="Government case",StartDate=DateTime.Parse("2019-08-01"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=113,ClientID=3,Type=Type.Marriage,Description="werd Case",CaseName="Club case",StartDate=DateTime.Parse("2016-06-05"),Lawyers= new List<Lawyer>()},
                new Case{CaseID=114,ClientID=1,Type=Type.General,Description="Hard Case",CaseName="Office case",StartDate=DateTime.Parse("2020-03-02"),Lawyers= new List<Lawyer>()}
            };

            cases.ForEach(c => context.Cases.AddOrUpdate(p => p.CaseID, c));
            context.SaveChanges();



            var officeAssignments = new List <OfficeLocation>
{
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Sayed").LawyerID,Location = "Dokki"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Ibrahim").LawyerID,Location = "5th Settelment"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Ahmed").LawyerID,Location = "Nasr City"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Elkady").LawyerID,Location = "Msr Elgdeda"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Mohamed").LawyerID,Location = "New Cairo"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Abdallah").LawyerID,Location = "6th of october"},
                new OfficeLocation{LawyerID = lawyers.Single(i => i.LastName == "Elfayek").LawyerID,Location = "Maadi"},
                    };
            officeAssignments.ForEach(s => context.officeLocations.AddOrUpdate(p =>p.LawyerID, s));
            context.SaveChanges();



            AddOrUpdateLawyerAdmin(context, 101, "Ibrahim");
            AddOrUpdateLawyerAdmin(context, 102, "Elkady");
            AddOrUpdateLawyerAdmin(context, 103, "Ahmed");
            AddOrUpdateLawyerAdmin(context, 104, "Sayed");
            AddOrUpdateLawyerAdmin(context, 105, "Ibrahim");
            AddOrUpdateLawyerAdmin(context, 106, "Sayed");
            AddOrUpdateLawyerAdmin(context, 107, "Ahmed");
            AddOrUpdateLawyerAdmin(context, 108, "Mohamed");
            AddOrUpdateLawyerAdmin(context, 109, "Abdallah");
            AddOrUpdateLawyerAdmin(context, 110, "Elkady");
            AddOrUpdateLawyerAdmin(context, 111, "Elkady");
            AddOrUpdateLawyerAdmin(context, 112, "Abdallah");
            AddOrUpdateLawyerAdmin(context, 113, "Elfayek");
            AddOrUpdateLawyerAdmin(context, 114, "Ibrahim");
            context.SaveChanges();



            var casedetails = new List<CaseDetail>
            {
                new CaseDetail{LawyerID=1,Hours=20,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "Mr.Fares case" ).CaseID},
                new CaseDetail{LawyerID=2,Hours=10,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "Mr.Fares case" ).CaseID},
                new CaseDetail{LawyerID=2,Hours=40,ClientID=clients.Single(c=>c.LastName == "Kaml").ID,CaseID = cases.Single(c => c.CaseName == "Limited case" ).CaseID},
                new CaseDetail{LawyerID=3,Hours=35,ClientID=clients.Single(c=>c.LastName == "Ayoub").ID,CaseID = cases.Single(c => c.CaseName == "cairo case" ).CaseID},
                new CaseDetail{LawyerID=4,Hours=10,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "cairo case" ).CaseID},
                new CaseDetail{LawyerID=5,Hours=70,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "werd case" ).CaseID},
                new CaseDetail{LawyerID=6,Hours=80,ClientID=clients.Single(c=>c.LastName == "Mahmoud").ID,CaseID = cases.Single(c => c.CaseName == "Outsource case" ).CaseID},
                new CaseDetail{LawyerID=6,Hours=120,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "strange case" ).CaseID},
                new CaseDetail{LawyerID=7,Hours=27,ClientID=clients.Single(c=>c.LastName == "Ayoub").ID,CaseID = cases.Single(c => c.CaseName == "Family case" ).CaseID},
                new CaseDetail{LawyerID=2,Hours=34,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "Alex case" ).CaseID},
                new CaseDetail{LawyerID=5,Hours=250,ClientID=clients.Single(c=>c.LastName == "Ahmed").ID,CaseID = cases.Single(c => c.CaseName == "Long case" ).CaseID},
                new CaseDetail{LawyerID=1,Hours=40,ClientID=clients.Single(c=>c.LastName == "Monir").ID,CaseID = cases.Single(c => c.CaseName == "Caffe Case" ).CaseID},
                new CaseDetail{LawyerID=4,Hours=48,ClientID=clients.Single(c=>c.LastName == "Monir").ID,CaseID = cases.Single(c => c.CaseName == "Caffe Case" ).CaseID},
                new CaseDetail{LawyerID=7,Hours=59,ClientID=clients.Single(c=>c.LastName == "Monir").ID,CaseID = cases.Single(c => c.CaseName == "Caffe Case" ).CaseID},
                new CaseDetail{LawyerID=6,Hours=14,ClientID=clients.Single(c=>c.LastName == "Monir").ID,CaseID = cases.Single(c => c.CaseName == "Caffe Case" ).CaseID},
                new CaseDetail{LawyerID=1,Hours=43,ClientID=clients.Single(c=>c.LastName == "Mahmoud").ID,CaseID = cases.Single(c => c.CaseName == "GYM case" ).CaseID},
                new CaseDetail{LawyerID=4,Hours=22,ClientID=clients.Single(c=>c.LastName == "Monir").ID,CaseID = cases.Single(c => c.CaseName == "Government case" ).CaseID},
                new CaseDetail{LawyerID=2,Hours=19,ClientID=clients.Single(c=>c.LastName == "Ayoub").ID,CaseID = cases.Single(c => c.CaseName == "Club case" ).CaseID},
                new CaseDetail{LawyerID=5,Hours=67,ClientID=clients.Single(c=>c.LastName == "Elsayed").ID,CaseID = cases.Single(c => c.CaseName == "Office case" ).CaseID}


            };

            foreach (CaseDetail e in casedetails)
            {
                var enrollmentInDataBase = context.CaseDetails.Where(s => s.Client.ID == e.ClientID && s.Case.CaseID == e.CaseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.CaseDetails.Add(e);
                }
            }
            context.SaveChanges();

        }
        void AddOrUpdateLawyerAdmin(ConsultingContext context, int Case, string AdminName)
        {
            var crs = context.Cases.SingleOrDefault(c => c.CaseID == Case);
            var inst = crs.Lawyers.SingleOrDefault(i => i.LastName == AdminName);
            if (inst == null)
                crs.Lawyers.Add(context.Lawyers.Single(i => i.LastName ==AdminName));
        }
    }
}