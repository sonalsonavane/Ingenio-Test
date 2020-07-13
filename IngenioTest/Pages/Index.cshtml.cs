using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IngenioTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private int cateGoryNum;

        private int levelNum;

        public string answer1;

        public string answer2;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //if form code is not finished use this to invoke functions and add values here

            ////problem 1
            //string answer1 = problem1.SolveProblem1(201);

            ////problem 2
            //List<int> answer2 = problem1.SolveProblem2(2);


        }

        public void OnPost()
        {

            //problem 1
            cateGoryNum = Convert.ToInt32( Request.Form[nameof(cateGoryNum)]);
            if(cateGoryNum > 0)
            {
                string pAnswer1 = problem1.SolveProblem1(cateGoryNum);
                answer1 = pAnswer1; //this assigment is only to keep results seperate.


            }
           

            //problem 2
            levelNum = Convert.ToInt32( Request.Form[nameof(levelNum)]);
            if(levelNum > 0)
            {
                List<int> listAnswer2 = problem1.SolveProblem2(levelNum);

                foreach (int item in listAnswer2)
                {
                    answer2 = answer2 + "," + item;
                }
                answer2 = answer2.TrimStart(',');



            }
            
        }
    }
}
