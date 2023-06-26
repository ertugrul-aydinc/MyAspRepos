using System;
using _6___ViewComonentResultExamples.Models;
using Microsoft.AspNetCore.Mvc;

namespace _6___ViewComonentResultExamples.ViewComponents
{
	public class GridViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(PersonGridModel personGridModel)
		{
			return View(personGridModel);
		}
	}
}

