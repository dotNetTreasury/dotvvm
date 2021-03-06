#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.Binding;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Utils;

namespace DotVVM.Framework.Compilation.ControlTree
{
    /// <summary>
    /// A runtime implementation of the control tree resolver.
    /// </summary>
    public class DefaultControlTreeResolver : ControlTreeResolverBase
    {
        private readonly IControlBuilderFactory controlBuilderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultControlTreeResolver"/> class.
        /// </summary>
        public DefaultControlTreeResolver(IControlResolver controlResolver, IControlBuilderFactory controlBuilderFactory, IAbstractTreeBuilder treeBuilder)
            : base(controlResolver, treeBuilder)
        {
            this.controlBuilderFactory = controlBuilderFactory;
        }

        protected override void ResolveRootContent(DothtmlRootNode root, IAbstractContentNode view, IControlResolverMetadata viewMetadata)
        {
            ((ResolvedTreeRoot)view).ResolveContentAction = () => base.ResolveRootContent(root, view, viewMetadata);
        }

        protected override IControlType CreateControlType(ITypeDescriptor wrapperType, string virtualPath)
        {
            return new ControlType(ResolvedTypeDescriptor.ToSystemType(wrapperType), virtualPath: virtualPath);
        }

        protected override IDataContextStack CreateDataContextTypeStack(ITypeDescriptor? viewModelType, IDataContextStack? parentDataContextStack = null, IReadOnlyList<NamespaceImport>? namespaceImports = null, IReadOnlyList<BindingExtensionParameter>? extensionParameters = null)
        {

            return DataContextStack.Create(
                ResolvedTypeDescriptor.ToSystemType(viewModelType) ?? typeof(ExpressionHelper.UnknownTypeSentinel),
                parentDataContextStack as DataContextStack,
                namespaceImports, extensionParameters);
        }

        protected override IAbstractBinding CompileBinding(DothtmlBindingNode node, BindingParserOptions bindingOptions, IDataContextStack context, IPropertyDescriptor property)
        {
            if (context == null)
            {
                node.AddError("The DataContext couldn't be evaluated because of the errors above.");
            }
            return treeBuilder.BuildBinding(bindingOptions, context!, node, property);
        }

        protected override object? ConvertValue(string value, ITypeDescriptor propertyType)
        {
            return ReflectionUtils.ConvertValue(value, ((ResolvedTypeDescriptor)propertyType).Type);
        }
    }
}
