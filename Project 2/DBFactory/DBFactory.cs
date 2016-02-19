// DBFactory.cs - Creates new immutable database                     //
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
 * This package supports creation of immutable database from resultant keys of query results.
 *
 */
/*
* Public Interface:
* -----------------
* public DBFactory(DBEngine<Key, DBElement<Key, Data>> DBEngine)
* --Constructor, initializes properties. 
*
* public void addKey(Key key)
* --Supports adding new key to immutable database
* 
* public List<Key> Keys()
* --Returns the list of keys in immutable database.
*
* public bool doKeyPatternSearch(string pattern,out List<Key> l)
* --Returns list of keys that starts with given pattern
*
* public bool getValue(Key key, out DBElement<Key, Data> val)
* --Supports getting the value object for given key
*
*
*/
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs
 *                 
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 10 Oct 15
 * - first release, added functions to creation of new immutable database.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Starter
{
    public class DBFactory<Key, Data>
    {
        DBEngine<Key, DBElement<Key, Data>> db;
        List<Key> keys=new List<Key>();

        //------------------------------------< DBFactory constructor >-------------------------------
        public DBFactory(DBEngine<Key, DBElement<Key, Data>> DBEngine)
        {
            db = DBEngine;
        }

        //---------------< Supports adding new key to immutable database >-------------------------------- 
       public void addKey(Key key)
        {
            keys.Add(key);
        }

        //-----------------< Returns the list of keys in immutable database >-------------------------------
        public List<Key> Keys()
        {
            return keys;
        }


        //----------------< Supports getting the value object for given key >-----------------------------------
        public bool getValue(Key key, out DBElement<Key, Data> val)
        {
          bool status = db.getValue(key, out val);
            return status;          
        }

    }
    #if (TESTDBFACTORY)
        class DBFactoryTest
        {
            static void Main(string[] args)
            {
            //There would be circular reference created if DBEngine is created here, so created 
            //TESTDBFactory package to test DBFactory
            }
        }
#endif
    }

