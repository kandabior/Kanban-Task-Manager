using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace milstone3
{
    class Program
    {
        /*public static void string(string[] args)
        {
            //Due to the presentation requirements, we have added only one task title Print method.
            //At first, we had a regular print of all the existing tasks data, as requested at work.
            UserController.initiateProg();
             UserController.register("kandabior@gmail.com","K2006k"); //created new user: kandabior@gmail.com
             UserController.register("kandabior@com", "K2006k"); //username not valied
             UserController.register("erezshmueli@gmail.com", "K2006k"); //created new user: erezshmueli@gmail.com
             UserController.register("kandabior@gmail.com", "K2006"); //user exist
             UserController.register("eladsolomon@gmail.com", "K2006k"); //created new user: eladsolomon@gmail.com
             UserController.login("kandabior@gmail.com", "K2006k"); //kandabior@gmail.com loged in successfuly
             UserController.addTask("first task", "now we will add the first task", "kandabior@gmail.com", "20/08/2020");
             //new task added by kandabior@gmail.com, your task Id 1
             UserController.printTask("kandabior@gmail.com", 1, ColumnState.BackLog); //first task
             UserController.changeState(1, "kandabior@gmail.com");
             UserController.logOut("kandabior@gmail.com");
             Console.Read();
        }*/
    }
}
