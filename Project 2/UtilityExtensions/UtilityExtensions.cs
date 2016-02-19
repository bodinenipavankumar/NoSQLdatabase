/////////////////////////////////////////////////////////////////////
// UtilityExtensions.cs - Demonstrate requirements                   //
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
 * This package implements utility extensions that are not specific
 * to a single package.
 */
/*
* Public Interface:
* -----------------
*  public static void title(this string aString, char underline = '-')
*  -- prints headings with underline 
*
* public static void write_console(this string aString, char underline = '-')
* -- Extension for string to print data to console.
*/
/*
 * Maintenance:
 * ------------
 * Required Files: UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 13 Sep 15
 * - first release
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
  public static class UtilityExtensions
  {
        //------------------< Extension to print headings in console >----------------
    public static void title(this string aString, char underline = '-')
    {
      Console.Write("\n  {0}", aString);
      Console.Write("\n {0}", new string(underline, aString.Length + 2));
    }

        //------------------------< Extension to print string data in console >--------------
        public static void write_console(this string aString, char underline = '-')
    {
       Console.Write("\n\n  {0}", aString);
    }
   }
  public class TestUtilityExtensions
  {
    static void Main(string[] args)
    {
      "Testing UtilityExtensions.title".title();
      Write("\n\n");
    }
  }
}
