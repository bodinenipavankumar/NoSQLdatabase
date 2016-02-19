// DBEdit.cs - Performs Edit operations on database                  //
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
 * This package supports editing metadata of Key/Value database.
 *
 */
/*
* Public Interface:
* -----------------
* public static bool edit_instance<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, DBElement<Key, Data> elem, Key key)
* --function supports replacing a value instance with new value instance for Collection type 
*
* public static bool edit_name<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_name, Key key)
* --function supports editing name in metadata for collection type
* 
* public static bool edit_desc<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_desc, Key key)
* --function supports editing description in metadata for collection type
*
* public static bool add_relationship<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)
* --function supports adding child relationship in metadata for collection type
*
* public static bool remove_relationship<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)
* --function supports removing child relationship in metadata for collection type
*
* public static bool AddKeyvaluepair<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key key, DBElement<Key, Data> elem)
* -- function supports adding new Key/Value pair into database of collection type
*
* public static bool deleteKeyvaluepair<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key key)
* -- function supports deleting a Key/Value pair from database of collection type
*
* public static void number_of_writes<Key, Value, Data, T>(this DBEngine<Key, Value> db)
* -- Persists database on number of writes
*/
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs,Persist_Engine.cs,Display.cs,UtilityExtensions.cs
 *                 DBExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 10 Oct 15
 * - first release, added functions to support edit operations on database.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Starter
{
    public static class DBEdit
    {
        /*<------------------------------< function supports replacing a value instance with new value instance for Collection type >------------------------------------>*/
        public static bool edit_instance<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, DBElement<Key, Data> elem, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;//count the number of writes on database
            bool status = db.edit_value(elem, key);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            return status;
        }

        //<------------------------------< function supports replacing an instance with new instance for primitive type >------------------------------------>*/
        public static bool edit_instance<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, DBElement<Key, Data> elem, Key key)
         {
            db.number_of_writes++;
            bool status = db.edit_value(elem, key);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            return status;
        }

        /*<------------------------------< function supports editing name in metadata for collection type >------------------------------------>*/
        public static bool edit_name<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_name, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            if (status)//if key exists in database
            {
                element.name = elem_name;
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports editing name in metadata for primitive type >------------------------------------>*/
        public static bool edit_name<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_name, Key key)         
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            if (status)//if key exists in database
            {
                element.name = elem_name;
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports editing description in metadata for collection type >------------------------------------>*/
        public static bool edit_desc<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_desc, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            if (status)//if key exists in database
            {
                element.descr = elem_desc;
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports editing description in metadata for primitive type >------------------------------------>*/
        public static bool edit_desc<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, string elem_desc, Key key)          
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            if (status)//if key exists in database
            {
                element.descr = elem_desc;
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports adding child relationship in metadata for collection type >------------------------------------>*/
        public static bool add_relationship<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            if (status)//if key exists in database
            {
                element.children.Add(children);
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports adding child relationship in metadata for primitive type >------------------------------------>*/
        public static bool add_relationship<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)          
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            if (status)//if key exists in database
            {
                element.children.Add(children);
                return true;
            }
            return false;
        }


        /*<------------------------------< function supports removing child relationship in metadata for collection type >------------------------------------>*/
        public static bool remove_relationship<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            if (status)//if key exists in database
            {
                element.children.Remove(children);
                return true;
            }
            return false;
        }

        /*<------------------------------< function supports adding child relationship in metadata for primitive type >------------------------------------>*/
        public static bool remove_relationship<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, Key children, Key key)
         {
            db.number_of_writes++;
            DBElement<Key, Data> element;
            bool status = db.getValue(key, out element);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            if (status)//if key exists in database
            {
                element.children.Remove(children);
                return true;
            }
            return false;
        }

        /*<------------------------------< function supports adding new Key/Value pair into database of collection type >------------------------------------>*/
        public static bool AddKeyvaluepair<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key key, DBElement<Key, Data> elem)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            bool status = db.insert(key, elem);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            return status;
        }

        /*<------------------------------< function supports adding new Key/Value pair into database of primitive type >------------------------------------>*/
        public static bool AddKeyvaluepair<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, Key key, DBElement<Key, Data> elem)
        {
            db.number_of_writes++;
            bool status = db.insert(key, elem);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            return status;
        }

        /*<------------------------------< function supports deleting a Key/Value pair from database of collection type >------------------------------------>*/
        public static bool deleteKeyvaluepair<Key, Data, T>(this DBEngine<Key, DBElement<Key, Data>> db, Key key)
            where Data : IEnumerable<T>
        {
            db.number_of_writes++;
            bool status = db.delete(key);
            db.number_of_writes<Key, DBElement<Key, Data>, Data, T>();
            return status;
        }

        /*<------------------------------< function supports deleting a Key/Value pair from database of primitive type >------------------------------------>*/
        public static bool deleteKeyvaluepair<Key, Data>(this DBEngine<Key, DBElement<Key, Data>> db, Key key)
        { 
            db.number_of_writes++;
            bool status = db.delete(key);
            db.number_of_writes<Key, DBElement<Key, Data>, Data>();
            return status;
        }

        /*<------------------------------function supports persisting of collection type database contents on number of writes on database------------------------------------>*/
        public static void number_of_writes<Key, Value, Data, T>(this DBEngine<Key, Value> db)
            where Data : IEnumerable<T>
        {
            
            if (db.number_of_writes == 5)
            {
                string write = "\n***An event triggered by number of writes to persist contents of collection type database to file " + db.filename;
                write.write_console();
                db.number_of_writes = 0;
                db.persist_database<Key, Value, Data, T>();
            }
        }

        /*<------------------------------function supports persisting of primitive type database contents on number of writes on database------------------------------------>*/
        public static void number_of_writes<Key, Value, Data>(this DBEngine<Key, Value> db)           
        {

            
            if (db.number_of_writes == 5)
            {
                string write = "\n***An event triggered by number of writes to persist contents of primitive type database to file " + db.filename;
                write.write_console();
                db.number_of_writes = 0;
                db.persist_database<Key, Value, Data>();
            }

        }

    }
#if (DBEdit)
        class DBEditTest
        {
            static void Main(string[] args)
            {
            //Test stub to test for Primitive types, though this package will support for Collection type DB
            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
            DBEngine<string, DBElement<string, List<string>>> db2 = new DBEngine<string, DBElement<string, List<string>>>();
            DBElement<int, string> elem = new DBElement<int, string>();
            elem.name = "element";
            elem.descr = "test element";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<int> { 1, 3 });
            elem.payload = "elem's payload pk";
            db.insert(elem.Generate_int_key(), elem);

            DBElement<int, string> elem1_ = new DBElement<int, string>();
            elem1_.name = "element2";
            elem1_.descr = "test element2";
            elem1_.timeStamp = DateTime.Now;
            elem1_.children.AddRange(new List<int> { 1, 3 });
            elem1_.payload = "elem's payload pkdsdsd";
            db.insert(elem.Generate_int_key(), elem1_);

            "\nDatabase Contents of primitive type ".write_console();
            db.showWithTestType1();

            IEnumerable<int> keys = db.Keys();
            int edit_key = keys.First();
            int child_key = keys.ElementAt(1);
            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "new instance for primitive";
            elem1.descr = "instance for replacing for primitive";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new[] { 1 });
            elem1.payload = "this is for editing";
            bool status = db.edit_instance<int, string>(elem1, edit_key);
            string write = "\nDatabase Contents of primitive type after replacing with new value instance for key " + edit_key;
            write.write_console();
            db.showWithTestType1();

            bool status1 = db.edit_name<int, string>("new name for edit", edit_key);
            bool status2 = db.edit_desc<int, string>("new desc for edit", edit_key);
            string edit = "\nDatabase Contents of primitive type after editing name and description for key " + edit_key;
            edit.write_console();
            db.showWithTestType1();

            db.add_relationship<int, string>(child_key, edit_key);
            string relation = "\nDatabase Contents of primitive type after adding " + child_key + " as child relationship for key " + edit_key;
            relation.write_console();
            db.showWithTestType1();

            db.remove_relationship<int, string>(child_key, edit_key);
            string del_relation = "\nDatabase Contents of primitive type after removing " + child_key + " as child for key " + edit_key;
            del_relation.write_console();
            db.showWithTestType1();

            "\nDatabase Contents for collection type database".write_console();
            DBElement<string, List<string>> newerelem1 = new DBElement<string, List<string>>();
            newerelem1.name = "newerelem1 testing";
            newerelem1.descr = "better formatting";
            newerelem1.timeStamp = DateTime.Now;
            newerelem1.children.AddRange(new[] { "first", "second" });
            newerelem1.payload = new List<string> { "alpha", "beta", "gamma" };
            newerelem1.payload.Add("delta");
            newerelem1.payload.Add("epsilon");
            // Write(newerelem1.showElementWithTestType2<string>());
            db2.insert(newerelem1.Generate_string_key(), newerelem1);
          

            DBElement<string, List<string>> newerele = new DBElement<string, List<string>>();
            newerele.name = "pk1";
            newerele.descr = "testing replacing instance";
            newerele.timeStamp = DateTime.Now;
            newerele.children.AddRange(new[] { "first", "second" });
            newerele.payload = new List<string> { "alpha", "beta", "gamma" };

            //db2.insert(newerele.Generate_string_key(), newerele);
            db2.AddKeyvaluepair<string, List<string>, string>(newerele.Generate_string_key(), newerele);
            db2.showEnumerableDB();

            IEnumerable<String> keys_ = db2.Keys();
            string edit_key_ = keys_.First();
            string child_key_ = keys_.ElementAt(1);
            DBElement<string, List<string>> elem11_ = new DBElement<string, List<string>>();
            elem11_.name = "new instance";
            elem11_.descr = "instance for replacing";
            elem11_.timeStamp = DateTime.Now;
            elem11_.children.AddRange(new[] { "fifth", "seven" });
            elem11_.payload = new List<string> { "al", "be", "ga" };
            
            bool status_ = db2.edit_instance<string, List<string>, string>(elem11_, edit_key_);
            string write1 = "\nDatabase Contents after replacing with new value instance for key " + edit_key_;
            write1.write_console();
            db2.showEnumerableDB();

            bool status1_ = db2.edit_name<string, List<string>, string>("new name for edit", edit_key_);
            bool status2_ = db2.edit_desc<string, List<string>, string>("new desc for edit", edit_key_);
            string edit_ = "\nDatabase Contents after editing name and description for key " + edit_key_;
            edit_.write_console();
            db2.showEnumerableDB();

            db2.add_relationship<string, List<string>, string>(child_key_, edit_key_);
            string relation_ = "\nDatabase Contents after adding " + child_key_ + " as child relationship for key " + edit_key_;
            relation_.write_console();
            db2.showEnumerableDB();

            db2.remove_relationship<string, List<string>, string>(child_key_, edit_key_);
            string del_relation_ = "\nDatabase Contents after removing " + child_key_ + " as child for key " + edit_key_;
            del_relation_.write_console();
            db2.showEnumerableDB();
        }
        }
#endif
    
}
