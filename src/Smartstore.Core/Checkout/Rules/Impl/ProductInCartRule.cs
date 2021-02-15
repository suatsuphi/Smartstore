﻿using System.Linq;
using System.Threading.Tasks;
using Smartstore.Core.Checkout.Cart;
using Smartstore.Core.Rules;

namespace Smartstore.Core.Checkout.Rules.Impl
{
    public class ProductInCartRule : IRule
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ProductInCartRule(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<bool> MatchAsync(CartRuleContext context, RuleExpression expression)
        {
            var cart = await _shoppingCartService.GetCartItemsAsync(context.Customer, ShoppingCartType.ShoppingCart, context.Store.Id);
            var productIds = cart
                .Select(x => x.Item.ProductId)
                .Distinct()
                .ToArray();

            var match = expression.HasListsMatch(productIds);
            return match;
        }
    }
}
