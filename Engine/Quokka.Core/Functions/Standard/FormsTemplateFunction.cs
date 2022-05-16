using System;

namespace Mindbox.Quokka
{
	internal class FormsTemplateFunction : ScalarTemplateFunction<decimal, string, string, string, string>
	{
		public FormsTemplateFunction()
			: base(
				"forms",
				new DecimalFunctionArgument("quantity"),
				new StringFunctionArgument("singularForm"),
				new StringFunctionArgument("dualForm"),
				new StringFunctionArgument("pluralForm"))
		{
		}

		public override string Invoke(decimal quantity, string singularForm, string dualForm, string pluralForm)
		{
			var intQuantity = Convert.ToInt32(quantity);
			return LanguageUtility.GetQuantityForm(intQuantity, singularForm, dualForm, pluralForm);
		}
	}
}