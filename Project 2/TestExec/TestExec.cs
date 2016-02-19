// demo_req.cs - Test Executive                                      //
// Ver 1.0                                                           //
// Application: Demonstration for CSE687-SMA, Project#2              //
// Language:    C#, ver 6.0, Visual Studio 2015                      //
// Platform:    Dell Inspiron, Core-i5, Windows 10                   //
// Author:      PavanKumar Bodineni, CST 4-187, Syracuse University  //
//              (315) 751-4931, pbodinen@syr.edu                     //
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
  class TestExec
  {
     
    static void Main(string[] args)
    {
      TestExec exec = new TestExec();
      DBEngine<int, DBElement<int, string>> db1 = new DBEngine<int, DBElement<int, string>>();
      DBEngine<string, DBElement<string,List< string>>> db2 = new DBEngine<string, DBElement<string,List< string>>>();
       db1.filename = "Persist_Primitivetype.xml";
       db2.filename = "Persist_Collectiontype.xml";
       "Demonstrating Project#2 Requirements".title('=');
      WriteLine();
 
         demo_requirements demo = new demo_requirements();
           
            demo.demo_req2(db1,db2);
            demo.demo_req3(db1, db2);
            demo.demo_req4(db1,db2);
            demo.demo_req5(db1, db2);
            demo.demo_req7(db1, db2);
            demo.demo_req8();
            demo.demo_req9();
            demo.demo_req12();
            demo.demo_req11();
            demo.demo_req6(db1, db2);
            
            Write("\n\n");
    }
  }
}
