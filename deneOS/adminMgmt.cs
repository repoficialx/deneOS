﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Traductor;

namespace deneOS
{
    internal class adminMgmt
    {
        public static bool admin = false;
        public static string[] args;
        public static int argsCount = 0;
        public static void setAdmin(bool isAdmin)
        {
            admin = isAdmin;
        }
        public static bool isAdmin()
        {
            return admin;
        }
        public static string getArgument(int arg)
        {
            if (arg < 0 || arg >= argsCount)
            {
                //throw new ArgumentOutOfRangeException(nameof(arg), "Argument index is out of range.");
                exMgmt.exception myEx = new exMgmt.exception($"{T("ex_argoutofindex")} {T("checkargindexyoutryingtoaccess")}", "adminMgmt", "Ok", null);
                myEx.ShowDialog();
                myEx.Dispose();
            }
            return args[arg];
        }
    }
}
