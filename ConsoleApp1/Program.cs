// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConsoleApp1;
using Dapper;

//var cancelationToken = new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token;
//cancelationToken.ThrowIfCancellationRequested();
//try
//{
//    await Task.Run(async () => {

//        await Task.Run(() => { 
//            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(2));
//            var f = "";
//        });

//    }, cancelationToken);

//}
//catch (Exception ex)
//{

//	throw;
//}



List<Task> list = new List<Task>();
list = func();
await execAllLists(list);

static List<Task> func()
{
    List<Task> list = new List<Task>();
    var executei = false;
    list.Add(Task.Run( async () =>
    {
        var rand = new Random(); 
        var minutes =  rand.Next(1,5);    
        Console.WriteLine($"executando método de minutos {minutes}");
        System.Threading.Thread.Sleep(TimeSpan.FromMinutes(minutes));
        Console.WriteLine("terminei o método longo");
        executei = true;
        if (minutes <= 2)
        {
            Console.WriteLine("tudo certo vamos dinovo");
            list = func();
            await execAllLists(list);
        }
    }));
    list.Add(Task.Run(async() =>
    {
        
        Console.WriteLine($"timeout por minutos 3");
        System.Threading.Thread.Sleep(TimeSpan.FromMinutes(3));
        if (!executei)
        {
            Console.WriteLine($"não executou reprocessar");
            list = func();
            await execAllLists(list);
        }
    }));
    return list;
}

static async Task execAllLists(List<Task> list)
{
    try
    {

        await Task.WhenAll(list);
    }
    catch (Exception ex)
    {

        throw;
    }
}

Console.ReadKey();