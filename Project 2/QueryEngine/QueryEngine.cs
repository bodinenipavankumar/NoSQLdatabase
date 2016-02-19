// Query_Engine.cs - supports Query operations                       //
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
 * This package supports queries on database for both primitive and Collection type of generic type.
 *
 */
/*
* Public Interface:
* -----------------
* public QueryEngine(DBEngine<Key, DBElement<Key, Data>> DBEngine)
* --Constructor, initializes properties. 
*
* public void getvaluebykey(Key key_,out DBElement<Key, Data> value_)
* --Returns Value when key is passed as input
* 
* public void getchildrenbykey(Key key_, out List<Key> children_)
* --Returns list of children for a key.
*
* public bool doKeyPatternSearch(string pattern,out List<Key> l)
* --Returns list of keys that starts with given pattern
*
* public bool dometadatasearch(string text, out List<Key> l)
* --Returns list of keys that contains given text in metadata(name or description)
*
* public bool dodatetimesearch(DateTime startdate,DateTime enddate, out List<Key> l)
* -- Returns list of keys that fall between given timestamps
*
* public bool dodatetimesearch(DateTime startdate, out List<Key> l)
* -- Returns list of keys that fall between given timestamps when end date is not given
*
*/
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs,QueryEngine.cs,Display.cs,UtilityExtensions.cs
 *                 
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 10 Oct 15
 * - first release, added functions to support queries on database.
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
   public class QueryEngine<Key, Data>
    {
        DBEngine<Key, DBElement<Key,Data>> db;

        //-------------------< Constructor >------------------------
        public QueryEngine(DBEngine<Key, DBElement<Key, Data>> DBEngine)
        {
            db = DBEngine;
        }

        //-------------------< Function returns Value for a given input key >----------------------
        public void getvaluebykey(Key key_,out DBElement<Key, Data> value_)
        {
            DBElement<Key, Data> new_value;
           bool status=db.getValue(key_, out new_value);
           value_ = new_value;
        }

        //-----------------< Function returns list of children for a given input key >--------------
        public void getchildrenbykey(Key key_, out List<Key> children_)
         {
            DBElement<Key, Data> new_value=null;
            bool status = db.getValue(key_, out new_value);
            children_ = new_value.children;
        }


        //------------------------< Query Predicate function to search on Key pattern >----------------
        private Func<Key, bool> defineQuery_searchkeypattern(string pattern)
        {
            Func<Key, bool> queryPredicate = (Key key) =>
            {
                if (key==null)
                    return false;
                if (key.ToString().StartsWith(pattern))  // string test will be captured by lambda
                    return true;
                return false;
            };
            return queryPredicate;
        }

        //------------------< Query Predicate function to search on Value metadata(Name,Description) >-------------
        private Func<Key, bool> defineQuery_searchmetadata(string text)
        {
            Func<Key, bool> queryPredicate = (Key key) =>
            {
                if (key == null)
                    return false;
                DBElement<Key, Data> val;
                db.getValue(key,out val);
                DBElement<Key, Data> elem = val as DBElement<Key, Data>;
                if (elem.name.Contains(text) || elem.descr.Contains(text))
                         return true;
                
                return false;
            };
            return queryPredicate;
        }

        //------------------< Query Predicate function to get results between given datetimes >-------------
         private Func<Key, bool> defineQuery_searchdatetime(DateTime startdate,DateTime enddate)
        {
            Func<Key, bool> queryPredicate = (Key key) =>
            {
                if (key == null)
                    return false;
                DBElement<Key, Data> val;
                db.getValue(key, out val);
                DBElement<Key, Data> elem = val as DBElement<Key, Data>;
                string pk=elem.timeStamp.ToString();
                if (IsBetween(elem.timeStamp,startdate,enddate))
                    return true;
                return false;
            };
            return queryPredicate;
        }



        //-------------< process query using queryPredicate >-------------------------------------------------

        private bool processQuery(Func<Key, bool> queryPredicate, DBFactory<Key, Data> df)
        {
            bool count = false;
            foreach (var key in db.Keys())
            {
                if (queryPredicate(key))
                {
                    count = true;
                    df.addKey(key);
                }
            }
            if (count)
                return true;
            return false;
        }
        //--------------------------< show query results >---------------------------------------

        private void showQueryResults(List<Key> keyCollection,bool result, string queryParam)
        {
            if (result) // query succeeded for at least one key
            {
                foreach (Key key in keyCollection)
                    Write("\n  found \"{0}\" in \"{1}\"", queryParam, key.ToString());
            }
            else
            {
                Write("\n  query failed with queryParam \"{0}\"", queryParam);
            }
        }

        //--------------------< Returns true if given time falls between start time and end time >----------
        private bool IsBetween(DateTime time, DateTime startTime, DateTime endTime)
        {
            if (time.TimeOfDay == startTime.TimeOfDay) return true;
            if (time.TimeOfDay == endTime.TimeOfDay) return true;

            if (startTime.TimeOfDay <= endTime.TimeOfDay)
                return (time.TimeOfDay >= startTime.TimeOfDay && time.TimeOfDay <= endTime.TimeOfDay);
            else
                return !(time.TimeOfDay >= endTime.TimeOfDay && time.TimeOfDay <= startTime.TimeOfDay);
        }

        //-------------------< performs key pattern search and sends result to demo >--------------------        
        public bool doKeyPatternSearch(string pattern,out List<Key> l)
        {
            DBFactory<Key,  Data> df = new DBFactory<Key,  Data>(db);
            Func<Key, bool> query = defineQuery_searchkeypattern(pattern);            
            bool result = processQuery(query,df);
            l = df.Keys();
            return result;
        }

        //-------------------< performs metadata search and sends result to demo >--------------------
        public bool dometadatasearch(string text, out List<Key> l)
        {
            DBFactory<Key, Data> df = new DBFactory<Key, Data>(db);
            Func<Key, bool> query = defineQuery_searchmetadata(text);
            bool result = processQuery(query, df);
            l = df.Keys();
            return result;
        }

        //-------------------< performs datetime search and sends result to demo >--------------------
        public bool dodatetimesearch(DateTime startdate,DateTime enddate, out List<Key> l)
        {
            DBFactory<Key, Data> df = new DBFactory<Key, Data>(db);
            Func<Key, bool> query = defineQuery_searchdatetime(startdate,enddate);
            bool result = processQuery(query, df);
            l = df.Keys();
            return result;
        }

        //-------------------< performs datetime search if enddate is not given and sends result to demo >--------------------
        public bool dodatetimesearch(DateTime startdate,out List<Key> l)
        {
            DateTime enddate = DateTime.Now;
            DBFactory<Key, Data> df = new DBFactory<Key, Data>(db);
            Func<Key, bool> query = defineQuery_searchdatetime(startdate, enddate);
            bool result = processQuery(query, df);
            l = df.Keys();
            return result;
        }
    }
 #if (DBQueryEngine)
    class DBEditTest
    {
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
#endif
}
