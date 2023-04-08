// IFN563 Assignment
// Created by Katie Tran - n11159243
using System;
using static System.Console;
using System.IO;

namespace Tic_Tac_Toe_Assignment
{
    internal class Program
    {
        static void Main()
        {
            UserInterface UI = new UserInterface();
            UI.StartApp();
            UI.MainApp();
        }
    }
}