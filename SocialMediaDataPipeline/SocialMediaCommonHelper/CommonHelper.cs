using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMediaCommonHelper
{
    public static class CommonHelper
    {
        public static void ExecuteIgnoreException(Action action, bool exceptionEmail = false, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine(ex.Message);
            }
        }
        public static T RetryMethod<T>(Func<T> action, int numRetries = 10, int retryTimeout = 60,
       [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            T retval = default(T);
            Random r = new Random();
            Guid retryCID = Guid.NewGuid();

            do
            {
                try
                {
                    retval = action();
                    return retval;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //onFailureAction();
                    if (numRetries <= 0) throw; // improved to avoid silent failure

                    // TODO: Log exception

                    Thread.Sleep(r.Next(retryTimeout, retryTimeout * 2) * 1000);
                }
            } while (numRetries-- > 0);
            return retval;
        }
        public static void RetryMethod(Action action, int numRetries = 10, int retryTimeout = 60,
   [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            RetryMethod<object>(() => { action(); return null; }, numRetries, retryTimeout, sourceFilePath: sourceFilePath, memberName: memberName, sourceLineNumber: sourceLineNumber);
        }

        public static void RetryAndIgnore(Action action, int numRetries = 10, int retryTimeout = 60,
        [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            ExecuteIgnoreException(() =>
            {
                RetryMethod<object>(() =>
                {
                    action(); return null;
                }, numRetries, retryTimeout, sourceFilePath: sourceFilePath, memberName: memberName, sourceLineNumber: sourceLineNumber);
            });
        }
    }
}
