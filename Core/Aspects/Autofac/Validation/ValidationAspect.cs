using Castle.DynamicProxy;
using Core.CrossCuttingConcerts.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
            _validatorType = validatorType;
        }

        //metodun argumanları dediğimiz invocation dır.
        //Buda bizim Manager sınıfındaki aspect i yazdığımız metot olmuş olmaktadır.
        protected override void OnBefore(IInvocation invocation)
        {
            //reflection yöntemi ile ProductValidator türünde bir instance ürettik bunu da validator atadık.
            var validator=(IValidator)Activator.CreateInstance(_validatorType);

            //productValidator un base clasının generic argumanlarının 0. indisli olanı verir bize yani
            //AbstractValidator<Product>, ProductValidator un base i olduğundan bunun generic argumanı olan Product
            //ı entityType a atamış olduk.
            var entityType =_validatorType.BaseType.GetGenericArguments()[0];


            //metotdun argumanlarını bul ve entityType ile aynı olanları döndür.
            var entities = invocation.Arguments.Where(t => t.GetType()==entityType);

            //
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
