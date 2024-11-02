using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QLTraSua
{
	public class AuthorizeRoleAttribute : ActionFilterAttribute
	{
		private readonly string[] allowedRoles;

		public AuthorizeRoleAttribute(params string[] roles)
		{
			allowedRoles = roles;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var userRole = context.HttpContext.Session.GetString("UserRole");

			if (userRole == null || !allowedRoles.Contains(userRole))
			{
				context.Result = new RedirectToActionResult("Login", "Auth", null);
			}
			base.OnActionExecuting(context);
		}
	}
}
