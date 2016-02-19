// DBExtensions.cs - persists database contents                      //
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
 * This package implements extensions methods to support 
 * displaying DBElements and DBEngine instances.
 */
/*
* Public Interface:
* -----------------
* public static string showMetaData<Key, Data>(this DBElement<Key, Data> elem)
* -- Displays contents of metadata
*
* public static void showchildren<Key>(this List<Key> childern_)
* -- Displays children for a value object
*
* public static string get_children1<Key, Data>(this DBElement<Key, Data> elem)
* --returns children in string format
*
* public static string showElement<Key, Data>(this DBElement<Key, Data> elem)
* -- write details of element with simple Data to string
*
* public static string Generate_string_key<Data>(this DBElement<string, Data> elem)
* --Generate keys if key in Key/value pairs is string 
*
*  public static void showEnumerable<Key, Value, Data, T>(this DBEngine<Key, Value> db)
* --write enumerable db elements out to Console
*
* public static void get_payload2<Key, Data, T>(this DBElement<Key, Data> elem, out XElement Payload_)
* --Constructs payload element for collection type to load into XML file
*
* public static void get_children2<Key, Data, T>(this DBElement<Key, Data> elem, out XElement children_)
* --Constructs children element for collection type to load into XML file
*
*/
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   DBExtensions.cs, DBEngine.cs, DBElement.cs, UtilityExtensions
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.2 : 10 Oct 15
 * -- Added functions to support project#2 requirements
 * ver 1.1 : 15 Sep 15
 * - added a few comments
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
using System.Xml;
using System.Xml.Linq;

namespace Project2Starter
{
  /////////////////////////////////////////////////////////////////////
  // Extension methods class 
  // - Extension methods are static methods of a static class
  //   that extend an existing class by adding functionality
  //   not part of the original class.
  // - These methods are all extending the DBElement<Key, Data> class.
  //
  public static class DBElementExtensions
  {
        //----< write metadata to string >--------------------------------
        static int key = 0;
        static int seed = 0;
        
     //--------------------< Displays contents of metadata >-----------------------------
     public static string showMetaData<Key, Data>(this DBElement<Key, Data> elem)
    {
      StringBuilder accum = new StringBuilder();
      accum.Append(String.Format("\n  name: {0}", elem.name));
      accum.Append(String.Format("\n  desc: {0}", elem.descr));
      accum.Append(String.Format("\n  timestamp: {0}", elem.timeStamp));
      if (elem.children.Count() > 0)
      {
        accum.Append(String.Format("\n  Children: "));
        bool first = true;
        foreach (Key key in elem.children)
        {
          if (first)
          {
            accum.Append(String.Format("{0}", key.ToString()));
            first = false;
          }
          else
            accum.Append(String.Format(", {0}", key.ToString()));
        }
      }
      return accum.ToString();
    }

        //--------------------------------< Displays children for a value object >-----------------------------
        public static void showchildren<Key>(this List<Key> childern_)
        {
            StringBuilder accum = new StringBuilder();
            if (childern_.Count() > 0)
            {
                bool first = true;
                foreach (Key key in childern_)
                {
                    if (first)
                    {
                        accum.Append(String.Format("{0}", key.ToString()));
                        first = false;
                    }
                    else
                        accum.Append(String.Format(", {0}", key.ToString()));
                }
                accum.ToString().write_console();
            }
            else
                "No children found for the key".write_console();
        }


        //------------------------------< Displays children in string format >---------------------------------
        public static string get_children1<Key, Data>(this DBElement<Key, Data> elem)
        {
            StringBuilder accum = new StringBuilder();
            if (elem.children.Count() > 0)
            {
                bool first = true;
                foreach (Key key in elem.children)
                {
                    if (first)
                    {
                        accum.Append(String.Format("{0}", key.ToString()));
                        first = false;
                    }
                    else
                        accum.Append(String.Format(", {0}", key.ToString()));
                }
            }
            return accum.ToString();
        }


        //----< write details of element with simple Data to string >-----
    public static string showElement<Key, Data>(this DBElement<Key, Data> elem)
    {
      StringBuilder accum = new StringBuilder();
      accum.Append(elem.showMetaData());
      if (elem.payload != null)
        accum.Append(String.Format("\n  payload: {0}", elem.payload.ToString()));
      return accum.ToString();
    }

        

    //---------------------- Generate integer keys if key in Key/value pairs is integer--------------------
    public static int Generate_int_key<Data>(this DBElement<int, Data> elem)
    {
      Func<int> keyGen = () => { ++key; return key; };
      return keyGen();
    }

        //---------------------------- Generate keys if key in Key/value pairs is string  ---------------------------------     
     public static string Generate_string_key<Data>(this DBElement<string, Data> elem)
     {
       string skey = seed.ToString();
       Func<string> skeyGen = () => {
                ++seed;
                skey = "string" + seed.ToString();
                skey = skey.GetHashCode().ToString();
                return skey;
            };
          return skeyGen();
        }

      //----< write details of an Enumerable element to string >--------
     public static string showEnumerableElement<Key, Data, T>(this DBElement<Key, Data> elem)
      where Data : IEnumerable<T>  // constraint clause
    {
      StringBuilder accum = new StringBuilder();
      accum.Append(elem.showMetaData());
      if (elem.payload != null)
      {
        bool first = true;
        accum.Append(String.Format("\n  payload:\n  "));
        foreach (var item in elem.payload)  // won't compile without constraint clause
        {
          if (first)
          {
            accum.Append(String.Format("{0}", item));
            first = false;
          }
          else
            accum.Append(String.Format(", {0}", item));
        }
      }
      return accum.ToString();
    }
  }
  public static class DBEngineExtensions
  {
    //----< write simple db elements out to Console >------------------

    public static void show<Key, Value, Data>(this DBEngine<Key, Value> db)
    {
      foreach (Key key in db.Keys())
      {
        Value value;
        db.getValue(key, out value);
        DBElement<Key, Data> elem = value as DBElement<Key, Data>;
        Write("\n\n  -- key = {0} --", key);
        Write(elem.showElement());
      }
    }
    //----< write enumerable db elements out to Console >--------------
    public static void showEnumerable<Key, Value, Data, T>(this DBEngine<Key, Value> db)
      where Data : IEnumerable<T>
    {
      foreach (Key key in db.Keys())
      {
        Value value;
        db.getValue(key, out value);
        DBElement<Key, Data> elem = value as DBElement<Key, Data>;
        Write("\n\n  -- key = {0} --", key);
        Write(elem.showEnumerableElement<Key, Data, T>());
      }
    }
        //----------------------< DIsplays children in string format >-------------------------------
        public static string get_children1<Key, Data, T>(this DBElement<Key, Data> elem)
           where Data : IEnumerable<T>
        {
            StringBuilder accum = new StringBuilder();
            //accum.Append(elem.showElemMetaData<Key, List<string>>());
            if (elem.children != null)
            {
                bool first = true;
                foreach (var item in elem.children)
                {
                    if (first)
                    {
                        accum.Append(String.Format("{0}", item));
                        first = false;
                    }
                    else
                        accum.Append(String.Format(", {0}", item));
                }
            }
            return accum.ToString();
        }

        //----------------------------< Displays payload in string format >--------------------------
        public static string get_payload1<Key, Data, T>(this DBElement<Key, Data> elem)
           where Data : IEnumerable<T>
        { 
                 StringBuilder accum = new StringBuilder();
            //accum.Append(elem.showElemMetaData<Key, List<string>>());
            if (elem.payload != null)
            {
                bool first = true;
                foreach (var item in elem.payload)
                {
                    if (first)
                    {
                        accum.Append(String.Format("{0}", item));
                        first = false;
                    }
                    else
                        accum.Append(String.Format(", {0}", item));
                }
            }
            return accum.ToString();
        }

        //--------------------< Constructs payload element for collection type to load into XML file>------------------------
        public static void get_payload2<Key, Data, T>(this DBElement<Key, Data> elem, out XElement Payload_)
         where Data : IEnumerable<T>
        {
            XElement new_Payload = new XElement("Payload"); ;
            if (elem.payload != null)
            {
                foreach (var item in elem.payload)
                {
                    XElement child = new XElement("item", item);
                    new_Payload.Add(child);
                }
            }
            Payload_ = new_Payload;
        }


        //--------------------< Constructs payload element for primitive type to load into XML file>------------------------
        public static void get_payload2<Key, Data>(this DBElement<Key, Data> elem,out XElement Payload_)
         {
            XElement new_Payload=null;
            if (elem.payload != null)
            {
                 new_Payload= new XElement("Payload",elem.payload.ToString());
                                                 
            }
            Payload_ = new_Payload;
           }


        //--------------------< Constructs children element for collection type to load into XML file>------------------------
        public static void get_children2<Key, Data, T>(this DBElement<Key, Data> elem, out XElement children_)
          where Data : IEnumerable<T>
        {
            XElement new_Children = new XElement("Children"); ;
            if (elem.payload != null)
            {
                foreach (var item in elem.children)
                {
                    XElement child = new XElement("item", item);
                    new_Children.Add(child);
                }
            }
            children_ = new_Children;
            
        }


        //--------------------< Constructs children element for collection type to load into XML file>------------------------
        public static void get_children2<Key, Data>(this DBElement<Key, Data> elem, out XElement children_)         
        {
            XElement new_Children = new XElement("Children"); ;
            if (elem.payload != null)
            {
                foreach (var item in elem.children)
                {
                    XElement child = new XElement("item", item);
                    new_Children.Add(child);
                }
            }
            children_ = new_Children;

        }




    }


    //------------------------< Test stub for DBExtensions >----------------------------
#if (TEST_DBEXTENSIONS)
  class TestDBExtensions
  {
    static void Main(string[] args)
    {
      "Testing DBExtensions Package".title('=');
      WriteLine();

      Write("\n --- Test DBElement<int,string> ---");
      DBElement<int, string> elem1 = new DBElement<int, string>();
      elem1.payload = "a payload";
      Write(elem1.showElementWithTestType1<int>());
      WriteLine();

      Write("\n --- Test DBElement<string,List<string>> ---");
      DBElement<string, List<string>> newelem1 = new DBElement<string, List<string>>();
      newelem1.name = "newelem1";
      newelem1.descr = "test new type";
      newelem1.children = new List<string> { "Key1", "Key2" };
      newelem1.payload = new List<string> { "one", "two", "three" };
      Write(newelem1.showElementWithTestType2<string>());
      WriteLine();

      Write("\n\n");
    }
  }
#endif
}
