﻿using System.Threading.Tasks;
using Smartstore.Core.Rules;

namespace Smartstore.Core.Checkout.Rules.Impl
{
    public class CurrencyRule : IRule
    {
        public Task<bool> MatchAsync(CartRuleContext context, RuleExpression expression)
        {
            var match = expression.HasListMatch(context.WorkContext.WorkingCurrency.Id);

            return Task.FromResult(match);
        }
    }
}
