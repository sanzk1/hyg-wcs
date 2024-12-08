using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AspectCore.DynamicProxy;

namespace infrastructure.Attributes
{
    /// <summary>
    /// 事务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionScopeAttribute : AbstractInterceptorAttribute
    {

        public TransactionScopeOption option;
        public TransactionScopeAttribute(TransactionScopeOption option){
            this.option = option;
        }
        public TransactionScopeAttribute(){
            
        }
               
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            if(option == null)
                option = TransactionScopeOption.Suppress;
            using var scope = new TransactionScope(option);
             try{

                await next(context);
                scope.Complete();

             }catch(Exception ex)
             {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
             }
        }

    }
}