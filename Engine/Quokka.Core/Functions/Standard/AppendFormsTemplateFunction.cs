using System;

namespace Mindbox.Quokka
{
	internal class AppendFormsTemplateFunction : ScalarTemplateFunction<decimal, string, string, string, string>
	{
		public AppendFormsTemplateFunction()
			: base(
				"appendForms",
				new DecimalFunctionArgument("quantity"), 
				new StringFunctionArgument("singularForm"), 
				new StringFunctionArgument("dualForm"), 
				new StringFunctionArgument("pluralForm"))
		{
		}

		public override string Invoke(decimal quantity, string singularForm, string dualForm, string pluralForm)
		{
			var intQuantity = Convert.ToInt32(quantity);
			return quantity + " " + LanguageUtility.GetQuantityForm(intQuantity, singularForm, dualForm, pluralForm);
		}
	}
}
