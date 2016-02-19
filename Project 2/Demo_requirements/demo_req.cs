// demo_req.cs - Demonstrate requirements                            //
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
 * This package demonstrates the requirements of project#2.
 */
/*
* Public Interface:
* -----------------
* public void demo_req2(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates requirement for implementing a generic key/value in-memory database.
*
* public void demo_req3(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates requirement for implementing addition and deletion of key/value pairs.
* 
* public void demo_req4(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates requirement for editing values.
*
* public void demo_req5(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates requirement to persist database contents to XML file, 
*   augmenting and restoring data from XML file to database.
*
* public void demo_req6(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates scheduler operations to persist database contents to XML file,
*   on number of writes and for time interval.
*
* public void demo_req7(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
* --Demonstrates Queries performed on database
*
* public void demo_req9()
* --Loads package structure from XML file to database and displays database contents.
*
* public void demo_req12()
* --Loads category data from XML file to database and displays it.
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
 * - first release, added functions to demonstrate requirements.
 *
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
    public class demo_requirements
    {
        //-------------------< Demonstrates requirement for implementing a generic key/value in-memory database. >------------------------
        public void demo_req2(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
        {
            "\nDemonstrating Requirement #2".title();
            "\n**Database Contents for primitive type".write_console();
            DBElement<int, string> elem = new DBElement<int, string>();
            elem.name = "The Dark Knight ";
            elem.descr = "Movie is based on super hero Batman";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<int> { 1,3 });
            elem.payload = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham";
            db1.AddKeyvaluepair(elem.Generate_int_key(), elem);

            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "Pulp Fiction";
            elem1.descr = "four tales of violence and redemption";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new List<int> { 1,3 });
            elem1.payload = "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits";
            db1.AddKeyvaluepair(elem.Generate_int_key(), elem1);
            WriteLine();
            db1.showWithTestType1();
            "\n**Database Contents for collection type database".write_console();
            DBElement<string, List<string>> newerelem1 = new DBElement<string, List<string>>();
            newerelem1.name = "Forrest Gump";
            newerelem1.descr = "Forrest Gump, while not intelligent, has accidentally been present at many historic moments";
            newerelem1.timeStamp = DateTime.Now;
            newerelem1.children.AddRange(new[] { "first", "second" });
            newerelem1.payload = new List<string> { "Tom Hanks", "Sally Field", "Sam Anderson" };
            newerelem1.payload.Add("Rebecca Williams");
            newerelem1.payload.Add("John Randall");
            db2.insert(newerelem1.Generate_string_key(), newerelem1);
            WriteLine();
            DBElement<string, List<string>> newerele = new DBElement<string, List<string>>();
            newerele.name = "Inception";
            newerele.descr = "A thief who steals corporate secrets through use of dream-sharing technology";
            newerele.timeStamp = DateTime.Now;
            newerele.children.AddRange(new[] { "first", "second" });
            newerele.payload = new List<string> { "Leonardo DiCaprio", "Joseph Gordon", "Ellen Page" };
            db2.AddKeyvaluepair<string, List<string>, string>(newerele.Generate_string_key(), newerele);
            db2.showEnumerableDB();
            WriteLine();
        }

        //-------------------< Demonstrates requirement for implementing addition and deletion of key/value pairs. >------------------------
        public void demo_req3(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
      {
            "\n\nDemonstrating Requirement #3".title();
            "\n**Database Contents before adding element for primitive type".write_console();
            db1.showWithTestType1();
            DBElement<int, string> elem = new DBElement<int, string>();
            elem.name = "Goodellas";
            elem.descr = "Gangster Triology";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<int> { 1, 2, 3 });
            elem.payload = "Henry Hill and his friends work their way up through the mob hierarchy";
            int key = elem.Generate_int_key();
            db1.AddKeyvaluepair<int, string>(key, elem);
            string add = "\n**Database Contents after adding element with key " + key + " for primitive type";
            add.write_console();
            db1.showWithTestType1();
            string write1 = "\n**Database Contents after deleting element with key " + key + " for primitive type ";
            write1.write_console();
            db1.deleteKeyvaluepair<int, string>(key);
            db1.showWithTestType1();

            "\n**Database Contents before adding element for collection type".write_console();
            db2.showEnumerableDB();

            DBElement<string, List<string>> newerelem2 = new DBElement<string, List<string>>();
            newerelem2.name = "The Matrix";
            newerelem2.descr = "A computer hacker learns from mysterious rebels about the true nature of his reality";
            newerelem2.timeStamp = DateTime.Now;
            newerelem2.payload = new List<string> { "Keanu Reeves", "Laurence Fishburne"};
            newerelem2.payload.Add("Gloria Foster");
            newerelem2.payload.Add("Joe Pantoliano");
            string new_key = newerelem2.Generate_string_key();           
            db2.AddKeyvaluepair< string, List < string >,string> (new_key, newerelem2);
            WriteLine();
            string add1 = "\n**Database Contents after adding element with key "+ new_key+" for list of collection type";
            add1.write_console();
            db2.showEnumerableDB();           
            string write = "\n**Database Contents after deleting element with key "+ new_key + " for collection type ";
            write.write_console();
            db2.deleteKeyvaluepair<string, List<string>, string>(new_key);
            db2.showEnumerableDB();
        }


        //-------------------< Demonstrates requirement for editing values for primitive type DB >------------------------
        private void demo_req4_primitive(DBEngine<int, DBElement<int, string>> db)
        {
            "\n**Database Contents of primitive type ".write_console();
            db.showWithTestType1();

            IEnumerable<int> keys = db.Keys();
            int edit_key = keys.First();
            int child_key = keys.ElementAt(1);
            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "seven";
            elem1.descr = "instance created to test edit instance functionality for primitive type";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new[] {1 });
            elem1.payload = "Two detectives, a rookie and a veteran, hunt a serial killer ";
            WriteLine();
            bool status = db.edit_instance<int, string>(elem1, edit_key);
            string write = "\n**Database Contents of primitive type after replacing with new value instance for key " + edit_key;
            write.write_console();
            db.showWithTestType1();

            bool status1 = db.edit_name<int,  string>("new name for edit", edit_key);
            bool status2 = db.edit_desc<int,  string>("new desc for edit", edit_key);
            string edit = "\n**Database Contents of primitive type after editing name and description for key " + edit_key;
            edit.write_console();
            db.showWithTestType1();

            db.add_relationship<int, string>(child_key, edit_key);
            string relation = "\n**Database Contents of primitive type after adding " + child_key + " as child relationship for key " + edit_key;
            relation.write_console();
            db.showWithTestType1();

            db.remove_relationship<int,  string>(child_key, edit_key);
            string del_relation = "\n**Database Contents of primitive type after removing " + child_key + " as child for key " + edit_key;
            del_relation.write_console();
            db.showWithTestType1();
        }



        //-------------------< Demonstrates requirement for editing values for collection type DB >------------------------
        public void demo_req4(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
        {
            "\n\nDemonstrating Requirement #4".title();
            demo_req4_primitive(db1);
            "\n**Database Contents for list of collection".write_console();
            db2.showEnumerableDB();

            IEnumerable<String> keys = db2.Keys();
            string edit_key = keys.First();
            string child_key = keys.ElementAt(1);
            DBElement<string, List<string>> elem1 = new DBElement<string, List<string>>();
            elem1.name = "Interstellar";
            elem1.descr = "instance created to test edit instance functionality for primitive type";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new[] { "fifth", "seven" });
            elem1.payload = new List<string> { "Ellen Burstyn", "Matthew McConaughey", "Mackenzie Foy" };
            WriteLine();
            bool status=db2.edit_instance<string, List<string>, string>(elem1,edit_key);
            string write = "\n**Database Contents after replacing with new value instance for key " + edit_key;
             write.write_console();
            db2.showEnumerableDB();

            bool status1=db2.edit_name<string, List<string>, string>("new name for edit", edit_key);
            bool status2 = db2.edit_desc<string, List<string>, string>("new desc for edit", edit_key);
            string edit = "\n**Database Contents after editing name and description for key " + edit_key;
            edit.write_console();
            db2.showEnumerableDB();

            db2.add_relationship<string, List<string>, string>(child_key,edit_key);
            string relation = "\n**Database Contents after adding "+child_key + " as child relationship for key " + edit_key;
            relation.write_console();
            db2.showEnumerableDB();

            db2.remove_relationship<string, List<string>, string>(child_key, edit_key);
            string del_relation = "\n**Database Contents after removing " + child_key + " as child for key " + edit_key;
            del_relation.write_console();
            db2.showEnumerableDB();
        }


        //-------< Demonstrates requirement to persist database contents to XML file,augmenting and restoring data from XML file to database. >-------------
        public void demo_req5(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
        {
            "\n\nDemonstrating Requirement #5".title();

            "\n**Persist contents of primitive type database to XML file,printing existing database".write_console();
            string dir="..\\..\\..\\..\\TestFolder\\";
            string filename = "augment_primitive.xml";
            string filename1 = "Persist_Primitivetype.xml";
            DBEngine<int, DBElement<int, string>> db_ = new DBEngine<int, DBElement<int, string>>();
            db1.persist_database<int, DBElement<int, string>, string>();
            db1.showWithTestType1();
            string write = "\n**Data after Augmenting data from XML file " + filename + " to primitive type database";
            write.write_console();
            
            db1.augment_to_database<int, DBElement<int,string>, string>(dir+filename);
            db1.showWithTestType1();
            string write1 = "\n**Data after Unpersisting data from XML file " + filename1 + " to primitive type database";
            write1.write_console();
            db_.filename= "primitive.xml";
            db_.unpersist_to_database<int, DBElement<int, string>, string >(dir+filename1);
            db_.showWithTestType1();




            "\n**Persist contents of collection database to XML file, Contents in existing database".write_console();
            DBEngine<string, DBElement<string, List<string>>> db = new DBEngine<string, DBElement<string, List<string>>>();
            db2.persist_database<string, DBElement<string, List<string>>, List<string>, string>();
            db2.showEnumerableDB();
            string filename2 = "augment_collection.xml";
            string filename3 = "Persist_Collectiontype.xml";

            string write2 = "\n**Data after Augmenting data from XML file "+filename2+" to collection database";
            write2.write_console();
            db2.augment_to_database<string, DBElement<string, List<string>>, List<string>, string>(dir+filename2);
            db2.showEnumerableDB();
            string write3 = "\n**Data after Unpersisting data from XML file "+filename3+" to collection database";
            write3.write_console();
            db.filename = "Test.xml";
            db.unpersist_to_database<string, DBElement<string, List<string>>, List<string>, string>(dir+filename3);
            db.showEnumerableDB();

            "\n**Unpersistinng to database when types in database and XML file doesn't match".write_console();
            db_.unpersist_to_database<int, DBElement<int, string>, string>(dir+filename3);
        }

        //-------< Demonstrates scheduler operations to persist database contents to XML file,on number of writes and for time interval. >-------------
        public void demo_req6(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
        {
            "\n\nDemonstrating Requirement #6".title();
            "\n**For every 5 writes occured on database, Application will take backup of database contents".write_console();
            "\n***Scheduling database".write_console();
            "\n**For demonstration purpose, I have stopped scheduler after persisting 3 times".write_console();
            Scheduler<string, DBElement<string, List<string>>, List<string>,string> s = new Scheduler<string, DBElement<string, List<string>>, List<string>, string>(3000,db2);
            s.schedular.Enabled = true;

            Scheduler<int, DBElement<int, string>, List<string>, string> s1 = new Scheduler<int, DBElement<int, string>, List<string>, string>(3000, db1);
            s1.schedular.Enabled = true;
            Console.ReadKey();
        }

        //-------------------< Demonstrates Queries performed on database for primitive type DB >------------------------
        private void demo_req7_primitive(DBEngine<int, DBElement<int, string>> db1)
        {
            QueryEngine<int,  string> q_engine_primitive = new QueryEngine<int,  string>(db1);
            IEnumerable<int> keys = db1.Keys();
            int key = keys.First();
            int child_key = keys.ElementAt(1);
            string write = "\n**Querying for value for key " + child_key + " in primitive type database";
            write.write_console();
            DBElement<int, string> value;
            q_engine_primitive.getvaluebykey(child_key, out value);
            value.showElement();
            string write1 = "\n**Querying for children for key " + child_key + " from primitive type database";
            write1.write_console();
            List<int> l;
            q_engine_primitive.getchildrenbykey(child_key, out l);
            l.showchildren();
            searchkeypattern_primitive(q_engine_primitive);
            searchmetadata_primitive(q_engine_primitive);
            searchdatetime_primitive(q_engine_primitive);
        }


        //-------------------< Demonstrates Queries performed on database for collection type DB>------------------------
        public void demo_req7(DBEngine<int, DBElement<int, string>> db1, DBEngine<string, DBElement<string, List<string>>> db2)
        {
            
            QueryEngine<string, List<string>> q =new QueryEngine<string, List<string>>(db2);
            "\n\nDemonstrating Requirement #7".title();
            demo_req7_primitive(db1);
            IEnumerable<String> keys = db2.Keys();
            string key = keys.First();
            string child_key = keys.ElementAt(1);
            string write = "\n**Querying for value for key " + key + " in Collection type database";
            write.write_console();
            DBElement<string, List<string>> value;
            q.getvaluebykey(key,out value);
            value.showEnumerableElement();
            string write1 = "\n**Querying for children for key " + key + " from collection type database";
            write1.write_console();
            List<string> l;
            q.getchildrenbykey(key,out l);
            l.showchildren();
            searchkeypattern(q);
            searchmetadata(q);
            searchdatetime(q);
         }

        //-------------------< Loads package structure from XML file to database and displays database contents. >------------------------
        public void demo_req9()
        {
            "\n\nDemonstrating Requirement #9".title();
            string dir = "..\\..\\..\\..\\TestFolder\\";
            string write = "\n**Loading contents of package structure to database and printing the database contents";
            write.write_console();
           
            DBEngine<string, DBElement<string, string>> db = new DBEngine<string, DBElement<string, string>>();
            db.filename = "Package_structure.xml";
            db.unpersist_to_database<string, DBElement<string, string>,string>(dir+"Package_structure.xml");
            db.showWithTestType1();
        }

        //-------------------< Demonstrates requirement 11 >------------------------
        public void demo_req11()
        {
            "\n\nDemonstrating Requirement #11".title();
            string write = "\n**Please refer for 'Requirement 11.docx' file in 'Project 2' folder for this requirement";
            write.write_console();
        }


        //-------------------< Loads category data from XML file to database and displays it. >------------------------
        public void demo_req12()
        {
            "\n\nDemonstrating Requirement #12".title();
            string dir = "..\\..\\..\\..\\TestFolder\\";
            string write = "\n**Loading contents of categories to database and printing the database contents";
            write.write_console();
            DBEngine<string, DBElement<string, List<string>>> db = new DBEngine<string, DBElement<string, List<string>>>();
            QueryEngine<string, List<string>> q = new QueryEngine<string, List<string>>(db);
            db.filename = "Categories.xml";
            db.unpersist_to_database<string, DBElement<string, List<string>>, List<string>, string>(dir+"Categories.xml");
            db.showWithTestType2();
            "\n\n****Queries on Categories Database".write_console();

            "\n**Below are the brands that produce mobile devices".write_console();
            
            string key = "Mobile";
            DBElement<string, List<string>> value;
            q.getvaluebykey(key, out value);
            string payload_ = value.get_payload1<string, List<string>,string>();
            payload_.write_console();

            "\n**Below are the products produced by samsung Company".write_console();
            List<string> l1 = null;
            string key_ = "Samsung";
            q.getchildrenbykey(key_, out l1);
            l1.showchildren();
        }

        //-------------------------------< Demonstrates creation on new immutable database >-------------------
        public void demo_req8()
        {
            "\n\nDemonstrating Requirement #8".title();
            string write = "Application supports creation of new immutable database on top of query results";
            string write1 = "This resultant database looks same as original database and can be useful for performing compound queries";
            string write2 = "This package does not change contents in original database, Check for public interface in DBFactory.cs at line 16 for this";

            write.write_console();
            write1.write_console();
            write2.write_console();           
        }
        //-------------------< Querying for Key pattern on primitive type DB >------------------------
        private void searchkeypattern_primitive(QueryEngine<int, string> q)
        {
            string pattern = "2";
            string write = "\n**Querying for keys starting with" + pattern;
            write.write_console();
            List<int> l;
            bool result = q.doKeyPatternSearch(pattern, out l);
            showQueryResults(l, result, pattern);
        }

        //-------------------< Querying for Key pattern on collection type DB >------------------------
        private void searchkeypattern(QueryEngine<string,  List<string>> q)
        {
            string pattern = "114";
            string write = "\n**Querying for keys starting with" + pattern;
            write.write_console();
            List<string> l;
            bool result = q.doKeyPatternSearch(pattern, out l);
            showQueryResults(l, result, pattern);
        }

        //-------------------< Querying for metadata on primitive type DB >------------------------
        private void searchmetadata_primitive(QueryEngine<int, string> q)
        {
            string pattern = "Augment";
            string write = "\n**Querying in metadata for " + pattern;
            write.write_console();
            List<int> l;
            bool result = q.dometadatasearch(pattern, out l);
            showQueryResults(l, result, pattern);
        }


        //-------------------< Querying for metadata on collection type DB >------------------------
        private void searchmetadata(QueryEngine<string, List<string>> q)
        {
            string pattern = "Augment";
            string write = "\n**Querying in metadata for " + pattern;
            write.write_console();
            List<string> l;
            bool result = q.dometadatasearch(pattern, out l);
            showQueryResults(l, result, pattern);
        }


        //--------------< Querying for Keys that fall between given time interval on primitive type DB >-------
        private void searchdatetime_primitive(QueryEngine<int, string> q)
        {
            DateTime startDate = new DateTime(2014, DateTime.Today.Month, DateTime.Today.Day, 00, 00, 01);
            DateTime EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            string write = "\n**Querying for keys between dates " + startDate.ToString() + " and " + EndDate.ToString();
            List<int> l;
            write.write_console();
            bool result = q.dodatetimesearch(startDate, EndDate, out l);
            showdatetimeresults(l, result);
            List<int> l1;
            string write1 = "\n**Querying when start date is given and End date is not given, End date will be taken as current time";
            write1.write_console();            
            bool result1 = q.dodatetimesearch(startDate, out l1);
            showdatetimeresults(l1, result);
        }


        //-------------------< Querying for Keys that fall between given time interval on collection type DB >------------------------
        private void searchdatetime(QueryEngine<string,  List<string>> q)
        {
            DateTime startDate = new DateTime(2014, DateTime.Today.Month, DateTime.Today.Day, 00, 00, 01);
            DateTime EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            string write = "\n**Querying for keys between dates " + startDate.ToString()+" and "+EndDate.ToString();
            write.write_console();
            List<string> l;
            //bool result = q.dodatetimesearch(startDate, EndDate, out l1);
            bool result = q.dodatetimesearch(startDate,EndDate, out l);
            showdatetimeresults(l, result);
            string write1 = "\n**Querying when start date is given and End date is not given, End date will be taken as current time";
            write1.write_console();
            List<string> l1;            
            bool result1 = q.dodatetimesearch(startDate, out l1);
            showdatetimeresults(l, result);
        }


        //-------------------< show results of queries for Key type, string >------------------------
        private void showQueryResults(List<String> keyCollection, bool result, string queryParam=null)
        {
            if (queryParam == null) queryParam = "";
            if (result) // query succeeded for at least one key
            {
                foreach (string key in keyCollection)
                    Write("\n  found \"{0}\" in element with key \"{1}\" ", queryParam, key.ToString());
            }
            else
            {
                Write("\n  No keys found with queryParam \"{0}\"", queryParam);
            }
        }

        //-------------------< show results of queries for Key type, int >------------------------
        private void showQueryResults(List<int> keyCollection, bool result, string queryParam = null)
        {
            if (queryParam == null) queryParam = "";
            if (result) // query succeeded for at least one key
            {
                foreach (int key in keyCollection)
                    Write("\n  found \"{0}\" in element with key \"{1}\"", queryParam, key.ToString());
            }
            else
            {
                Write("\n  No keys found with queryParam \"{0}\"", queryParam);
            }
        }



        //-------------------< show datetime query results for Key type, string >------------------------
        private void showdatetimeresults(List<String> keyCollection, bool result)
        {
            if (result) // query succeeded for at least one key
            {
                foreach (string key in keyCollection)
                    Write("\n  found in element with key \"{0}\"",  key.ToString());
            }
            else
            {
                Write("\n\nNo keys fall in between these timestamps");
            }
        }

        
        //-------------------< show datetime query results for Key type, int >------------------------
        private void showdatetimeresults(List<int> keyCollection, bool result)
        {
            if (result) // query succeeded for at least one key
            {
                foreach (int key in keyCollection)
                    Write("\n  found in element with key \"{0}\"", key.ToString());
            }
            else  {
                Write("\n\nNo keys fall in between these timestamps");
            }
        }
    }
#if (DEMO_REQUIREMENTS)
    public class TestDemo_requirements
    {
        static void Main(string[] args)
        {            
            DBEngine<int, DBElement<int, string>> db1 = new DBEngine<int, DBElement<int, string>>();
            DBEngine<string, DBElement<string, List<string>>> db2 = new DBEngine<string, DBElement<string, List<string>>>();

            "Demonstrating Project#2 Requirements".title('=');
            WriteLine();

            demo_requirements demo = new demo_requirements();

            demo.demo_req2(db1, db2);
            demo.demo_req3(db1, db2);
            demo.demo_req4(db1, db2);
            demo.demo_req5(db1, db2);
            demo.demo_req7(db1, db2);
            demo.demo_req9();
            demo.demo_req6(db1, db2);

            Write("\n\n");
        }
    }
#endif
    }

