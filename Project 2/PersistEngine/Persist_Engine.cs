// Persist_Engine.cs - persists database contents                    //
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
 * This package supports persisting database contents to an XML file,
 * augmenting and Restoring data from XML file into database.
 *
 */
/*
* Public Interface:
* -----------------
* public static void persist_database<Key,Value,Data, T>(this DBEngine<Key, Value> db)
* --Supports persisting the contents of collection type database to an XML file 
*
*  public static void persist_database<Key, Value, Data>(this DBEngine<Key, Value> db)
* --Supports persisting the contents of collection type database to an XML file
* 
* public static void augment_to_database<Key, Value, Data, T>(this DBEngine<Key, Value> db,string filename)
* --Supports augmenting the contents of an XML file to collection type database
*
*  public static void augment_to_database<Key, Value, Data>(this DBEngine<Key, Value> db,string filename) 
* --Supports augmenting the contents of an XML file to primitive type database
*
* public static void unpersist_to_database<Key, Value, Data, T>(this DBEngine<Key, Value> db,string filename)
* --Supports restoring the contents of an XML file to collection type database
*
* public static void unpersist_to_database<Key, Value, Data>(this DBEngine<Key, Value> db, string filename) 
* -- Supports restoring the contents of an XML file to primitive type database
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
 * - first release, added functions to support persisting,augmenting and Unpersisting.
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
   public static class Persist_Engine
    {
        public static object Cast { get; private set; }


        //--------------------Supports persisting the contents of collection type database to an XML file-----------
        public static void persist_database<Key,Value,Data, T>(this DBEngine<Key, Value> db)
            where Data : IEnumerable<T>
        {
            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement root = new XElement("root");
            XElement Keytype = new XElement("Keytype",typeof(Key));
            XElement Payloadtype = new XElement("Payloadtype", typeof(Data));
            root.Add(Keytype);
            root.Add(Payloadtype);
            xml.Add(root);
            foreach (Key key in db.Keys())
            {
                Value value_out;
                db.getValue(key, out value_out);
                DBElement<Key, Data> elem = value_out as DBElement<Key, Data>;
                XElement Payload = new XElement("Payload");
                XElement Children = new XElement("Children");
                elem.get_payload2<Key, Data, T>(out Payload);
                XElement child = new XElement("Key", Payload);
                elem.get_children2<Key, Data, T>(out Children);
                child.SetAttributeValue("Key", key.ToString());
                child.SetAttributeValue("Name", elem.name);
                child.SetAttributeValue("Description", elem.descr);
                child.SetAttributeValue("Timestamp", elem.timeStamp);
                root.Add(child);
                child.Add(Children);
            }
            xml.Save("Persist_Collectiontype.xml");
        }


        //--------------------Supports persisting the contents of primitive type database to an XML file-----------
        public static void persist_database<Key, Value, Data>(this DBEngine<Key, Value> db)         
        {
            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement root = new XElement("root");
            XElement Keytype = new XElement("Keytype", typeof(Key));
            XElement Payloadtype = new XElement("Payloadtype", typeof(Data));
            root.Add(Keytype);
            root.Add(Payloadtype);
            xml.Add(root);
            foreach (Key key in db.Keys())
            {
                Value value_out;
                db.getValue(key, out value_out);
                DBElement<Key, Data> elem = value_out as DBElement<Key, Data>;
                XElement Payload = new XElement("Payload");
                XElement Children = new XElement("Children");
                elem.get_payload2<Key, Data>(out Payload);
                XElement child = new XElement("Key", Payload);
                elem.get_children2<Key, Data>(out Children);
                child.SetAttributeValue("Key", key.ToString());
                child.SetAttributeValue("Name", elem.name);
                child.SetAttributeValue("Description", elem.descr);
                child.SetAttributeValue("Timestamp", elem.timeStamp);
                root.Add(child);
                child.Add(Children);
            }
             xml.Save("Persist_Primitivetype.xml");
        }


        //--------------------< Supports augmenting the contents of an XML file to collection type database >-----------
        public static void augment_to_database<Key, Value, Data, T>(this DBEngine<Key, Value> db,string filename)
            where Data : List<T>
        {
            XDocument newdoc=null;
            try {
                newdoc = XDocument.Load(filename);
            }
            catch (Exception )
            {
                string write = "file not found in the location,just printing the existing database";
                write.write_console();
                return;
            }
            XElement keytype = newdoc.Root.Element("Keytype");
            XElement payloadtype = newdoc.Root.Element("Payloadtype");      

            if (typeof(Key).ToString() == keytype.Value.ToString() && typeof(Data).ToString() == payloadtype.Value.ToString())
            {
                IEnumerable<XElement> allElements = newdoc.Root.Elements("Key");
                foreach (XElement elem in allElements)
                {
                    string key_= elem.Attribute("Key").Value;
                    Key key = (Key)Convert.ChangeType(key_, typeof(Key));
                    DBElement<Key, Data> newerelem1 = new DBElement<Key, Data>();
                    newerelem1.name = elem.Attribute("Name").Value;
                    newerelem1.descr = elem.Attribute("Description").Value;
                    newerelem1.timeStamp = DateTime.Now;
                    IEnumerable<XElement> children_ = elem.Elements("Children").Elements("item");                    
                    foreach (var child_ in children_) {

                        //casting children to key type
                        Key child1_ = (Key)Convert.ChangeType(child_.Value, typeof(Key));
                        newerelem1.children.AddRange(new[] { child1_ });
                    }
                    IEnumerable<XElement> payload_ = elem.Elements("Payload").Elements("item");
                    List<T> temp = new List<T>();
                    foreach (var data_ in payload_) {

                        //casting payload to type T
                        T payload1_ = (T)Convert.ChangeType(data_.Value, typeof(T));
                        temp.Add(payload1_);
                    }
                    newerelem1.payload = temp as Data;
                    Value Dbval = (Value)Convert.ChangeType(newerelem1, typeof(Value));
                    db.insert(key, Dbval);                   
                 }
            }
            else
                "Database type is not compatible with XML data".write_console();
         }


        //--------------------< Supports augmenting the contents of an XML file to primitive type database >-----------
        public static void augment_to_database<Key, Value, Data>(this DBEngine<Key, Value> db,string filename)        
        {
            XDocument newdoc = null;

            try
            {
                newdoc = XDocument.Load(filename);
            }
            catch (Exception)
            {
                string write = "file not found in the location, just printing the existing database";
                write.write_console();
                return;
            }

            XElement keytype = newdoc.Root.Element("Keytype");
            XElement payloadtype = newdoc.Root.Element("Payloadtype");

            if (typeof(Key).ToString() == keytype.Value.ToString() && typeof(Data).ToString() == payloadtype.Value.ToString())
            {
                IEnumerable<XElement> allElements = newdoc.Root.Elements("Key");
                foreach (XElement elem in allElements)
                {
                    string key_ = elem.Attribute("Key").Value;
                    Key key = (Key)Convert.ChangeType(key_, typeof(Key));
                    DBElement<Key, Data> newerelem1 = new DBElement<Key, Data>();
                    newerelem1.name = elem.Attribute("Name").Value;
                    newerelem1.descr = elem.Attribute("Description").Value;
                    newerelem1.timeStamp = DateTime.Now;
                    IEnumerable<XElement> children_ = elem.Elements("Children").Elements("item");
                    foreach (var child_ in children_) {

                        //casting children to key type
                        Key child1_ = (Key)Convert.ChangeType(child_.Value, typeof(Key));
                        newerelem1.children.AddRange(new[] { child1_ });
                    }
                    Data payload_ = (Data)Convert.ChangeType(elem.Element("Payload").Value, typeof(Data));
                    newerelem1.payload = payload_;
                    Value Dbval = (Value)Convert.ChangeType(newerelem1, typeof(Value));
                    db.insert(key, Dbval);
                }
            }
            else
                "Database type is not compatible with XML data".write_console();
        }



        //--------------------< Supports restoring the contents of an XML file to collection type database >-----------
        public static void unpersist_to_database<Key, Value, Data, T>(this DBEngine<Key, Value> db,string filename)
            where Data : List<T>
        {
            XDocument newdoc = null;
            try  {
                newdoc = XDocument.Load(filename);
            }
            catch (Exception) {
                string write = "file not found in the location,just printing the existing database"+filename;
                write.write_console();
                return;
            }
            XElement keytype = newdoc.Root.Element("Keytype");
            XElement payloadtype = newdoc.Root.Element("Payloadtype");
            if (typeof(Key).ToString() == keytype.Value.ToString() && typeof(Data).ToString() == payloadtype.Value.ToString())
            {
                IEnumerable<XElement> allElements = newdoc.Root.Elements("Key");
                foreach (XElement elem in allElements)
                {
                    string key_ = elem.Attribute("Key").Value;
                    Key key = (Key)Convert.ChangeType(key_, typeof(Key));
                    DBElement<Key, Data> newerelem1 = new DBElement<Key, Data>();
                    newerelem1.name = elem.Attribute("Name").Value;
                    newerelem1.descr = elem.Attribute("Description").Value;
                    newerelem1.timeStamp = DateTime.Now;
                    IEnumerable<XElement> children_ = elem.Elements("Children").Elements("item");
                    foreach (var child_ in children_) {

                        //casting children to Key type
                        Key child1_ = (Key)Convert.ChangeType(child_.Value, typeof(Key));
                        newerelem1.children.AddRange(new[] { child1_ });
                    }

                    IEnumerable<XElement> payload_ = elem.Elements("Payload").Elements("item");
                    List<T> temp = new List<T>();
                    foreach (var data_ in payload_) {

                        //casting payload to T type
                        T payload1_ = (T)Convert.ChangeType(data_.Value, typeof(T));
                        temp.Add(payload1_);
                    }
                    newerelem1.payload = temp as Data;
                    Value Dbval = (Value)Convert.ChangeType(newerelem1, typeof(Value));
                    db.insert(key, Dbval);
                }
            }
            else
                "Database type is not compatible with XML data".write_console();
        }



        //--------------------Supports restoring the contents of an XML file to collection type database-----------
        public static void unpersist_to_database<Key, Value, Data>(this DBEngine<Key, Value> db, string filename)           
        {
            XDocument newdoc = null;
            try
            {
                newdoc = XDocument.Load(filename);
            }
            catch (Exception)
            {
                string write = "file not found in the location,just printing the existing database";
                write.write_console();
                return;
            }
            XElement keytype = newdoc.Root.Element("Keytype");
            XElement payloadtype = newdoc.Root.Element("Payloadtype");

            if (typeof(Key).ToString() == keytype.Value.ToString() && typeof(Data).ToString() == payloadtype.Value.ToString())
            {
                IEnumerable<XElement> allElements = newdoc.Root.Elements("Key");
                foreach (XElement elem in allElements)
                {
                    string key_ = elem.Attribute("Key").Value;
                    Key key = (Key)Convert.ChangeType(key_, typeof(Key));
                    DBElement<Key, Data> newerelem1 = new DBElement<Key, Data>();
                    newerelem1.name = elem.Attribute("Name").Value;
                    newerelem1.descr = elem.Attribute("Description").Value;
                    newerelem1.timeStamp = DateTime.Now;
                    IEnumerable<XElement> children_ = elem.Elements("Children").Elements("item");
                    foreach (var child_ in children_) {                        
                        //casting children to key type
                        Key child1_ = (Key)Convert.ChangeType(child_.Value, typeof(Key));
                        newerelem1.children.AddRange(new[] { child1_ });
                    }
                     //casting payload to Data type
                    Data payload_ = (Data)Convert.ChangeType(elem.Element("Payload").Value, typeof(Data));
                    newerelem1.payload = payload_;
                    Value Dbval = (Value)Convert.ChangeType(newerelem1, typeof(Value));
                    db.insert(key, Dbval);
                }
            }
            else
                "Database type is not compatible with XML data".write_console();
        }
    }
#if (TEST_PERSISTENTENGINE)
    public class Test_persist
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

            "\nPersist contents of primitive type database to XML file".write_console();
            DBEngine<int, DBElement<int, string>> db_ = new DBEngine<int, DBElement<int, string>>();
            db1.persist_database<int, DBElement<int, string>, string>();
            db1.showWithTestType1();
            "\nData after Augmenting data from XML file to primitive type database".write_console();
            db1.augment_to_database<int, DBElement<int, string>, string>("augment_primitive.xml");
            db1.showWithTestType1();
            "\nData after Unpersisting data from XML file to primitive type database".write_console();
            db_.unpersist_to_database<int, DBElement<int, string>, string>("primitive.xml");
            db_.showWithTestType1();



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
            db2.insert(newerele.Generate_string_key(), newerele);
            db2.showEnumerableDB();
            "\nPersist contents of collection database to XML file".write_console();
            DBEngine<string, DBElement<string, List<string>>> db = new DBEngine<string, DBElement<string, List<string>>>();
            db2.persist_database<string, DBElement<string, List<string>>, List<string>, string>();
            db2.showEnumerableDB();
            "\nData after Augmenting data from XML file to collection database".write_console();
            db2.augment_to_database<string, DBElement<string, List<string>>, List<string>, string>("augment.xml");
            db2.showEnumerableDB();
            "\nData after Unpersisting data from XML file to collection database".write_console();
            db.unpersist_to_database<string, DBElement<string, List<string>>, List<string>, string>("Test.xml");
            db.showEnumerableDB();
        }
#endif
    }
}
