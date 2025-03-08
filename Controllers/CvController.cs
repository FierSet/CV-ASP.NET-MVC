using Microsoft.AspNetCore.Mvc;
using CV_MVC.Models;
using CV_MVC.conndb;
using System.Text.Json;
using System.Runtime.ConstrainedExecution;

namespace CV_MVC.Controllers;

public class CvController : Controller
{
    Transfer _transfer = new Transfer();
    
    
    public IActionResult CV()
    {
        var data = new DataModel();

        var contact = _transfer.Loaddata("SELECT * FROM cv_contact;");
        data.Contacts = new List<Contact>
        {
            new Contact 
            {
                Img = contact[0]["img"].ToString(),
                Name = contact[0]["name"].ToString(),
                Address = contact[0]["address"].ToString(),
                Phone = contact[0]["phone"].ToString(),
                Email = contact[0]["email"].ToString()
            }
        };

        var link_social = _transfer.Loaddata("SELECT * FROM cv_social_links;");
        data.Links_social = link_social.Select(dict => new Link_social{

            Name = dict["name"].ToString(),
            Link = dict["link"].ToString()

        }).ToList();

        var Education = _transfer.Loaddata("SELECT * FROM cv_education;");
        data.Educations = Education.Select(dict => new Education{

            Degree = dict["degree"].ToString(),
            School = dict["school"].ToString()

        }).ToList();

        var Language = _transfer.Loaddata("SELECT * FROM cv_language;");
        data.Languages = Language.Select(dict => new Language{

            Name = dict["name"].ToString(),
            Level = dict["level"].ToString()

        }).ToList();

        var skill = _transfer.Loaddata("SELECT * FROM cv_skills;");
        data.Skills = skill.Select(dict => new Skill{

            Name = dict["skill_name"].ToString()

        }).ToList();

        var xp = _transfer.Loaddata("SELECT * FROM cv_experience;");

        data.XPS = xp.Select(dict => new Xp{

            Company = dict["company"].ToString(),
            Years = dict["years"].ToString(),
            Position = dict["position"].ToString(),
            Work = dict["work"].ToString().Replace("\n", "<br>").Replace("\t", "&emsp;")

        }).ToList();

        var knowledge = _transfer.Loaddata("SELECT * FROM cv_knowledge;");
        data.Knowledges = knowledge.Select(dict => new Knowledge{

            Zone = dict["zone"].ToString(),
            Tech = dict["Technologies"].ToString()

        }).ToList();

        var certificates = _transfer.Loaddata("SELECT * FROM cv_certificate;");
        data.Certificates = certificates.Select(dict => new Cert{

            Company = dict["company_cer"].ToString(),
            Name = dict["name_cer"].ToString()

        }).ToList();
        
        /*
        string jsonResult = JsonSerializer.Serialize(certificates, new JsonSerializerOptions { WriteIndented = true }); //convert to Json in string data
        Console.WriteLine(jsonResult);  //print the jsomn
        foreach(var dict in data.Certificates)
        {
            Console.WriteLine($"ID: {dict.Company}, Name: {dict.Name}");
        }
        */

        return View(data);
    }
}