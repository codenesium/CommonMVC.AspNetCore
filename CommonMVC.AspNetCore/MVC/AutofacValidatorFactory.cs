using Autofac;
using FluentValidation;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// From https://json.codes/blog/integrating-fluent-validation-with-aspnet-web-api/
    /// Provides a validation module for classes that end with "Validator"
    /// </summary>
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (_context.TryResolve(validatorType, out instance))
            {
                var validator = instance as IValidator;
                return validator;
            }

            return null;
        }
    }

    public class ValidationModule : Autofac.Module
    {
        private List<Type> _types = new List<Type>();

        public ValidationModule(List<Type> types)
        {
            this._types = types;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(this._types.ToArray())
           .Where(t => t.Name.EndsWith("Validator"))
           .AsImplementedInterfaces()
           .AsSelf()
           .PropertiesAutowired();

            builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();

            base.Load(builder);
        }
    }
}