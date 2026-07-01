using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace registration.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var roleId =
                context.HttpContext.Session
                .GetInt32("RoleId");

            if (roleId == null)
            {
                context.Result =
                    new RedirectToActionResult(
                        "Index",
                        "Login",
                        null);

                return;
            }

            if (roleId != 1)
            {
                context.Result =
                    new ContentResult
                    {
                        Content = "Access Denied"
                    };

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}