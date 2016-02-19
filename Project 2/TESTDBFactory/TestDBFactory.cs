// TESTDBFactory.cs - Test stub for DBFatcory                        //
// Ver 1.0                                                           //
// Application: Demonstration for CSE687-SMA, Project#2              //
// Language:    C#, ver 6.0, Visual Studio 2015                      //
// Platform:    Dell Inspiron, Core-i5, Windows 10                   //
// Author:      PavanKumar Bodineni, CST 4-187, Syracuse University  //
//              (315) 751-4931, pbodinen@syr.edu                     //
///////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package is just a test stub for DBFactory operations.
 *
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs,DBEdit.cs,Persist_Engine.cs,QueryEngine.cs,
 *                 Display.cs,Scheduler.cs,UtilityExtensions.cs
 *                 
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 10 Oct 15
 * - first release, supports testing DBFactory functions.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
    class TestDBFactory
    {
        //---------------------------< Test stub to test DBFactory functionalities >-----------------------------
        static void Main(string[] args)
        {
            DBEngine<int, DBElement<int, string>> db1 = new DBEngine<int, DBElement<int, string>>();
            DBEngine<string, DBElement<string, List<string>>> db2 = new DBEngine<string, DBElement<string, List<string>>>();
            DBElement<int, string> elem = new DBElement<int, string>();
            elem.name = "element";
            elem.descr = "test element";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<int> { 1, 3 });
            elem.payload = "elem's payload pk";
            db1.insert(elem.Generate_int_key(), elem);

            DBElement<int, string> elem1_ = new DBElement<int, string>();
            elem1_.name = "element2";
            elem1_.descr = "test element2";
            elem1_.timeStamp = DateTime.Now;
            elem1_.children.AddRange(new List<int> { 1, 3 });
            elem1_.payload = "elem's payload pkdsdsd";
            db1.insert(elem.Generate_int_key(), elem1_);

            "\nDatabase Contents of primitive type ".write_console();
            db1.showWithTestType1();

            QueryEngine<int, string> q_engine_primitive = new QueryEngine<int, string>(db1);
            IEnumerable<int> keys = db1.Keys();
            int key = keys.First();
            int child_key = keys.ElementAt(1);
            string write = "\nQuerying for value for key " + child_key + " in primitive type database";
            write.write_console();
            DBElement<int, string> value;
            q_engine_primitive.getvaluebykey(child_key, out value);
            value.showElement();
            string write1 = "\nQuerying for children for key " + child_key + " from primitive type database";
            write1.write_console();
            List<int> l;
            q_engine_primitive.getchildrenbykey(child_key, out l);
            l.showchildren();

            string pattern_ = "test";
            string write12_ = "\nQuerying in metadata for " + pattern_;
            write12_.write_console();
            List<int> l12;
            bool result12_ = q_engine_primitive.dometadatasearch(pattern_, out l12);
            if (result12_) // query succeeded for at least one key
            {
                foreach (int l_ in l12)
                    Write("\n  found \"{0}\" in element with key \"{1}\"", pattern_, key.ToString());
            }
            else
            {
                Write("\n  No keys found with queryParam \"{0}\"", pattern_);
            }
            DateTime startDate = new DateTime(2014, DateTime.Today.Month, DateTime.Today.Day, 00, 00, 01);
            DateTime EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            string write13 = "\nQuerying for keys between dates " + startDate.ToString() + " and " + EndDate.ToString();
            List<int> l13;
            write13.write_console();
            bool result = q_engine_primitive.dodatetimesearch(startDate, EndDate, out l13);
            if (result) // query succeeded for at least one key
            {
                foreach (int l12_ in l13)
                    Write("\n  found in element with key \"{0}\"", l12_.ToString());
            }
            else
            {
                Write("\n\nNo keys fall in between these timestamps");
            }
        }
    }
}
