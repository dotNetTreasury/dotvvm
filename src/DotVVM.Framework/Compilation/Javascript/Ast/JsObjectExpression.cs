﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotVVM.Framework.Compilation.Javascript.Ast
{
    public class JsObjectExpression: JsExpression
    {
        public static JsTreeRole<JsObjectProperty> PropertyRole = new JsTreeRole<JsObjectProperty>("Property");
        public JsNodeCollection<JsObjectProperty> Properties => new JsNodeCollection<JsObjectProperty>(this, PropertyRole);

        public JsObjectExpression(params JsObjectProperty[] properties) : this((IEnumerable<JsObjectProperty>)properties) { }
        public JsObjectExpression(IEnumerable<JsObjectProperty> properties)
        {
            foreach (var prop in properties)
            {
                AddChild(prop, PropertyRole);
            }
        }

        public override void AcceptVisitor(IJsNodeVisitor visitor) => visitor.VisitObjectExpression(this);
    }

    public class JsObjectProperty: JsNode
    {
        public JsIdentifier Identifier
        {
            get => GetChildByRole(JsTreeRoles.Identifier);
            set => SetChildByRole(JsTreeRoles.Identifier, value);
        }
        public string Name
        {
            get => GetChildByRole(JsTreeRoles.Identifier).Name;
            set => SetChildByRole(JsTreeRoles.Identifier, new JsIdentifier(value));
        }

        public JsExpression Expression
        {
            get => GetChildByRole(JsTreeRoles.Expression);
            set => SetChildByRole(JsTreeRoles.Expression, value);
        }

        public JsObjectProperty(string name, JsExpression key): this(new JsIdentifier(name), key) { }
        public JsObjectProperty(JsIdentifier name, JsExpression key)
        {
            this.Identifier = name;
            this.Expression = key;
        }

        public override void AcceptVisitor(IJsNodeVisitor visitor) => visitor.VisitObjectProperty(this);
    }
}
