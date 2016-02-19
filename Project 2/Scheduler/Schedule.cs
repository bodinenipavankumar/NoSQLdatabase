// Scheduler.cs - Schedule database contents                         //
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
 * This package supports scheduling of database contents to Persist Engine .
 *
 */
/*
* Public Interface:
* -----------------
* public Scheduler(int time, DBEngine<Key, Value> db1)
* --constructor calls persist engine thrice and exits
*
*/
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs, DBExtensions.cs, Persist_Engine.cs
 *                 
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 10 Oct 15
 * - first release, added function to schedule database contents to persist engine.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Timers;


namespace Project2Starter
{
    public class Scheduler<Key, Value,Data,T>
        where Data : IEnumerable<T>
    {
        public Timer schedular { get; set; } = new Timer();
        DBEngine<Key, Value> db;
        private int persist_count=0;
       

        //----------------------------< constructor calls persist engine thrice and exits >---------------------
        public Scheduler(int time, DBEngine<Key, Value> db1)
        {
            db = db1;
            schedular.Interval = time;
            schedular.AutoReset = true;
            schedular.Elapsed += (object source, ElapsedEventArgs e) =>
            {
               
                persist_count++;
                if (typeof(DBElement<Key, T>).ToString() == typeof(Value).ToString())
                {
                    db1.persist_database<Key,Value, T>();
                    Console.Write("\n  an event occurred at {0} to persist database contents of primitive type to file {1}", e.SignalTime,db.filename);
                }
                else {
                    Console.Write("\n  an event occurred at {0} to persist database contents of collection type to file {1}", e.SignalTime, db.filename);
                    db1.persist_database<Key, Value,Data, T>();
                }

                if (persist_count == 3) {
                    schedular.Enabled = false;
                    
                }

            };
        }
    }

    //-------------------------------< Test stub to test scheduler functionalities >----------------------
#if (TEST_SCHEDULER)
    public class Test_scheduler

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
            Scheduler<int, DBElement<int, string>, List<string>, string> s1 = new Scheduler<int, DBElement<int, string>, List<string>, string>(3000, db1);
            s1.schedular.Enabled = true;
            Console.ReadKey();

        }
#endif
    }
}
