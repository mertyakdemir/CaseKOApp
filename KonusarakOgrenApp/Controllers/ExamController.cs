using HtmlAgilityPack;
using KonusarakOgrenApp.Data;
using KonusarakOgrenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgrenApp.Controllers
{
    public class ExamController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ExamController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Authorize(Roles="Admin")]
        public IActionResult CreateExam()
        {
            string HTML;
            using (var wc = new WebClient())
            {
                HTML = wc.DownloadString("https://www.wired.com/");
            }
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(HTML);

            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants(0).Where(n => n.HasClass("SummaryItemHedBase-dZmlME")).Take(5);
            var builder = new StringBuilder();

            foreach (var item in nodes)
            {
                builder.AppendLine(item.InnerHtml.ToString());
                builder.AppendLine("---");
            }
            ViewBag.Result = builder.ToString();
            QuestionViewModel model = new QuestionViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateExam(QuestionViewModel viewModel, string title)
        {
            
            Guid guid = Guid.NewGuid();
            foreach (var item in viewModel.Questions)
            {
                Question question = new Question()
                {
                    QuestionUnique = guid.ToString(),
                    QuestionText = item.QuestionText,
                    Title = title,
                    Option = item.Option,
                    Option2 = item.Option2,
                    Option3 = item.Option3,
                    Option4 = item.Option4,
                    CorrectId = item.CorrectId,
                };
               
                if(item.CorrectId == "1") { question.CorrectOption = item.Option; }
                else if (item.CorrectId == "2") { question.CorrectOption = item.Option2; }
                else if (item.CorrectId == "3") { question.CorrectOption = item.Option3; }
                else if (item.CorrectId == "4") { question.CorrectOption = item.Option4; }
                _appDbContext.Questions.Add(question);
                _appDbContext.SaveChanges();
            }

            QuestionViewModel questionViewModel = new QuestionViewModel()
            {
                QuestionId = guid.ToString(),
                Title = title,
                Date = DateTime.Now
            };
            _appDbContext.QuestionViewModels.Add(questionViewModel);
            _appDbContext.SaveChanges();

            return RedirectToAction("ExamList", "Exam");
        }

        public IActionResult SolveExam(string id)
        {
            var question = _appDbContext.Questions.Where(x => x.QuestionUnique == id).ToList();
            ViewBag.QuizTitle = question.Select(x => x.Title).FirstOrDefault();
            return View(question);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ExamList()
        {
            return View(_appDbContext.QuestionViewModels.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteExam(string id)
        {
            _appDbContext.QuestionViewModels.RemoveRange(_appDbContext.QuestionViewModels.Where(x => x.QuestionId == id));
            _appDbContext.SaveChanges();
            _appDbContext.Questions.RemoveRange(_appDbContext.Questions.Where(x => x.QuestionUnique == id));
            _appDbContext.SaveChanges();

            return RedirectToAction("ExamList","Exam");
        }

        public IActionResult AllExam()
        {
            return View(_appDbContext.QuestionViewModels.ToList());
        }
    }
}
