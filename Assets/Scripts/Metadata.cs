using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace TerraMission
{
    public class Metadata : Dictionary<string, object>
    {
        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (TryGetValue(key, out value))
                return (T)value;

            return defaultValue;
        }

        public T GetValue<T>(Expression<Func<T>> propertyExpression, T defaultValue = default(T))
        {
            var propertyName = GetPropertyName(propertyExpression);
            return GetValue<T>(propertyName, defaultValue);
        }

        public void SetValue<T>(string key, T value)
        {
            this[key] = value;
        }

        public void SetValue<T>(Expression<Func<T>> propertyExpression, T value)
        {
            var propertyName = GetPropertyName(propertyExpression);
            SetValue(propertyName, value);
        }


        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            //RaisePropertyChanged(propertyExpression);
            return true;
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Invalid argument", "propertyExpression");

            var property = body.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("Argument is not a property", "propertyExpression");

            return property.Name;
        }
    }
}
