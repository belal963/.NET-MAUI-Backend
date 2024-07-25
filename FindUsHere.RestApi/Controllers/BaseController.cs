using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FindUsHere.General.Interfaces;
using FindUsHere.General;
using System.Data.SqlClient;

namespace FindUsHere.RestApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IDBConnection _connection;

        protected BaseController(IDBConnection connection)
        {
            _connection = connection;
        }

        #region TryCatch Methods
        protected async Task<IActionResult> TryCatch<returnT>(Func<returnT> func)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Ok(func());
                }
                catch (BadRequestException e)
                {
                    return MakeTrouble(e, 400);
                }
                catch (NotFoundException e)
                {
                    return MakeTrouble(e, 404);
                }
                catch (AlreadyExistsException e)
                {
                    return MakeTrouble(e, 208);
                }
                catch (SqlException e)
                {
                    return MakeTrouble(new InternalServerErorrException(e.Message) , 500); 
                }
                catch (Exception e)
                {
                    return MakeTrouble(e, 418);
                }
            });
        }

        protected async Task<IActionResult> TryCatch(Action action)
        {
            try
            {
                await Task.Run(action);
                return Ok();
            }
            catch (BadRequestException e)
            {
                return MakeTrouble(e, 400);
            }
            catch (NotFoundException e)
            {
                return MakeTrouble(e, 404);
            }
            catch (Exception e)
            {
                return MakeTrouble(e, 500);
            }
        }

        ObjectResult MakeTrouble(Exception e, int statusCode)
        {
            string detail = e.GetType().Name + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace;
            while (e.InnerException != null)
            {
                e = e.InnerException;
                detail += e.GetType().Name + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace;
            }
            return Problem(detail: detail, statusCode: statusCode, type: e.GetType().Name);
        }
        #endregion
    }
}
