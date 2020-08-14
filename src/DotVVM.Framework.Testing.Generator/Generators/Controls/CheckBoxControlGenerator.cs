﻿using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Testing.Generator;

namespace DotVVM.Framework.Testing.Generator.Generators.Controls
{
    public class CheckBoxControlGenerator : SeleniumGenerator<CheckBox>
    {
        private static readonly DotvvmProperty[] nameProperties = { CheckBox.CheckedProperty, CheckBox.CheckedItemsProperty, Validator.ValueProperty };

        public override DotvvmProperty[] NameProperties => nameProperties;

        public override bool CanUseControlContentForName => false;



        protected override void AddDeclarationsCore(PageObjectDefinition pageObject, SeleniumGeneratorContext context)
        {
            const string type = "CheckBoxProxy";
            AddPageObjectProperties(pageObject, context, type);
        }
    }
}
