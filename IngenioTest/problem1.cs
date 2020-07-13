using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IngenioTest
{
    public class problem1
    {
        public problem1()
        {
        }

        #region Problem 1
        public static string SolveProblem1(int categoryID)
        {
            string resultOne = string.Empty;

            if(categoryID <= 0)
            {
                return resultOne; // return empty array
            }

            //Get data from dataset function
             DataSet ds = getDataset();

            List<professions> items = new List<professions>();
            List<professions> filtredList = new List<professions>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                professions pTempObj = new professions();

                pTempObj.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                pTempObj.ParentCategoryId = Convert.ToInt32(dr["ParentCategoryId"]);
                pTempObj.Name = dr["Name"].ToString();
                pTempObj.Keywords = dr["Keywords"].ToString();

                items.Add(pTempObj);

            }

            //check if input category exists corner case
            int index = items.FindIndex(i => i.CategoryId == categoryID);

            if (index <= 0) return "This category does not exists";

            //find parent if for filtering
            Int32 parentId = items.Find(x => x.CategoryId == categoryID).ParentCategoryId;


            //create a dataset that contains rows for given caretegoryid+ parentid to reduce no of loops
            try
            {
                filtredList = items.Where(x => x.CategoryId == categoryID || x.CategoryId == parentId).ToList();

            }

            catch(Exception ex)
            {
                string errMsg = ex.Message;
            }


            //assign values to result string based on conditions.
                foreach (professions p in filtredList)
                {
                if (!String.IsNullOrWhiteSpace(p.Keywords) && (p.CategoryId == categoryID))
                {
                    resultOne = "ParentCategoryID=" + p.ParentCategoryId + ", Name=" + p.Name + ", Keywords=" + p.Keywords;
                }
                else
                {
                    
                    resultOne = "ParentCategoryID=" + p.ParentCategoryId + ", Name=" + p.Name + ", Keywords=" + items.Find(x => x.CategoryId == parentId).Keywords;
                }

            }

            return resultOne;
        }
        #endregion


        #region Problem 2
        public static List<int> SolveProblem2(int inputLevel)
        {
            List<int> levelList = new List<int>();

            if (inputLevel <= 0) return levelList; //return emptyList

            //Get data from dataset function
            DataSet ds = getDataset();

            List<hierarchy> hItems = new List<hierarchy>();

            //map dataset to hierachy class
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                hierarchy tempObj = new hierarchy();

                tempObj.id = Convert.ToInt32(dr["CategoryId"]);
                tempObj.parentID = Convert.ToInt32(dr["ParentCategoryId"]);

                hItems.Add(tempObj);

            }

            //asiggn level to each category
            foreach (hierarchy item in hItems)
            {
                if (item.parentID == -1)
                {
                    item.level = 1;
                }
                else
                {
                    Int32 parent = item.parentID;

                    Int32 parentLevel = hItems.Find(p => p.id == parent).level;

                    item.level = parentLevel + 1;

                }
            }

            
            //filter list for result
            levelList = hItems.Where(l => l.level == inputLevel).Select(l => l.id).ToList();

            return levelList;

        }
        #endregion

        #region Data Function
        public static DataSet getDataset()
        {
            DataSet ds = new DataSet();

            DataTable table1 = new DataTable("professions");
            //Add Columns
            table1.Columns.Add("CategoryId");
            table1.Columns.Add("ParentCategoryId");
            table1.Columns.Add("Name");
            table1.Columns.Add("Keywords");

            ////Add Data rows
            //table1.Rows.Add("100", "-1", "Business", "Money");
            //table1.Rows.Add("200", "-1", "Tutoring", "Teaching");
            //table1.Rows.Add("101", "100", "Accounting", "Taxes");
            //table1.Rows.Add("102", "100", "Taxation", null);
            //table1.Rows.Add("201", "200", "Computer", null);
            //table1.Rows.Add("103", "101", "Corporate Tax", null);
            //table1.Rows.Add("202", "201", "Operating System", null);
            //table1.Rows.Add("109", "101", "Small Business Tax", null);


            //Add Data rows
            table1.Rows.Add(100, -1, "Business", "Money");
            table1.Rows.Add(200, -1, "Tutoring", "Teaching");
            table1.Rows.Add(101, 100, "Accounting", "Taxes");
            table1.Rows.Add(102, 100, "Taxation", null);
            table1.Rows.Add(201, 200, "Computer", null);
            table1.Rows.Add(103, 101, "Corporate Tax", null);
            table1.Rows.Add(202, 201, "Operating System", null);
            table1.Rows.Add(109, 101, "Small Business Tax", null);

            ds.Tables.Add(table1);

            return ds;

        }
        #endregion

    }
}
