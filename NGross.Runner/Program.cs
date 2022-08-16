// See https://aka.ms/new-console-template for more information

using System.Reflection;
using NGross.Core.Builders;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Plan;
using System.CommandLine;
using NGross.Core.Manager;

var cmd = new RootCommand();
var path = new Argument<string>("path");
var startCommand = new Command("start", "Execute a Test")
{
    path
};

Console.ForegroundColor = ConsoleColor.Green;

cmd.AddCommand(startCommand);

startCommand.SetHandler((pathValue) =>
{
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine($"Starting new test run on {pathValue}");
    Console.WriteLine("Loading Tests");
    var builder = new TestBuilder(pathValue, 
        new AssemblyLoader(), 
        new ThreadGroupParser(), 
        new ActionParser());
    var test = builder.Build();
    Console.WriteLine("Tests Loaded");
    Console.ForegroundColor = ConsoleColor.White;

}, path);

cmd.InvokeAsync(args);