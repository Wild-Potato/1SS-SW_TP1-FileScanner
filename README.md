# 1SS-SW_TP1-FileScanner DOC

https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=net-5.0 : Comprendre Task.run, pour le Async

https://stackoverflow.com/questions/18013523/when-correctly-use-task-run-and-when-just-async-await : Approfondir la compréhension du Task.run  « ask.Run(() => DoWork()); »


https://stackoverflow.com/questions/10820137/c-sharp-try-catch-continue-execution : Trouver comment « continue » après avoir catch l’erreur de l’admin
try{
     //ToDo
    }
    catch { continue; }    
    
    
https://docs.devexpress.com/CodeRush/10220/concepts/code-analysis/code-issues/cannot-yield-in-the-body-of-a-try-block-with-a-catch-clause : Comment yield return avec un try catch 

try
        {
            text = File.ReadAllText(fName);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            continue;
        }
        yield return text;
